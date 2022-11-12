using LearningPortal.Application.Contract.ApplicationDTO.FileServer;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using LearningPortal.Domain.FileServers.ServerAgg.Contracts;
using LearningPortal.Domain.FileServers.ServerAgg.Entity;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Const;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.FileServer
{
    public class FileServerApplication : IFileServerApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IFileServerRepository _FileServerRepository;

        public FileServerApplication(IFileServerRepository fileServerRepository, ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer)
        {
            _FileServerRepository=fileServerRepository;
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _Localizer=localizer;
        }

        public async Task<OperationResult<OutGetServerDetails>> GetServerDetailsAsync(InpGetServerDetails Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _MapConfig = TypeAdapterConfig<tblFileServers, OutGetServerDetails>.NewConfig()
                                                    .Map(dest => dest.FreeSpace, src => src.Capacity - src.tblFilePaths.SelectMany(b => b.tblFiles.Select(c => c.SizeOnDisk)).Sum()).Config;

                var qData = await _FileServerRepository.Get
                                                       .Where(a => a.Id==Input.ServerId.ToGuid())
                                                       .Where(a => a.IsActive)
                                                       .ProjectToType<OutGetServerDetails>(_MapConfig)
                                                       .SingleOrDefaultAsync();

                if (qData==null)
                    return new OperationResult<OutGetServerDetails>().Failed("ServerId is invalid");

                string DecryptedData = qData.FtpData.AesDecrypt(AuthConst.SecretKey);

                var _FtpData = JsonSerializer.Deserialize<OutGetServerDetails>(DecryptedData);

                qData.FtpHost = _FtpData.FtpHost;
                qData.FtpPath = _FtpData.FtpPath;
                qData.FtpPort = _FtpData.FtpPort;
                qData.FtpUserName = _FtpData.FtpUserName;
                qData.FtpPassword = _FtpData.FtpPassword;

                return new OperationResult<OutGetServerDetails>().Succeeded(qData);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<OutGetServerDetails>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<OutGetServerDetails>().Failed(_Localizer["Error500"]);
            }
        }

        public async Task<OperationResult<string>> GetBestServerIdAsync(InpGetBestServerId Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _FileServerRepository.Get
                                    .Where(a => a.IsPrivate==false)
                                    .Select(a => new
                                    {
                                        Id = a.Id.ToString(),
                                        FreeSize = a.Capacity - a.tblFilePaths.SelectMany(b => b.tblFiles.Select(c => c.SizeOnDisk)).Sum()
                                    })
                                    .OrderByDescending(a => a.FreeSize)
                                    .FirstOrDefaultAsync();

                return new OperationResult<string>().Succeeded(qData.Id);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed(_Localizer["Error500"]);
            }
        }
    }
}
