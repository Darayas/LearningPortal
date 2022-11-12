using System.IO;

namespace LearningPortal.Framework.Application.Services.FTP
{
    public interface IFtpWorker
    {
        bool CheckFileExist(string FtpPath, string Path, string FileName);
        void Connect(string FtpHost, string FtpPort, string FtpUserName, string FtpPassword);
        bool CreateDirectoryExist(string FtpPath, string Path);
        void Disconnect();
        long GetFileSize(string FtpPath, string Path, string FileName);
        bool RemoveFile(string FtpPath, string Path, string FileName);
        bool Upload(Stream _File, string FtpPath, string Path, string FileName);
    }
}