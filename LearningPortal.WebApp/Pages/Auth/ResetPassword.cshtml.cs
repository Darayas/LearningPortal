using LearningPortal.Application.App.User;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.Auth
{
    public class ResetPasswordModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public ResetPasswordModel(ILogger logger, IMsgBox msgBox, ILocalizer localizer, IServiceProvider serviceProvider, IUserApplication userApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _UserApplication=userApplication;
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

                var _Result = await _UserApplication.RecoveryPasswordStep2Async(Input.Adapt<InpRecoveryPasswordStep2>());
                if (!_Result.IsSucceeded)
                    return _MsgBox.ErrorMsg(_Result.Message);

                return _MsgBox.SuccessMsg(_Result.Message,"GotoLoginPage()");
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ErrorMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.ErrorDefMsg();
            }
        }

        [BindProperty(SupportsGet = true)]
        public viResetPassword Input { get; set; }
    }
}
