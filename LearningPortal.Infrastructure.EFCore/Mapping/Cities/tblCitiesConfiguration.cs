using LearningPortal.Domain.Region.CitryAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Cities
{
    public class tblCitiesConfiguration : IEntityTypeConfiguration<tblCities>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCities> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.ProvinceId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(40);

            builder.HasOne(l => l.tblProvinces)
                   .WithMany(l => l.tblCities)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}