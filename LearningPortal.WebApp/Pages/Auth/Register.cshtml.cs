using LearningPortal.Application.App.User;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Const;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _userApplication;
        private readonly IServiceProvider _serviceProvider;

        public RegisterModel(ILogger logger, IUserApplication userApplication, IServiceProvider serviceProvider, IMsgBox msgBox, ILocalizer localizer)
        {
            _logger = logger;
            _userApplication = userApplication;
            _serviceProvider = serviceProvider;
            _MsgBox=msgBox;
            _Localizer=localizer;
        }

        public IActionResult OnGet(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}/User/Index");

            ViewData["returnUrl"] = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string LangId)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_serviceProvider);
                #endregion

                if (Input.AcceptRules==false)
                    return _MsgBox.ModelStateMsg(_Localizer["YouMustbeAcceptRules"]);

                var _Result = await _userApplication.RegisterByEmailAsync(new InpRegisterByEmail
                {
                    Email=Input.Email,
                    FullName=Input.FullName,
                    Password=Input.Password
                });
                if (_Result.IsSucceeded)
                {
                    // اگر دیتا دارای مقدار باشید یعنی این حساب نیازمند تایید میباشد
                    if (_Result.Data!=null)
                    {
                        var Res = await _userApplication.ConfirmationEmailAccountAsync(new InpConfirmationEmailAccount
                        {
                            UserId=_Result.Data,
                            ConfirmationEmailLink=SiteSettingConst.SiteUrl+$"{CultureInfo.CurrentCulture.Parent.Name}/Auth/ConfirmEmail?Token=[Token]",
                            LangId=LangId
                        });

                        return _MsgBox.SuccessMsg(_Localizer[Res.Message]);
                    }
                    else
                    {
                        return _MsgBox.SuccessMsg(_Localizer[_Result.Message]);
                    }
                }
                else
                {
                    return _MsgBox.ErrorMsg(_Localizer[_Result.Message]);
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _logger.Debug(ex);
                return _MsgBox.ErrorMsg(_Localizer[ex.Message.Replace(",", "<br/>")]);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return _MsgBox.ErrorMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viRegister Input { get; set; }

    }
}
