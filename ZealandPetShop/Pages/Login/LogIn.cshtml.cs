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
  
        private UserService _userService;
        public LogInModel(UserService userService) //Dependency injection i constructor
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

            List<User> users = _userService._users; //henter listen af users vha. userservice
            foreach (User user in users)
            {
                if (string.Equals(Email, user.Email, StringComparison.OrdinalIgnoreCase)) //Sammenligner alle user.Email med inputted email som er case insensitiv
                {
                    var passwordHasher = new PasswordHasher<string>(); //Laver en password hasher for at kunne verify hashed passwords i users

                    if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success) 
                    {
                        

                        var claims = new List<Claim> //Laver ny liste af claims
                        { 
                            new Claim(ClaimTypes.Name, user.FirstName), //Sætter claimstype name til at være lig med users firstname
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) //Sætter nameidentifier til at være lig med en string variation af Id.

                        };

                        if (Email == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin")); //midlertidig måde at give admin brugeren "admin" rollen

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
