using LearningPortal.Application.Contract.ApplicationDTO.FileServer;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.FileServer
{
    public interface IFileServerApplication
    {
        Task<OperationResult<string>> GetBestServerIdAsync(InpGetBestServerId Input);
        Task<OperationResult<OutGetServerDetails>> GetServerDetailsAsync(InpGetServerDetails Input);
    }
}