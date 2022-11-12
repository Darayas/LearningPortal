using LearningPortal.Application.App.User;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using LearningPortal.Framework.Common.ExMethod;

namespace LearningPortal.WebApp.Pages.Auth
{
    public class ChangeEmailModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;

        public ChangeEmailModel(ILogger logger, IServiceProvider serviceProvider, IUserApplication userApplication, ILocalizer localizer)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _UserApplication=userApplication;
            _Localizer=localizer;
        }

        public async Task<IActionResult> OnGetAsync(viChangeEmail Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _UserApplication.ChangeEmailAsync(new InpChangeEmail
                {
                    Token=Input.Token
                });

                if (_Result.IsSucceeded==false)
                    State= new ChangeEmailModelState
                    {
                        Type="danger",
                        Message= _Result.Message.Replace(",", "<br/>")
                    };
                else
                    State= new ChangeEmailModelState
                    {
                        Type="success",
                        Message= _Localizer[_Result.Message]
                    };

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                State= new ChangeEmailModelState
                {
                    Type="danger",
                    Message= ex.Message
                };

                return Page();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                State= new ChangeEmailModelState
                {
                    Type="danger",
                    Message= _Localizer["Error500"]
                };

                return Page();
            }
        }

        public ChangeEmailModelState State { get; set; }
    }

    public class ChangeEmailModelState
    {
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
