using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.ProvinceTranslates
{
    //public virtual tblLanguage tblLanguage { get; set; }
    public class tblProvinceTranslatesConfiguration : IEntityTypeConfiguration<tblProvinceTranslates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProvinceTranslates> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.ProvinceId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.LangId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Title).IsRequired().HasMaxLength(100);

            builder.HasOne(l => l.tblProvinces)
                   .WithMany(l => l.tblProvinceTranslates)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.tblLanguage)
                   .WithMany(l => l.tblProvinceTranslates)
                   .HasPrincipalKey(l => l.Id)
                   .HasForeignKey(l => l.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}