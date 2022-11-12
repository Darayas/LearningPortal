using LearningPortal.WebApp.Common.ExMethods;
using LearningPortal.WebApp.Common.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningPortal.WebApp.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            Response.Logout();

            return new JSResult("location.reload()");
        }
    }
}
