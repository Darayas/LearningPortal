using LearningPortal.Application.Contract.ApplicationDTO.AccessLevel;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.AccessLevels
{
    public interface IAccessLevelsApplication
    {
        Task<OperationResult<string>> GetIdByNameAsync(InpGetIdByName Input);
    }
}
