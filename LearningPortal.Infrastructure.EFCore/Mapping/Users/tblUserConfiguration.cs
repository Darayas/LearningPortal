using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPortal.Infrastructure.EFCore.Mapping.Users
{
    public class tblUserConfiguration : IEntityTypeConfiguration<tblUsers>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblUsers> builder)
        {
            builder.Property(u => u.ProfileImgId).IsRequired(false).HasMaxLength(150);
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(150);
            builder.Property(u => u.SmsHashCode).IsRequired(false).HasMaxLength(100);
            builder.Property(u => u.Bio).IsRequired(false).HasMaxLength(250);

            builder.HasOne(u => u.tblAccessLevels)
                   .WithMany(u => u.tblUsers)
                   .HasPrincipalKey(u => u.Id)
                   .HasForeignKey(u => u.AccessLevelId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.tblProfileImg)
                   .WithMany(u => u.tblUserImg)
                   .HasPrincipalKey(u => u.Id)
                   .HasForeignKey(u => u.ProfileImgId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
