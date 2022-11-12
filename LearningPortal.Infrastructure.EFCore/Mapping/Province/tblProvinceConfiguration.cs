using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Province
{
    public class tblProvinceConfiguration : IEntityTypeConfiguration<tblProvinces>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProvinces> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblCountry)
                   .WithMany(a => a.tblProvinces)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}