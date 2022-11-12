using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Roles
{
    public class tblRoleConfiguration : IEntityTypeConfiguration<tblRoles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblRoles> builder)
        {
            builder.Property(u => u.ParentId).HasMaxLength(450);
            builder.Property(u => u.PageName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Description).IsRequired().HasMaxLength(300);

            builder.HasOne(a => a.tblRole_Parent)
                   .WithMany(a => a.tblRole_Childs)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
