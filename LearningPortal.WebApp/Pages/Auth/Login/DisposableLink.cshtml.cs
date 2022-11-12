using LearningPortal.Application.App.User;
using LearningPortal.Application.Common.Services.JWT;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.WebApp.Common.ExMethods;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.Auth.Login
{
    public class DisposableLinkModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IJwtBuilder _JwtBuilder;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public DisposableLinkModel(ILogger logger, IServiceProvider serviceProvider, IUserApplication userApplication, ILocalizer localizer, IJwtBuilder jwtBuilder)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _UserApplication=userApplication;
            _Localizer=localizer;
            _JwtBuilder=jwtBuilder;
        }

        public async Task<IActionResult> OnGetAsync(viDisposableLink Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Login
                string UserId;
                {
                    var _Result = await _UserApplication.LoginByDisposableLinkStep2Async(Input.Adapt<InpLoginByDisposableLinkStep2>());
                    if (!_Result.IsSucceeded)
                    {
                        Message=_Localizer[_Result.Message];
                        return Page();
                    }
                    else
                        UserId = _Result.Data;
                }
                #endregion

                #region Generated JWToken
                string _JWToken;
                {
                    var _Result = await _JwtBuilder.GenerateTokenAsync(UserId);
                    if (!_Result.IsSucceeded)
                    {
                        Message = _Localizer[_Result.Message];
                        return Page();
                    }
                    else
                        _JWToken= _Result.Data;
                }
                #endregion

                #region Create Auth Cookie
                {
                    Response.CreateAuthCookie(_JWToken, true);
                }
                #endregion

                return Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}/User/Index");
            }
            catch (ArgumentInvalidException ex)
            {
                Message = ex.Message;

                return Page();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                Message=_Localizer["Error500"];

                return Page();
            }
        }

        public string Message { get; set; }
    }
}
