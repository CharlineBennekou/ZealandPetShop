using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Login
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            HttpContext.SignOutAsync("MyCookieAuthenticationScheme"); //Bruger metode til at logge ud
            return RedirectToPage("/Index");
        }
    }
}
