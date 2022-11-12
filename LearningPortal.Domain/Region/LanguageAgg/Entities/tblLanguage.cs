using LearningPortal.Domain.Region.CitryAgg.Entity;
using LearningPortal.Domain.Region.CountryAgg.Entity;
using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Domain.Settings.NotificationTemplateAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace LearningPortal.Domain.Region.LanguageAgg.Entities
{
    public class tblLanguage : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbr { get; set; }
        public string NativeName { get; set; }
        public bool IsRtl { get; set; }
        public bool IsActive { get; set; }
        public bool UseForSiteLanguage { get; set; }

        public virtual ICollection<tblNotificationTemplates> tblNotificationTemplates { get; set; }
        public virtual ICollection<tblCountryTranslates> tblCountryTranslates { get; set; }
        public virtual ICollection<tblProvinceTranslates> tblProvinceTranslates { get; set; }
        public virtual ICollection<tblCityTranslates> tblCityTranslates { get; set; }


    }
}
