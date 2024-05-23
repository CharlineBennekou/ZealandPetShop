using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Login
{
    public class CreateUserModel : PageModel
    {
        private UserService _userService;




        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        private PasswordHasher<string> passwordHasher;

        public CreateUserModel(UserService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {

        }

        public string errorMessage = "";
        private User user;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Alle felter skal udfyldes korrekt";
                return Page();
            }
            await _userService.AddUser(new User(Email, passwordHasher.HashPassword(null, Password), FirstName, LastName, Phone, Address)); ;
            await _userService.SaveUSer(user); // Antager denne metode håndterer dbContext.SaveChangesAsync()

            return RedirectToPage("Index");
        }
    }
}
