using LearningPortal.Application.App.User;
using LearningPortal.Application.Common.Services.JWT;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Const;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.WebApp.Common.ExMethods;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.User.Profile
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;
        private readonly IJwtBuilder _JwtBuilder;

        public EditProfileModel(ILogger logger, IMsgBox msgBox, ILocalizer localizer, IServiceProvider serviceProvider, IUserApplication userApplication, IJwtBuilder jwtBuilder)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _UserApplication=userApplication;
            _JwtBuilder=jwtBuilder;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                string _UserId = User.GetUserDetails().UserId;

                var _Result = await _UserApplication.GetUserForEditProfileAsync(new InpGetUserForEditProfile { UserId=_UserId });
                if (!_Result.IsSucceeded)
                    return StatusCode(500);

                Input = _Result.Data.Adapt<viEditProfile>();

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> OnPostAsync(string LangId)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _UserApplication.SaveEditProfileAsync(new InpSaveEditProfile
                {
                    UserId=User.GetUserDetails().UserId,
                    Bio=Input.Bio,
                    Email=Input.Email,
                    FullName=Input.FullName,
                    Image = Input.Image,
                    LangId = LangId,
                    ChangeEmailLink = SiteSettingConst.SiteUrl + $"{CultureInfo.CurrentCulture.Parent.Name}/Auth/ChangeEmail?Token=[Token]",
                });

                if (_Result.IsSucceeded)
                {
                    #region Renew Token
                    {
                        var _Token = await _JwtBuilder.GenerateTokenAsync(User.GetUserDetails().UserId);

                        Response.CreateAuthCookie(_Token.Data, true);
                    }
                    #endregion

                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "ReloadPage()");
                }
                else
                    return _MsgBox.ErrorMsg(_Localizer[_Result.Message]);
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
        public viEditProfile Input { get; set; }
    }
}
