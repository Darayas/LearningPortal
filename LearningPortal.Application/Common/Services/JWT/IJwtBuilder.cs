using LearningPortal.Application.Contract.ApplicationDTO.Result;
using System.Threading.Tasks;

namespace LearningPortal.Application.Common.Services.JWT
{
    public interface IJwtBuilder
    {
        Task<OperationResult<string>> GenerateTokenAsync(string UserId);
    }
}