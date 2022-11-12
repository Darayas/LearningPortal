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

namespace LearningPortal.WebApp.Pages.Auth.Login.Compo.PhoneNumber
{
    public class Compo_LoginByPhoneNumberOTPModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IJwtBuilder _JwtBuilder;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public Compo_LoginByPhoneNumberOTPModel(ILogger logger, IServiceProvider serviceProvider, IMsgBox msgBox, IUserApplication userApplication, ILocalizer localizer, IJwtBuilder jwtBuilder)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _MsgBox = msgBox;
            _UserApplication = userApplication;
            _Localizer = localizer;
            _JwtBuilder = jwtBuilder;
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
                    var _Result = await _UserApplication.LoginByPhoneNumberStep2Async(new InpLoginByPhoneNumberStep2() { PhoneNumber = Input.PhoneNumber, OTPCode = Input.OTPCode });
                    if (_Result.IsSucceeded == false)
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
                return _MsgBox.ErrorMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.ErrorMsg(_Localizer["Error500"]);
            }
        }

        public async Task<IActionResult> OnPostResendAsync(viResendCompo_LoginByPhoneNumberOTP Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _UserApplication.ResendOTPSmsCodeAsync(new InpResendOTPSmsCode { PhoneNumber = Input.PhoneNumber });
                if (!_Result.IsSucceeded)
                    return _MsgBox.ErrorMsg(_Localizer[_Result.Message]);

                return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "StartTimer()");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return _MsgBox.ErrorMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.ErrorMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_LoginByPhoneNumberOTP Input { get; set; }
    }
}
