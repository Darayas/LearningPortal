using LearningPortal.Domain.Region.LanguageAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;

namespace LearningPortal.Domain.Region.CitryAgg.Entity
{
    public class tblCityTranslates : IEntity
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblCities tblCities { get; set; }
        public virtual tblLanguage tblLanguage { get; set; }
    }
}
