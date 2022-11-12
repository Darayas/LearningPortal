using LearningPortal.Application.App.User;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Const;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.Auth.Login.Compo.Email
{
    public class Compo_RecoveryEmailPasswordModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public Compo_RecoveryEmailPasswordModel(ILogger logger, IMsgBox msgBox, ILocalizer localizer, IServiceProvider serviceProvider, IUserApplication userApplication)
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

        public async Task<IActionResult> OnPostAsync(string LangId)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Inp = Input.Adapt<InpRecoveryPasswordStep1>();
                _Inp.LangId = LangId;
                _Inp.RecoveryPageUrl = $"{SiteSettingConst.SiteUrl}{CultureInfo.CurrentCulture.Parent.Name}/ResetPassword?Token=[Token]";

                var _Result = await _UserApplication.RecoveryPasswordStep1Async(_Inp);
                if (!_Result.IsSucceeded)
                    return _MsgBox.ErrorMsg(_Localizer[_Result.Message]);

                return _MsgBox.SuccessMsg(_Localizer["RecoveryEmailWasSent"]);
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
        public viCompo_RecoveryEmailPassword Input { get; set; }
    }
}
