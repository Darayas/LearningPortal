using LearningPortal.Framework.Contracts;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using LearningPortal.WebApp.Filters;
using LearningPortal.WebApp.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace LearningPortal.WebApp.Config
{
    public static class StartUpEx
    {
        public static IServiceCollection AddLocalization(this IServiceCollection services, string resourcePath)
        {
            return services.AddLocalization(a => a.ResourcesPath = resourcePath);
        }
        public static IMvcBuilder AddCustomViewLocalization(this IMvcBuilder builder, string resourcePath)
        {
            return builder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, a => a.ResourcesPath = resourcePath);
        }
        public static IMvcBuilder AddRazorPage(this IServiceCollection services)
        {
            return services.AddRazorPages(a =>
            {
                a.Conventions.AddPageRoute("/home/index", "");
            });
        }
        public static IMvcBuilder AddCustomDataAnnotationLocalization(this IMvcBuilder builder, IServiceCollection services, Type sharedResource)
        {
            builder.AddDataAnnotationsLocalization(opt =>
            {
                var localizer = new FactoryLocalizer().Set(services, sharedResource);

                opt.DataAnnotationLocalizerProvider = (t, f) => localizer;
            });

            return builder;
        }
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app, List<CultureInfo> supportedLangs, string defaultCultureName = "fa-IR")
        {
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(defaultCultureName),
                SupportedCultures = supportedLangs,
                SupportedUICultures = supportedLangs,
                RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    //new CookieRequestCultureProvider(),
                    //new QueryStringRequestCultureProvider()
                    new PathRequestCultureProvider()
                }
            };

            return app.UseRequestLocalization(options);
        }
        public static void Injection(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizer, Localizer>();
            services.AddSingleton<IMsgBox, MsgBox>();
        }
        public static IMvcBuilder AddFilters(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(opt =>
            {
                opt.Filters.Add(new FillRegoinParametersFilter());
            });
        }
        public static IServiceCollection WebEncoderConfig(this IServiceCollection services)
        {
            return services.Configure<WebEncoderOptions>(opt =>
            {
                opt.TextEncoderSettings= new TextEncoderSettings(UnicodeRanges.Arabic, UnicodeRanges.BasicLatin);
            });
        }
    }
}
