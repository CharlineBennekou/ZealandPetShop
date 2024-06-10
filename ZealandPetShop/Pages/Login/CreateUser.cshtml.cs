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
        [Required(ErrorMessage = "Alle felter skal udfyldes")]
        public string Fornavn { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Alle felter skal udfyldes")]
        public string Efternavn { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email skal udfyldes!")]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Alle felter skal udfyldes")]
        [StringLength(8)]
        public string Telefon { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Alle felter skal udfyldes")]
        public string Addresse { get; set; }


        [BindProperty, DataType(DataType.Password)]
        [Required(ErrorMessage = "Alle felter skal udfyldes")]
        public string Password { get; set; }

        private PasswordHasher<string> passwordHasher;

        //Konstruktør
        //UserService initialliseres ved hjælp af depency injektion
        public CreateUserModel(UserService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {

        }

        public string errorMessage = "";

        
/// <summary>
        /// OnPostasyns-metoden håndtårer POST-andmodinger. Den udfører følgende trin:
        /// Metoden kontrollere først om modeltilstanden er gyldig. Hvis den ikke er gyldig, sætters en fejlmeddelelse ("Alle felter skal udfyldes korrekt") og siden retuneres med fejlmeddelelsen.
        /// Tilføjelse af User: Hvis modeltilstanden er gyldig, opretter metoden en ny bruger ved hjælp af UserService. Brugeren oprettes med hash-kode for adganskoden samt øverrige brugeroplysinger fra properties.
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

            if (await _userService.EmailInUseAsync(Email))
            {
                errorMessage = "Email er allerede i brug. Indtast venligst en anden email addresse.";
                return Page();
            }

            await _userService.AddUser(new User(Email, passwordHasher.HashPassword(null, Password), Fornavn, Efternavn, Telefon, Addresse)); ;
            return RedirectToPage("/Index");
        }
    }
}
 
