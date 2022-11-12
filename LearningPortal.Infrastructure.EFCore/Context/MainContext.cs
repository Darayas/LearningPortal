using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using LearningPortal.Infrastructure.EFCore.Common.ExMethods;
using LearningPortal.Infrastructure.EFCore.Context.ConnStr;
using LearningPortal.Infrastructure.EFCore.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace LearningPortal.Infrastructure.EFCore.Context
{
    public class MainContext : IdentityDbContext<tblUsers, tblRoles, Guid, IdentityUserClaim<Guid>, tblUserRoles, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public MainContext(DbContextOptions options)
                    : base(options)
        {
        }
        public MainContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(tblUserConfiguration);
            base.OnModelCreating(builder);

            var entitesAssemblies = typeof(IEntity).Assembly;
            builder.RegisterAllEntities<IEntity>(entitesAssemblies);

            var entitiesConfAssembly = typeof(IEntityConf).Assembly;
            builder.RegisterEntityTypeConfiguration(entitiesConfAssembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.GetConnectionString());
        }
    }
}
