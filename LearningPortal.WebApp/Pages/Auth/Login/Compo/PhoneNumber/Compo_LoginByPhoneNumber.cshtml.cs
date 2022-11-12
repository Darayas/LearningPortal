using LearningPortal.Application.App.User;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.Auth.Login.Compo.PhoneNumber
{
    public class Compo_LoginByPhoneNumberModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public Compo_LoginByPhoneNumberModel(ILogger logger, IServiceProvider serviceProvider, IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
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

                var _Result = await _UserApplication.LoginByPhoneNumberStep1Async(new InpLoginByPhoneNumberStep1
                {
                    PhoneNumber = Input.PhoneNumber
                });
                if (!_Result.IsSucceeded)
                    return _MsgBox.ErrorMsg(_Localizer[_Result.Message.Replace(",", "<br/>")]);
                else
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], $"LoadOtpCompo('{Input.PhoneNumber}')");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Debug(ex);
                return _MsgBox.ErrorMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viCompo_LoginByPhoneNumber Input { get; set; }
    }
}
