using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Login
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            //Console.WriteLine("???");
            //if (LogInModel.LoggedInUser == null)
            //{
            //    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //    return RedirectToPage("/Index");
            //}
            return RedirectToPage("/Index");

        }
        public IActionResult OnPost()
        {
            Console.WriteLine("???");
            if (LogInModel.LoggedInUser != null)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index");
        }
    }
}
