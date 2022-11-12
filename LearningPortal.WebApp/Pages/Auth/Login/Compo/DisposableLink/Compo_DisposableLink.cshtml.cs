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

namespace LearningPortal.WebApp.Pages.Auth.Login.Compo.DisposableLink
{
    public class Compo_DisposableLinkModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public Compo_DisposableLinkModel(ILogger logger, IServiceProvider serviceProvider, IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _UserApplication=userApplication;
            _MsgBox=msgBox;
            _Localizer=localizer;
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

                var _Inp = Input.Adapt<InpLoginByDisposableLinkStep1>();
                _Inp.LangId = LangId;
                _Inp.EmailLinkTemplate = $"{SiteSettingConst.SiteUrl}{CultureInfo.CurrentCulture.Parent.Name}/DisposableLink?Token=[Token]";

                var _Result = await _UserApplication.LoginByDisposableLinkStep1Async(_Inp);
                if (!_Result.IsSucceeded)
                    return _MsgBox.ModelStateMsg(_Result.Message);

                return _MsgBox.SuccessMsg();
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.ErrorDefMsg();
            }
        }

        [BindProperty]
        public viCompo_DisposableLink Input { get; set; }
    }
}
