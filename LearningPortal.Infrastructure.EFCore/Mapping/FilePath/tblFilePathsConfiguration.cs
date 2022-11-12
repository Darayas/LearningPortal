using LearningPortal.Domain.FileServers.FilePathAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.FilePath
{
    public class tblFilePathsConfiguration : IEntityTypeConfiguration<tblFilePaths>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFilePaths> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.FileServerId).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Path).IsRequired().HasMaxLength(300);

            builder.HasOne(a => a.tblFileServer)
                   .WithMany(a => a.tblFilePaths)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileServerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
