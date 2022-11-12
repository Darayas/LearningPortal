using LearningPortal.Application.Contract.ApplicationDTO.NotificationTemplate;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using LearningPortal.Domain.Settings.NotificationTemplateAgg.Contracts;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Const;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.NotificationTemplate
{
    public class NotificationTemplateApplication : INotificationTemplateApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly INotificationTemplateRepository _NotificationTemplateRepository;

        public NotificationTemplateApplication(ILogger logger, IServiceProvider serviceProvider, INotificationTemplateRepository notificationTemplateRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _NotificationTemplateRepository=notificationTemplateRepository;
        }

        public async Task<OperationResult<string>> GetTemplateAsync(InpGetTemplate Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _NotificationTemplateRepository.Get
                                                    .Where(a => a.LangId==Input.LangId.ToGuid())
                                                    .Where(a => a.Name==Input.Name)
                                                    .Select(a => a.Text)
                                                    .SingleOrDefaultAsync();

                if (qData is null)
                    return new OperationResult<string>().Failed("Name and LangId NotFound");

                #region Affect General Parameter
                {
                    qData = qData.Replace("[LogoUrl]", "")
                                 .Replace("[SiteUrl]", SiteSettingConst.SiteUrl)
                                 .Replace("[SiteName]", "")
                                 .Replace("[SiteEmail]", "")
                                 .Replace("[SitePhoneNumber]", "");
                }
                #endregion

                return new OperationResult<string>().Succeeded(qData);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }
    }
}
