using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ZealandPetShop.Services;
using ZealandPetShop.Models.Login;

namespace ZealandPetShop.Pages.Login
{
    public class LogInModel : PageModel
    {
        public static User LoggedInUser { get; set; } = null;

        private UserService _userService;
        public LogInModel(UserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {

            List<User> users = _userService._users;
            foreach (User user in users)
            {
                if (string.Equals(Email, user.Email, StringComparison.OrdinalIgnoreCase))
                {
                    var passwordHasher = new PasswordHasher<string>();

                    if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                    {
                        LoggedInUser = user;

                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.FirstName) };

                        if (Email == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/index");
                    }
                }
            }
            Message = "Invalid attempt";
            return Page();

        }
    }
}
