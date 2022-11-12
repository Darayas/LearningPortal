using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LearningPortal.Framework.Services.AntiShell
{
    public interface IAntiShell
    {
        Task<(string Extention, string[] Mime)> GetExtentionAsync(IFormFile _FormFile);
        Task<bool> ValidateFileAsync(IFormFile _FormFile);
    }
}