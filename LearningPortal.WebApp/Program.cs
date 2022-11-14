using LearningPortal.Core.Configuration;
using LearningPortal.Infrastructure.Serilog;
using LearningPortal.WebApp.Authentication;
using LearningPortal.WebApp.Config;
using LearningPortal.WebApp.Localization.Resource;
using LearningPortal.WebApp.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
WebApplication app = null;

#region Add Services
{
    //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
    //{

    //}
    //else
    //{

    //}

    builder.Host.UseSerilog_SQLServer();

    builder.Services.AddLocalization("Localization/Resource");

    builder.Services.AddAntiforgery(a => a.HeaderName = "XSRF-TOKEN");

    builder.Services.WebEncoderConfig();

    builder.Services.AddRazorPage()
                    .AddFilters()
                    .AddCustomViewLocalization("Localization/Resource")
                    .AddCustomDataAnnotationLocalization(builder.Services, typeof(SharedResource));

    builder.Services.Injection();

    builder.Services.Config();

    builder.Services.AddCustomIdentity()
        .AddErrorDescriber<CustomIdentityErrorDescriber>();

    builder.Services.AddJwtAuthentication();

    builder.Services.AddKendo();
}
#endregion

#region Configure Service
{
    app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseLocalization(new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("fa-IR") });

    app.UseJwtAuthentication();

    app.UseMiddleware<RedirectToValidLangMiddleware>();

    //app.MapRazorPages();

    app.UseEndpoints(ep =>
    {
        ep.MapRazorPages();
    });
}
#endregion

await app.RunAsync();