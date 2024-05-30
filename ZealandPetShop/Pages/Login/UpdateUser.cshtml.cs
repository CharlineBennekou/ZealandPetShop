using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims; // Giver adgang til brugerens claims
using ZealandPetShop.Models.Login;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Login
{
    public class UpdateUserModel : PageModel
    {
       
        private UserService _userService;

        //Binder brugerdata til UI
        //(At forbinde data fra modelklassen til brugergrænsefladen(UI)).
        [BindProperty]
        public User EUser { get; set; }

        // Binder password data til UI med en datavalidering
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        // Konstruktor for at initialisere UserService
        public UpdateUserModel(UserService userService)
        {
            _userService = userService;
        }

        // Metode, der kaldes ved GET-forespøregsler
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Henter brugerdata asynkront ved hjælp af UserService
            EUser = await _userService.GetUser(id);
            //Hvis brugeren ikke findes, returner NotFound
            if (User == null) return NotFound();

            // Returnerer Page visning
            return Page();
        }

        // Metode, der kaldes ved POST-forespørgelser
        public async Task<IActionResult> OnPostAsync()
        {
            await _userService.UpdateUser(EUser);

            // Returnerer Page visning
            return Page();
        }


      
    }
}
