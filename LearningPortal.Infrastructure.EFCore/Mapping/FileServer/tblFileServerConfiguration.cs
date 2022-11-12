using LearningPortal.Domain.FileServers.ServerAgg.Entity;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.FileServer
{
    public class tblFileServerConfiguration : IEntityTypeConfiguration<tblFileServers>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFileServers> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(200);
            builder.Property(l => l.Description).IsRequired().HasMaxLength(500);
            builder.Property(l => l.HttpDomin).IsRequired().HasMaxLength(100);
            builder.Property(l => l.HttpPath).IsRequired().HasMaxLength(300);
            builder.Property(l => l.FtpData).IsRequired();
        }
    }
}
