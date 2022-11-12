using LearningPortal.Application.App.Language;
using LearningPortal.Application.Contract.ApplicationDTO.Language;
using LearningPortal.Framework.Application.Services.IPChecker;
using LearningPortal.Framework.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Middlewares
{
    public class RedirectToValidLangMiddleware
    {
        private readonly RequestDelegate _next;
        public RedirectToValidLangMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                string[] Paths = context.Request.Path.HasValue ? context.Request.Path.Value.Trim('/').Split('/') : new string[] { };

                var _languageApplication = context.RequestServices.GetService<ILanguageApplication>();

                if (Paths.Any())
                {
                    string langAbbr = Paths.First();

                    if (!await _languageApplication.IsValidAbbrForSiteLangAsync(new InpIsValidAbbrForSiteLang { Abbr = langAbbr }))
                    {
                        Paths[0] = await GetLangAbbrByIpAddress(context);

                        string Url = SiteSettingConst.SiteUrl + string.Join("/", Paths);

                        if (context.Request.QueryString.HasValue)
                        {
                            Url += context.Request.QueryString.Value;
                        }

                        context.Response.StatusCode = 301;
                        context.Response.Redirect(Url);
                    }
                }
                else
                {
                    string Url = SiteSettingConst.SiteUrl + GetLangAbbrByIpAddress(context);

                    context.Response.StatusCode = 301;
                    context.Response.Redirect(Url);
                }
            }

            await _next(context);
        }
        public async Task<string> GetLangAbbrByIpAddress(HttpContext context)
        {
            var _ipAddressChecker = context.RequestServices.GetService<IIPAddressChecker>();

            var remoteIpAddress = context.Connection.RemoteIpAddress.ToString();

            var LangAbbrByIpAddress = _ipAddressChecker.GetLangAbbr(remoteIpAddress);

            if (LangAbbrByIpAddress == null)
            {
                return "fa";
            }
            else
            {
                return LangAbbrByIpAddress;
            }
        }
    }
}
