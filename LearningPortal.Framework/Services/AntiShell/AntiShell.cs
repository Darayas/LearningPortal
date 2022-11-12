using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Framework.Services.AntiShell
{
    public class AntiShell : IAntiShell
    {
        private readonly ILogger _Logger;

        public AntiShell(ILogger logger)
        {
            _Logger=logger;
        }

        public async Task<bool> ValidateFileAsync(IFormFile _FormFile)
        {
            var _FileInfo = await GetExtentionAsync(_FormFile);
            if (_FileInfo == default)
                return false;

            string _FileExtention = _FormFile.FileName.Split('.').Last();

            if (!(_FileInfo.Extention == _FileExtention && _FileInfo.Mime.Contains(_FormFile.ContentType)))
                return false;
            else
                return true;
        }

        public async Task<(string Extention, string[] Mime)> GetExtentionAsync(IFormFile _FormFile)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException(nameof(_FormFile));

                byte[] buffer = new byte[50];
                await _FormFile.OpenReadStream().ReadAsync(buffer, 0, buffer.Length);

                string hex = "";
                foreach (var item in buffer)
                {
                    hex+= string.Format("{0:X}", item) + " ";
                }

                if (hex.StartsWith("89 50 4E 47 D A 1A A"))
                    return ("png", new string[] { "image/png" });
                else if (hex.StartsWith("FF D8"))
                    return ("jpg", new string[] { "image/jpg", "image/jpeg", "image/jpe", "image/jfif" });
                else if (hex.StartsWith("47 49 46 38 37 61") || hex.StartsWith("47 49 46 38 39 61"))
                    return ("gif", new string[] { "image/gif" });
                else if (hex.StartsWith("42 4D"))
                    return ("bmp", new string[] { "image/bmp" });
                else if (hex.StartsWith("50 4B 3 4"))
                    return ("zip", new string[] { "application/zip" });
                else if (hex.StartsWith("52 61 72 21 1A 7 0") || hex.StartsWith("52 61 72 21 1A 7 1 0"))
                    return ("rar", new string[] { "application/rar" });
                else if (hex.Contains("66 74 79 70 4D 53 4E 56") || hex.Contains("66 74 79 70 69 73 6F 6D"))
                    return ("mp4", new string[] { "video/mp4" });
                else if (hex.StartsWith("49 44 33"))
                    return ("mp3", new string[] { "audio/mp3" });
                else
                    throw new FileFormatException("File format is invalid");
            }
            catch (FileFormatException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }
    }
}
