using LearningPortal.Domain.Region.CitryAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.CityTranslates
{
    public class tblCityTranslatesConfiguration : IEntityTypeConfiguration<tblCityTranslates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCityTranslates> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.CityId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.LangId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Title).IsRequired().HasMaxLength(100);

            builder.HasOne(l => l.tblCities)
                   .WithMany(l => l.tblCityTranslates)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.tblLanguage)
                   .WithMany(l => l.tblCityTranslates)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}