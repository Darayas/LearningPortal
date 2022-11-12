using LearningPortal.Application.App.FilePath;
using LearningPortal.Application.App.FileServer;
using LearningPortal.Application.Contract.ApplicationDTO.FilePath;
using LearningPortal.Application.Contract.ApplicationDTO.Files;
using LearningPortal.Application.Contract.ApplicationDTO.FileServer;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using LearningPortal.Domain.FileServers.FileAgg.Contratcs;
using LearningPortal.Domain.FileServers.FileAgg.Entity;
using LearningPortal.Framework.Application.Services.FTP;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.Framework.Services.AntiShell;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.Files
{
    public class FileApplication : IFileApplication
    {
        private readonly ILogger _Logger;
        private readonly IFtpWorker _FtpWorker;
        private readonly IAntiShell _AntiShell;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IFileRepository _FileRepository;
        private readonly IFilePathApplication _FilePathApplication;
        private readonly IFileServerApplication _FileServerApplication;

        public FileApplication(ILogger logger, IFileRepository fileRepository, IFtpWorker ftpWorker, ILocalizer localizer, IServiceProvider serviceProvider, IFilePathApplication filePathApplication, IFileServerApplication fileServerApplication, IAntiShell antiShell)
        {
            _Logger=logger;
            _FileRepository=fileRepository;
            _FtpWorker=ftpWorker;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _FilePathApplication=filePathApplication;
            _FileServerApplication=fileServerApplication;
            _AntiShell=antiShell;
        }

        public async Task<OperationResult<string>> UploadProfileImageAsync(InpUploadProfileImage Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion

                #region Validate Mime Type
                (string Extentiton, string[] Mime) _ValidMimeType = default;
                {
                    _ValidMimeType = await _AntiShell.GetExtentionAsync(Input.FormFile);
                    if (_ValidMimeType==default)
                        return new OperationResult<string>().Failed("FileFormatIsInvalid");
                }
                #endregion

                #region Generate file name
                string _FileName;
                {
                    _FileName = new Guid().SequentialGuid().ToString().Replace("-", "") + "." + _ValidMimeType.Extentiton;
                }
                #endregion

                #region UploadFile
                {
                    var _Result = await UploadFileAsync(new InpUploadFile
                    {
                        Title=Input.Title,
                        FileName=_FileName,
                        FilePathId=Input.FilePathId,
                        FileServerId=Input.FileServerId,
                        UploaderUserId=Input.UploaderUserId,
                        Files=Input.FormFile
                    });

                    if (_Result.IsSucceeded)
                        return new OperationResult<string>().Succeeded(_Result.Data);
                    else
                        return new OperationResult<string>().Failed(_Result.Message);
                }
                #endregion
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

        private async Task<bool> CheckFileNameIsDuplicateAsync(string FileServerId, string FilePathId, string FileName)
        {
            var qData = await _FileRepository.Get
                                             .Where(a => a.tblFilePaths.FileServerId == FileServerId.ToGuid())
                                             .Where(a => a.FilePathId == FilePathId.ToGuid())
                                             .Where(a => a.FileName == FileName)
                                             .AnyAsync();

            return qData;
        }

        private bool CheckExistFileInFtp(string FtpPath, string Path, string FileName)
        {
            return _FtpWorker.CheckFileExist(FtpPath, Path, FileName);
        }

        public async Task<OperationResult<string>> UploadFileAsync(InpUploadFile Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion

                #region Validate Mime Type
                (string Extentiton, string[] Mime) _ValidMimeType = default;
                {
                    _ValidMimeType = await _AntiShell.GetExtentionAsync(Input.Files);
                    if (_ValidMimeType==default)
                        return new OperationResult<string>().Failed("FileFormatIsInvalid");
                }
                #endregion

                #region برسی تکراری نبودن نام فایل در دیتابیس
                {
                    var IsDuplicate = await CheckFileNameIsDuplicateAsync(Input.FileServerId, Input.FilePathId, Input.FileName);
                    if (IsDuplicate)
                        return new OperationResult<string>().Failed("FileNameIsDuplicated");
                }
                #endregion

                #region واکشی مسیر پوشه
                string _Path;
                {
                    _Path = await _FilePathApplication.GetDirectoryPathByPathIdAsync(new InpGetDirectoryPathByPathId { FilePathId=Input.FilePathId });
                    if (_Path == null)
                        return new OperationResult<string>().Failed("PathIdIsInvalid");

                }
                #endregion

                #region واکشی اطلاعات سرور
                OutGetServerDetails qServer;
                {
                    var _Result = await _FileServerApplication.GetServerDetailsAsync(new InpGetServerDetails { ServerId=Input.FileServerId });
                    if (_Result == null)
                        return new OperationResult<string>().Failed(_Result.Message);

                    qServer= _Result.Data;
                }
                #endregion

                #region Check Server Free Space
                {
                    if (qServer.FreeSpace < (Input.Files.Length + Input.Files.Length * 0.25))
                    {
                        _Logger.Fatal($"{qServer.Name} is full.");
                        return new OperationResult<string>().Failed("Server not Respond");
                    }
                }
                #endregion

                #region Connect to ftp
                {
                    _FtpWorker.Connect(qServer.FtpHost, qServer.FtpPort, qServer.FtpUserName, qServer.FtpPassword);
                }
                #endregion

                #region برسی وجود فایل در FTP
                {
                    var _Result = CheckExistFileInFtp(qServer.FtpPath, _Path, Input.FileName);
                    if (_Result==true)
                        return new OperationResult<string>().Failed("FileName is duplicated");
                }
                #endregion

                #region واکشی اطلاعات ویدیوها
                {
                    // TODO: استفاده از FFMPEG
                }
                #endregion

                #region Upload file and add to database
                {
                    #region Upload File
                    {
                        var _Result = _FtpWorker.Upload(Input.Files.OpenReadStream(), qServer.FtpPath, _Path, Input.FileName);
                        if (_Result==false)
                            return new OperationResult<string>().Failed("UploadFaild");
                    }
                    #endregion

                    #region Add file info to database
                    {
                        #region Add
                        string _Id;
                        {
                            _Id = new Guid().SequentialGuid().ToString();
                            var _Result = await AddFileInfoAsync(new InpAddFileInfo
                            {
                                Id=_Id,
                                FileMetaData="",
                                FileName=Input.FileName,
                                FileServerId=qServer.Id,
                                MimeType=Input.Files.ContentType,
                                PathId=Input.FilePathId,
                                SizeOnDisk=Input.Files.Length,
                                Title=Input.Title,
                                UserId=Input.UploaderUserId
                            });

                            if (_Result.IsSucceeded)
                                return new OperationResult<string>().Succeeded(_Id);
                        }
                        #endregion

                        #region حذف فایل آپلود شده در صورتی که اطلاعات فایل ثبت نشود
                        {
                            var _Result = _FtpWorker.RemoveFile(qServer.FtpPath, _Path, Input.FileName);
                            if (_Result == false)
                                _Logger.Error($"فایل روی سرور آپلود شد اما قادر به ثبت مشخصات آن نبودیم. همچنین سعی به حذف فایل با شکست مواجه شد. اطلاعات فایل: ServerId:'{qServer.Id}', ServerPath: '{qServer.FtpPath}', Path:'{_Path}', FileName: {Input.FileName}");

                            return new OperationResult<string>().Failed("UploadFaild");
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        private async Task<OperationResult> AddFileInfoAsync(InpAddFileInfo Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion

                #region برسی تکراری نبودن نام فایل در دیتابیس
                {
                    var IsDuplicate = await CheckFileNameIsDuplicateAsync(Input.FileServerId, Input.PathId, Input.FileName);
                    if (IsDuplicate)
                        return new OperationResult().Failed("FileNameIsDuplicated");
                }
                #endregion

                tblFiles tFile = new tblFiles
                {
                    Id= Input.Id.ToGuid(),
                    Date=DateTime.Now,
                    FilePathId=Input.PathId.ToGuid(),
                    MimeType=Input.MimeType,
                    UserId=Input.UserId==null ? null : Input.UserId.ToGuid(),
                    FileName=Input.FileName,
                    SizeOnDisk=Input.SizeOnDisk,
                    Title=Input.Title,
                    FileMetaData=Input.FileMetaData
                };

                await _FileRepository.AddAsync(tFile, default, true);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> RemoveFileAsync(InpRemoveFile Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion

                #region Get file data from db
                var qData = await _FileRepository.Get
                                    .Where(a => a.Id==Input.FileId.ToGuid())
                                    .Select(a => new
                                    {
                                        File = a,
                                        FileServerId = a.tblFilePaths.FileServerId.ToString(),
                                        FilePathId = a.FilePathId.ToString(),
                                        FilePath = a.tblFilePaths.Path,
                                        FileName = a.FileName
                                    })
                                    .SingleOrDefaultAsync();
                #endregion

                #region Remove file from ftp
                {
                    #region واکشی اطلاعات سرور
                    OutGetServerDetails qServer;
                    {
                        var _Result = await _FileServerApplication.GetServerDetailsAsync(new InpGetServerDetails { ServerId = qData.FileServerId });
                        if (_Result == null)
                            return new OperationResult<string>().Failed(_Result.Message);

                        qServer= _Result.Data;
                    }
                    #endregion

                    #region Connect to ftp
                    {
                        _FtpWorker.Connect(qServer.FtpHost, qServer.FtpPort, qServer.FtpUserName, qServer.FtpPassword);
                    }
                    #endregion

                    #region برسی وجود فایل در FTP
                    {
                        var _Result = CheckExistFileInFtp(qServer.FtpPath, qData.FilePath, qData.FileName);
                        if (_Result==false)
                            return new OperationResult<string>().Failed("File not found");
                    }
                    #endregion

                    #region Remove File
                    {
                        var _Result = _FtpWorker.RemoveFile(qServer.FtpPath, qData.FilePath, qData.FileName);
                        if (_Result == false)
                            return new OperationResult().Failed("Cant remove file");
                    }
                    #endregion
                }
                #endregion

                #region Remove file from database
                {
                    await _FileRepository.DeleteAsync(qData.File, default);
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
