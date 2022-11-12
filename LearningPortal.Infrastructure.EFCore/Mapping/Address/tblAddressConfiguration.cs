using LearningPortal.Domain.Region.CitryAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using LearningPortal.Domain.Region.CountryAgg.Entity;
using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Domain.Users.AddressAgg.Entity;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Address
{
    public class tblAddressConfiguration : IEntityTypeConfiguration<tblAddress>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAddress> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.UserId).IsRequired().HasMaxLength(450);
            builder.Property(l => l.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.ProvinceId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.CityId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Address).IsRequired().HasMaxLength(500);
            builder.Property(l => l.PostalCode).IsRequired().HasMaxLength(50);
            builder.Property(l => l.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(l => l.LastName).IsRequired().HasMaxLength(100);
            builder.Property(l => l.PhoneNumber).IsRequired().HasMaxLength(50);

            builder.HasOne(l => l.tblUsers)
                   .WithMany(l => l.tblAddress)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(l => l.tblCountry)
                   .WithMany(l => l.tblAddress)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.tblProvinces)
                   .WithMany(l => l.tblAddress)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.tblCities)
                   .WithMany(l => l.tblAddress)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
