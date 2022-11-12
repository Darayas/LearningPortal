using LearningPortal.Domain.FileServers.ServerAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningPortal.Domain.Region.CountryAgg.Entity;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Country
{
    public class tblCountryConfiguration : IEntityTypeConfiguration<tblCountry>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCountry> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.FlagImgId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(150);
            builder.Property(l => l.PhoneCode).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblCountry)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FlagImgId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
