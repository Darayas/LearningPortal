using LearningPortal.Application.App.Language;
using LearningPortal.Application.Contract.ApplicationDTO.Language;
using LearningPortal.Framework.Application.Services.IPChecker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Localization
{
    public class PathRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            string path = httpContext.Request.Path;
            string cultureName = path.Trim('/').Split('/').First();

            var _languageApplication = httpContext.RequestServices.GetService<ILanguageApplication>();
            string langCode = await _languageApplication.GetCodeByAbbrAsync(new InpGetCodeByAbbr { Abbreviation = cultureName });

            if (langCode == null)
            {
                var _ipAddressChecker = httpContext.RequestServices.GetService<IIPAddressChecker>();
                var _ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
                var _countryAbbr = _ipAddressChecker.CheckIp(_ipAddress);

                if (_countryAbbr == "ir")
                {
                    langCode = "fa-IR";
                }
                else if (_countryAbbr == "us")
                {
                    langCode = "en-US";
                }
                else
                {
                    langCode = "fa-IR";
                }
            }

            return new ProviderCultureResult(langCode, langCode);
        }
    }
}
