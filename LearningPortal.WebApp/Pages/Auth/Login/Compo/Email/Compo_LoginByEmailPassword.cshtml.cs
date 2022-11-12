using LearningPortal.Application.App.User;
using LearningPortal.Application.Common.Services.JWT;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.WebApp.Common.ExMethods;
using LearningPortal.WebApp.Common.Types;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ILogger = LearningPortal.Framework.Contracts.ILogger;

namespace LearningPortal.WebApp.Pages.Auth.Login.Compo.Email
{
    public class Compo_LoginByEmailPasswordModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IJwtBuilder _JwtBuilder;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public Compo_LoginByEmailPasswordModel(ILogger logger, IMsgBox msgBox, IServiceProvider serviceProvider, IUserApplication userApplication, ILocalizer localizer, IJwtBuilder jwtBuilder)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _ServiceProvider=serviceProvider;
            _UserApplication=userApplication;
            _Localizer=localizer;
            _JwtBuilder=jwtBuilder;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Login
                string _UserId = null;
                {
                    var _Result = await _UserApplication.LoginByEmailPasswordAsync(new InpLoginByEmailPassword() { Email=Input.Email, Password=Input.Password });
                    if (_Result.IsSucceeded==false)
                        return _MsgBox.ErrorMsg(_Result.Message.Replace(",", "<br/>"));

                    _UserId = _Result.Data;
                }
                #endregion

                #region Generate Token
                string _GeneratedToken = null;
                {
                    var _Result = await _JwtBuilder.GenerateTokenAsync(_UserId);
                    if (!_Result.IsSucceeded)
                        return _MsgBox.ErrorMsg(_Result.Message.Replace(",", "<br/>"));

                    _GeneratedToken = _Result.Data;
                }
                #endregion

                #region Create Cookie
                {
                    Response.CreateAuthCookie(_GeneratedToken, true);
                }
                #endregion

                return new JSResult("GotoReturnUrl()");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.ErrorMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viCompo_LoginByEmailPassword Input { get; set; }
    }
}
