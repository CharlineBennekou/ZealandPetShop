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

        
        public User user { get; set; }

        public LoggedInUserModel(UserService userService)
        {
            _userService = userService;
        }

		// Bruger async og returner Task<IActionResult> for korrekt asynkron håndtering
		public async Task<IActionResult> OnGetAsync(int id)
		{
			var user = await _userService.GetUser(id);  // Antager, at GetUser er en asynkron metode

			if (User == null)
			{
				return NotFound();  // Returnerer en 404 side, hvis ingen bruger findes
			}

			return Page();
		}


        [BindProperty]
        public User User { get; set; }

      
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _userService.UpdateUser(User);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {

                // Log exception or handle errors
                ModelState.AddModelError(string.Empty, "Der opstod en fejl under opdatering af profilen.");
                return Page();
            }

           /* return RedirectToPage("./Index"); */ // Redirect to a confirmation page or back to the profile
        }
    }
}


		//public List<Models.Login.User> _user { get; set; }


		//{

		//    int id = int.Parse(id);

		//    return id;
		//}






		//public GetUser(UserService userService)
		//{
		//    _userService = userService;
		//}


		//public IActionResult OnGet(int id)
		//{
		//    Console.WriteLine("OnGet");
		//    _user = _userService.GetUser(id);
		//    if (_user == null)
		//    {
		//        return NotFound();
		//    }
		//    return Page();
		//}

	
