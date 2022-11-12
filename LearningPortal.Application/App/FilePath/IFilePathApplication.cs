using LearningPortal.Application.Contract.ApplicationDTO.FilePath;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.FilePath
{
    public interface IFilePathApplication
    {
        Task<string> GetDirectoryPathByPathIdAsync(InpGetDirectoryPathByPathId Input);
        Task<string> GetProfileImagePathIdAsync(InpGetProfileImagePathId Input);
    }
}