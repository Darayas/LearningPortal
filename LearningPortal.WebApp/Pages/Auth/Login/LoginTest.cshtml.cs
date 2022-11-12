using LearningPortal.Application.App.User;
using LearningPortal.Application.Common.Services.JWT;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Framework.Contracts;
using LearningPortal.WebApp.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.Auth.Login
{
    public class LoginTestModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IJwtBuilder _JwtBuilder;
        private readonly IUserApplication _UserApplication;
        public LoginTestModel(IJwtBuilder jwtBuilder, ILogger logger, IUserApplication userApplication)
        {
            _JwtBuilder=jwtBuilder;
            _Logger=logger;
            _UserApplication=userApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var _Result = await _UserApplication.GetIdByPhoneNumberAsync(new InpGetIdByPhoneNumber { PhoneNumber="09010112829" });
                if (_Result.IsSucceeded == false)
                    return StatusCode(400);

                var _Token = await _JwtBuilder.GenerateTokenAsync(_Result.Data);
                if (_Token.IsSucceeded == false)
                    return StatusCode(400);

                Response.CreateAuthCookie(_Token.Data, true);

                return Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}/User/Index");
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }
    }
}
