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



        /// <summary>
        /// Properties for user skal opfylde at logge ind
        /// </summary>
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

        //Konstruktør
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


        /// <summary>
        /// OnPostasyns-metoden håndtårer POST-andmodinger. Den udfører følgende trin:
        /// Metoden kontrollere først om modeltilstanden er gyldig. Hvis den ikke er gyldig, sætters en fejlmeddelelse ("Alle felter skal udfyldes korrekt") og siden retuneres med fejlmeddelelsen.
        /// Tilføjelse af User: Hvis modeltilstanden er gyldig, opretter metoden en ny bruger ved hjælp af UserService. Brugeren oprettes med hash-kode for adganskoden samt øverrige brugeroplysinger fra properties.
        /// Gemme User: Efter at have tilføjet bruger gemmer metoden brugeren i databasen (userService.SaveUser(user).
        /// Omdirigering: Til sidst omdigere metoden brugeren til index-siden, hvis brugeroprettelsen er gennemført.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Alle felter skal udfyldes korrekt";
                return Page();
            }
            await _userService.AddUser(new User(Email, passwordHasher.HashPassword(null, Password), FirstName, LastName, Phone, Address)); ;
            /*await _userService.SaveUSer(user);*/ // Antager denne metode håndterer dbContext.SaveChangesAsync()

            return RedirectToPage("/Index");
        }
    }
}
