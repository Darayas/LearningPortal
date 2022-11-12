using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Infrastructure.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LearningPortal.Core.Configuration
{
    public static class IdentityConfig
    {
        public static IdentityBuilder AddCustomIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<tblUsers, tblRoles>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
                opt.SignIn.RequireConfirmedAccount = false;

                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                opt.User.RequireUniqueEmail = false;

                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<MainContext>()
            .AddDefaultTokenProviders();
        }
    }
}
