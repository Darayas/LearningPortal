using LearningPortal.Domain.Users.AccessLevelAgg.Entities;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.AccessLevels
{
    public class tblAccessLevelRolesConfiguration : IEntityTypeConfiguration<tblAccessLevelRoles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAccessLevelRoles> builder)
        {
            builder.HasKey(al => al.Id);
            builder.Property(al => al.Id).IsRequired().HasMaxLength(150);
            builder.Property(al => al.AccessLevelId).IsRequired().HasMaxLength(150);
            builder.Property(al => al.RoleId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblAccessLevels)
                   .WithMany(a => a.tblAccessLevelRoles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.AccessLevelId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblRoles)
                   .WithMany(a => a.tblAccessLevelRoles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
