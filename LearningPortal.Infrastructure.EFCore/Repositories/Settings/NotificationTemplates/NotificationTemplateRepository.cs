using LearningPortal.Domain.Settings.NotificationTemplateAgg.Contracts;
using LearningPortal.Domain.Settings.NotificationTemplateAgg.Entities;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Settings.NotificationTemplates
{
    public class NotificationTemplateRepository : BaseRepository<tblNotificationTemplates>, INotificationTemplateRepository
    {
        public NotificationTemplateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
