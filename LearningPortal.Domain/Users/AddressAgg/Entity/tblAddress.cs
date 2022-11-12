using LearningPortal.Domain.Region.CitryAgg.Entity;
using LearningPortal.Domain.Region.CountryAgg.Entity;
using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;

namespace LearningPortal.Domain.Users.AddressAgg.Entity
{
    public class tblAddress : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // 450
        public Guid CountryId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid CityId { get; set; }
        public string Address { get; set; } // 500
        public string PostalCode { get; set; } // 50
        public string FirstName { get; set; } // 100
        public string LastName { get; set; } //100
        public string PhoneNumber { get; set; }// 50
        public DateTime Date { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual tblCountry tblCountry { get; set; }
        public virtual tblProvinces tblProvinces { get; set; }
        public virtual tblCities tblCities { get; set; }
    }
}
