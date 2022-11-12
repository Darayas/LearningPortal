using LearningPortal.Application.Contract.ApplicationDTO.Files;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.Files
{
    public interface IFileApplication
    {
        Task<OperationResult> RemoveFileAsync(InpRemoveFile Input);
        Task<OperationResult<string>> UploadFileAsync(InpUploadFile Input);
        Task<OperationResult<string>> UploadProfileImageAsync(InpUploadProfileImage Input);
    }
}