using LearningPortal.Application.Contract.ApplicationDTO.NotificationTemplate;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.NotificationTemplate
{
    public interface INotificationTemplateApplication
    {
        Task<OperationResult<string>> GetTemplateAsync(InpGetTemplate Input);
    }
}