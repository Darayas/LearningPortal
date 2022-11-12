using LearningPortal.Domain.Users.AccessLevelAgg.Entities;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.AccessLevels
{
    public class tblAccessLevelsConfiguration : IEntityTypeConfiguration<tblAccessLevels>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAccessLevels> builder)
        {
            builder.HasKey(al => al.Id);
            builder.Property(al => al.Id).IsRequired().HasMaxLength(150);
            builder.Property(al => al.Name).IsRequired().HasMaxLength(100);
        }
    }
}
