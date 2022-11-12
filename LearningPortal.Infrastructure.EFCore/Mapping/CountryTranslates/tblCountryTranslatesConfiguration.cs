using LearningPortal.Domain.Region.CountryAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.CountryTranslates
{
    public class tblCountryTranslatesConfiguration : IEntityTypeConfiguration<tblCountryTranslates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCountryTranslates> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.LangId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Title).IsRequired().HasMaxLength(100);

            builder.HasOne(l => l.tblLanguage)
                   .WithMany(l => l.tblCountryTranslates)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.tblCountry)
                   .WithMany(l => l.tblCountryTranslates)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
