using LearningPortal.Application.App.User;
using LearningPortal.Framework.Contracts;
using LearningPortal.WebApp.Authentication;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace LearningPortal.WebApp.Pages.Admin.Users
{
    [Authorize(Roles = Roles.CanManageUser)]
    public class ListUsersModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
