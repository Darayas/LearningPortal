using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Roles
{
    public class tblUserRoleConfiguration : IEntityTypeConfiguration<tblUserRoles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblUserRoles> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(u => u.Id).IsRequired().HasMaxLength(450);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblUserRoles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblRoles)
                   .WithMany(a => a.tblUserRoles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
