using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Domain.Users.AddressAgg.Entity;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Domain.Region.CitryAgg.Entity
{
    public class tblCities : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual tblProvinces tblProvinces { get; set; }
        public virtual ICollection<tblCityTranslates> tblCityTranslates { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }

    }
}
