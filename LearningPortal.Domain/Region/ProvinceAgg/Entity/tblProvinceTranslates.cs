using LearningPortal.Domain.Region.LanguageAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Domain.Region.ProvinceAgg.Entity
{
    public class tblProvinceTranslates : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblProvinces tblProvinces { get; set; }
        public virtual tblLanguage tblLanguage { get; set; }
    }
}
