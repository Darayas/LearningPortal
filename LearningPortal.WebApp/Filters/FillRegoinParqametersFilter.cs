using LearningPortal.Application.App.Language;
using LearningPortal.Application.Contract.ApplicationDTO.Language;
using LearningPortal.Framework.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Filters
{
    public class FillRegoinParametersFilter : IAsyncPageFilter
    {
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var _Logger = context.HttpContext.RequestServices.GetService<ILogger>();
            var _LanguageApplication = context.HttpContext.RequestServices.GetService<ILanguageApplication>();
            string LangCode = CultureInfo.CurrentCulture.Name;

            #region Get LangId By LangCode
            string _LangId;
            {
                var _Result = await _LanguageApplication.GetIdByLangCode(new InpGetIdByLangCode { LangCode=LangCode });
                if (_Result.IsSucceeded==false)
                {
                    _Logger.Fatal(_Result.Message);
                    context.HttpContext.Response.Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}/Error500");
                }

                _LangId=_Result.Data;
            }
            #endregion

            context.HandlerArguments.Add("LangId", _LangId);

            await next();
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {

        }
    }
}
