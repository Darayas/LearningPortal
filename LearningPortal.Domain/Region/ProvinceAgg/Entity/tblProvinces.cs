using LearningPortal.Domain.Region.CitryAgg.Entity;
using LearningPortal.Domain.Region.CountryAgg.Entity;
using LearningPortal.Domain.Users.AddressAgg.Entity;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace LearningPortal.Domain.Region.ProvinceAgg.Entity
{
    public class tblProvinces : IEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual tblCountry tblCountry { get; set; }
        public virtual ICollection<tblProvinceTranslates> tblProvinceTranslates { get; set; }
        public virtual ICollection<tblCities> tblCities { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }

    }
}
