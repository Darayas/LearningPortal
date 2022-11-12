using LearningPortal.Domain.FileServers.FileAgg.Entity;
using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Domain.Users.AddressAgg.Entity;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace LearningPortal.Domain.Region.CountryAgg.Entity
{
    public class tblCountry : IEntity
    {
        public Guid Id { get; set; }
        public Guid FlagImgId { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public bool IsActive { get; set; }

        public virtual tblFiles tblFiles { get; set; }
        public virtual ICollection<tblCountryTranslates> tblCountryTranslates { get; set; }
        public virtual ICollection<tblProvinces> tblProvinces { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }

    }
}
