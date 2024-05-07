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
        //private IPasswordHasher<User> _passwordHasher;

        public LogInModel(UserService userService/*, IPasswordHasher<User> passwordHasher*/)
        {
            _userService = userService;
            //_passwordHasher = passwordHasher;

        }
        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }




        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddRazorPages();
        //    // Registrer PasswordHasher som en service
        //    services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        //}


        public void OnGet()
        {

        }




        public async Task<IActionResult> OnPost()
        {

            //List<User> users = _userService._users;
            var users = await _userService.GetAllUsersAsync();

            if (users == null)
            {
                Message = "Ingen brugere fundet.";
                return Page();
            }

            foreach (User user in users)
            {
                //if (_passwordHasher == null)
                //{
                //    throw new InvalidOperationException("Password hasher is not initialized.");
                //}

                // Hent alle brugere fra databasen via den generiske service
                //var users = await _userService.GetAllUsersAsync();

                // Tjek for null for at undgå NullReferenceException
                if (users == null)
                {
                    Message = "Ingen brugere fundet.";
                    return Page();
                }

                if (string.Equals(Email, user.Email, StringComparison.OrdinalIgnoreCase))
                {
                    var passwordHasher = new PasswordHasher<string>();

                    if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                    {
                        

                        var claims = new List<Claim>
                        { 
                            new Claim(ClaimTypes.Name, user.FirstName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

                        };

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

        //public async Task<IActionResult> OnGetAsync(int userId)
        //{
        //    var user = await _userService.GetUser(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    // Fortsæt med at bruge 'user' objektet
        //    return Page();
        //}
    }
}
