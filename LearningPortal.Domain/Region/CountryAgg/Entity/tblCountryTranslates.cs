using LearningPortal.Domain.Region.LanguageAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Domain.Region.CountryAgg.Entity
{
    public class tblCountryTranslates : IEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; } // Lenght: 150

        public virtual tblLanguage tblLanguage { get; set; }
        public virtual tblCountry tblCountry { get; set; }
    }
}
