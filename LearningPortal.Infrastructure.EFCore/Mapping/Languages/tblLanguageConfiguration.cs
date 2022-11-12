using LearningPortal.Domain.Region.LanguageAgg.Entities;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Languages
{
    public class tblLanguageConfiguration : IEntityTypeConfiguration<tblLanguage>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblLanguage> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Code).IsRequired().HasMaxLength(50);
            builder.Property(l => l.Abbr).IsRequired().HasMaxLength(50);
            builder.Property(l => l.NativeName).IsRequired().HasMaxLength(100);
        }
    }
}
