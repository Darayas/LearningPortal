using LearningPortal.Domain.Settings.NotificationTemplateAgg.Entities;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.NotificationTemplate
{
    public class tblNotificationTemplateConfiguration : IEntityTypeConfiguration<tblNotificationTemplates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblNotificationTemplates> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.LangId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Text).IsRequired();

            builder.HasOne(a => a.tblLanguage)
                   .WithMany(a => a.tblNotificationTemplates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
