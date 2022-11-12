#region Application & System Usings 
using LearningPortal.Application.App.AccessLevels;
using LearningPortal.Application.App.Address;
using LearningPortal.Application.App.Cities;
using LearningPortal.Application.App.Country;
using LearningPortal.Application.App.FilePath;
using LearningPortal.Application.App.Files;
using LearningPortal.Application.App.FileServer;
using LearningPortal.Application.App.Language;
using LearningPortal.Application.App.NotificationTemplate;
using LearningPortal.Application.App.Province;
using LearningPortal.Application.App.Roles;
using LearningPortal.Application.App.User;
using LearningPortal.Application.Common.ExMethods;
using LearningPortal.Application.Common.Services.JWT;
using LearningPortal.Domain.FileServers.FileAgg.Contratcs;
using LearningPortal.Domain.FileServers.FilePathAgg.Contracts;
using LearningPortal.Domain.FileServers.ServerAgg.Contracts;
using LearningPortal.Domain.Region.CitryAgg.Contracts;
using LearningPortal.Domain.Region.CountryAgg.Contracts;
using LearningPortal.Domain.Region.LanguageAgg.Contract;
using LearningPortal.Domain.Region.ProvinceAgg.Contracts;
using LearningPortal.Domain.Settings.NotificationTemplateAgg.Contracts;
using LearningPortal.Domain.Users.AccessLevelAgg.Contracts;
using LearningPortal.Domain.Users.AddressAgg.Contracts;
using LearningPortal.Domain.Users.RoleAgg.Contracts;
using LearningPortal.Domain.Users.UserAgg.Contracts;
using LearningPortal.Framework.Application.Services.FTP;
using LearningPortal.Framework.Application.Services.IPChecker;
using LearningPortal.Framework.Application.Services.IpList;
using LearningPortal.Framework.Common.Utilities.Downloader;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Services.AntiShell;
using LearningPortal.Framework.Services.Email;
using LearningPortal.Framework.Services.Sms;
using LearningPortal.Infrastructure.EFCore.Context;
using LearningPortal.Infrastructure.EFCore.Context.ConnStr;
using LearningPortal.Infrastructure.EFCore.Repositories.AccessLevels;
using LearningPortal.Infrastructure.EFCore.Repositories.Address;
using LearningPortal.Infrastructure.EFCore.Repositories.Cities;
using LearningPortal.Infrastructure.EFCore.Repositories.CityTranslates;
using LearningPortal.Infrastructure.EFCore.Repositories.Country;
using LearningPortal.Infrastructure.EFCore.Repositories.CountryTranslates;
using LearningPortal.Infrastructure.EFCore.Repositories.FilePath;
using LearningPortal.Infrastructure.EFCore.Repositories.Files;
using LearningPortal.Infrastructure.EFCore.Repositories.FileServers;
using LearningPortal.Infrastructure.EFCore.Repositories.Language;
using LearningPortal.Infrastructure.EFCore.Repositories.Province;
using LearningPortal.Infrastructure.EFCore.Repositories.ProvinceTranslates;
using LearningPortal.Infrastructure.EFCore.Repositories.Roles;
using LearningPortal.Infrastructure.EFCore.Repositories.Settings.NotificationTemplates;
using LearningPortal.Infrastructure.EFCore.Repositories.User;
using LearningPortal.Infrastructure.Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace LearningPortal.Core.Configuration
{
    public static class Bootstrapper
    {
        public static void Config(this IServiceCollection services)
        {
            #region Service
            {
                services.AddDbContext<MainContext>(opt => opt.UseSqlServer(ConnectionString.GetConnectionString()));
                services.AddCustomAutoMapper();

                services.AddSingleton<ILogger, Serilogger>();
                services.AddSingleton<IIPList, IPList>();
                services.AddSingleton<IIPAddressChecker, IPAddressChecker>();
                services.AddSingleton<IEmailSender, DnlnSender>();
                services.AddSingleton<ISmsSender, KaveNegarSmsSender>();
                services.AddSingleton<IDownloader, Downloader>();
                services.AddScoped<IJwtBuilder, JwtBuilder>();
                services.AddScoped<IAntiShell, AntiShell>();
                services.AddScoped<IFtpWorker, FtpWorker>();
            }
            #endregion 

            #region Repository
            {
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ILanguageRepository, LanguageRepository>();
                services.AddScoped<IAccessLevelsRepository, AccessLevelsRepository>();
                services.AddScoped<INotificationTemplateRepository, NotificationTemplateRepository>();
                services.AddScoped<IRoleRepository, RoleRepository>();
                services.AddScoped<IAccessLevelRoleRepository, AccessLevelRoleRepository>();
                services.AddScoped<IUserRoleRepository, UserRoleRepository>();
                services.AddScoped<IFileServerRepository, FileServerRepository>();
                services.AddScoped<IFilePathRepository, FilePathRepository>();
                services.AddScoped<IFileRepository, FileRepository>();
                services.AddScoped<ICountryRepository, CountryRepository>();
                services.AddScoped<IProvinceRepository, ProvinceRepository>();
                services.AddScoped<ICityRepository, CityRepository>();
                services.AddScoped<IAddressRepository, AddressRepository>();
                services.AddScoped<ICountryTranslatesRepository, CountryTranslatesRepository>();
                services.AddScoped<IProvinceTranslatesRepository, ProvinceTranslatesRepository>();
                services.AddScoped<ICityTranslatesRepository, CityTranslatesRepository>();
            }
            #endregion

            #region Application
            {
                services.AddScoped<IUserApplication, UserApplication>();
                services.AddScoped<ILanguageApplication, LanguageApplication>();
                services.AddScoped<IAccessLevelsApplication, AccessLevelsApplication>();
                services.AddScoped<INotificationTemplateApplication, NotificationTemplateApplication>();
                services.AddScoped<IRoleApplication, RoleApplication>();
                services.AddScoped<IFileServerApplication, FileServerApplication>();
                services.AddScoped<IFilePathApplication, FilePathApplication>();
                services.AddScoped<IFileApplication, FileApplication>();
                services.AddScoped<ICountryApplication, CountryApplication>();
                services.AddScoped<IProvinceApplication, ProvinceApplication>();
                services.AddScoped<ICityApplication, CityApplication>();
                services.AddScoped<IAddressApplication, AddressApplication>();
            }
            #endregion
        }
    }
}
