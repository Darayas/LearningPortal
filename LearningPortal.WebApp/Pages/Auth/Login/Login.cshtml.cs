using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace LearningPortal.WebApp.Pages.Auth.Login
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet(string ReturnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}/User/Index");

            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Index";

            return Page();
        }
    }
}
