using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims; // Giver adgang til brugerens claims
using ZealandPetShop.Models.Login;
using ZealandPetShop.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            // Henter bruger-ID fra krav (claims)
            int userId = GetUserIdFromClaims();
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


        // Privat metode til at hente bruger-ID fra krav (claims)
        private int GetUserIdFromClaims()
        {
            // Henter brugerens claims identity (som er Id)
            var claimsIdentity = User.Identity as ClaimsIdentity;
            // Henter bruger-ID claim
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            // Hvis bruger-ID claim er null, kaster en undtagelse
            if (userIdClaim == null)
            {
                // Forsøger at parse bruger-ID til en int
                throw new InvalidOperationException("No claim found for user ID.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new InvalidOperationException("Invalid user ID claim.");
            }
            // Returnerer bruger-ID
            return userId;
        }

    }
}
