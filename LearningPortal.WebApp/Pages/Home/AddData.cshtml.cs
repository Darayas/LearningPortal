using LearningPortal.Infrastructure.EFCore.Data.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.Home
{
    public class AddDataModel : PageModel
    {
        public async Task OnGet()
        {
            await new MainData().RunAsync();
        }
    }
}
