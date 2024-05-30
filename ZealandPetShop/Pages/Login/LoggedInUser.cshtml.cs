using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Login
{
    public class LoggedInUserModel : PageModel
    {

        private UserService _userService;

        [BindProperty]
        public User User { get; set; }


        //Kontruktør til at intialisere UserService
        public LoggedInUserModel(UserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// 1. (public async Task<IActionResult> OnGetAsync(int id)), er en asynkron metode, 
        /// der håndtere GET-andmodninger for at hente brugerdata baseret på brugerens Id
        /// 2. (User = await _userService.GetUser(id);) Henter brugeren med det andgivende Id og genner i _userService
        /// 3. (if(User == null {return NotFound}): Hvis brugeren ikke findes, returnere den NotFound.
        /// 4. Hvis brugeren findes, returnere siden: (return Page();)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task <IActionResult> OnGetAsync(int id)
        {

            User = await _userService.GetUser(id);
            if (User == null) 
            { 
                return NotFound();
            }
            return Page();
           

        }


        

    }
}
