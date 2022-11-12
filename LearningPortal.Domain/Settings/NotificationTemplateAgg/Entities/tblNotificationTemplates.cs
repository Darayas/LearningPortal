using LearningPortal.Domain.Region.LanguageAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;

namespace LearningPortal.Domain.Settings.NotificationTemplateAgg.Entities
{
    public class tblNotificationTemplates : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public virtual tblLanguage tblLanguage { get; set; }
    }
}
