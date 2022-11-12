using LearningPortal.Domain.FileServers.FilePathAgg.Contracts;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using System;
using LearningPortal.Application.Contract.ApplicationDTO.FilePath;
using Microsoft.EntityFrameworkCore;
using LearningPortal.Framework.Common.ExMethod;

namespace LearningPortal.Application.App.FilePath
{
    public class FilePathApplication : IFilePathApplication
    {
        private readonly ILogger _Logger;
        private readonly IFilePathRepository _FilePathRepository;
        private readonly IServiceProvider _ServiceProvider;

        public FilePathApplication(ILogger logger, IFilePathRepository filePathRepository, IServiceProvider serviceProvider)
        {
            _Logger=logger;
            _FilePathRepository=filePathRepository;
            _ServiceProvider=serviceProvider;
        }

        public async Task<string> GetDirectoryPathByPathIdAsync(InpGetDirectoryPathByPathId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                return await _FilePathRepository.Get
                                                .Where(a => a.Id == Input.FilePathId.ToGuid())
                                                .Select(a => a.Path)
                                                .SingleOrDefaultAsync();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<string> GetProfileImagePathIdAsync(InpGetProfileImagePathId Input)
        {
            try
            {
                var qData = await _FilePathRepository.Get
                                        .Where(a => a.FileServerId == Input.FileServerId.ToGuid())
                                        .Where(a => a.Path == "/ProfileImg/")
                                        .Select(a => a.Id.ToString())
                                        .SingleOrDefaultAsync();

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
