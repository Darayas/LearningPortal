using FluentFTP;
using FluentFTP.Helpers;
using LearningPortal.Framework.Contracts;
using System;
using System.IO;
using System.Net;

namespace LearningPortal.Framework.Application.Services.FTP
{
    public class FtpWorker : IFtpWorker
    {
        private readonly ILogger _Logger;
        private FtpClient _FtpClient;
        public FtpWorker(ILogger logger)
        {
            _Logger=logger;
        }

        public void Connect(string FtpHost, string FtpPort, string FtpUserName, string FtpPassword)
        {
            _FtpClient = new FtpClient(new Uri(FtpHost).Host, new NetworkCredential(FtpUserName, FtpPassword), int.Parse(FtpPort));
            _FtpClient.Connect();
        }

        public void Disconnect()
        {
            if (_FtpClient != null)
                if (_FtpClient.IsConnected)
                {
                    _FtpClient.Disconnect();
                    _FtpClient.Dispose();
                    _FtpClient=null;
                }
        }

        public bool Upload(Stream _File, string FtpPath, string Path, string FileName)
        {
            try
            {
                _File.Position = 0;
                var _Result = _FtpClient.UploadStream(_File, $"/{FtpPath.Trim('/')}/{Path.Trim('/')}/{FileName.Trim('/')}", FtpRemoteExists.Overwrite, createRemoteDir: true);
                if (_Result.IsSuccess())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public bool CheckDirectoryExist(string FtpPath, string Path)
        {
            try
            {
                var _Result = _FtpClient.DirectoryExists($"/{FtpPath.Trim('/')}/{Path.Trim('/')}");
                return _Result;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public bool CreateDirectoryExist(string FtpPath, string Path)
        {
            try
            {
                var _Result = _FtpClient.CreateDirectory($"/{FtpPath.Trim('/')}/{Path.Trim('/')}");
                return _Result;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public bool RemoveFile(string FtpPath, string Path, string FileName)
        {
            try
            {
                _FtpClient.DeleteFile($"/{FtpPath.Trim('/')}/{Path.Trim('/')}/{FileName.Trim('/')}");
                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public bool CheckFileExist(string FtpPath, string Path, string FileName)
        {
            try
            {
                var _Result = _FtpClient.FileExists($"/{FtpPath.Trim('/')}/{Path.Trim('/')}/{FileName.Trim('/')}");
                return _Result;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public long GetFileSize(string FtpPath, string Path, string FileName)
        {
            try
            {
                var _Result = _FtpClient.GetFileSize($"/{FtpPath.Trim('/')}/{Path.Trim('/')}/{FileName.Trim('/')}");
                return _Result;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return -1;
            }
        }
    }
}
