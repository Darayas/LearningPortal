using LearningPortal.Domain.FileServers.FileAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Files
{
    public class tblFilesConfiguration : IEntityTypeConfiguration<tblFiles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFiles> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.FilePathId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.UserId).IsRequired(false).HasMaxLength(450);
            builder.Property(l => l.Title).IsRequired().HasMaxLength(100);
            builder.Property(l => l.FileName).IsRequired().HasMaxLength(150);
            builder.Property(l => l.MimeType).IsRequired().HasMaxLength(50);
            builder.Property(l => l.FileMetaData).IsRequired().HasMaxLength(600);

            builder.HasOne(a => a.tblFilePaths)
                   .WithMany(a => a.tblFiles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FilePathId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblFiles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
