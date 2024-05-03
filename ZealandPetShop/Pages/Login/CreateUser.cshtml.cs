using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Login
{
    public class CreateUserModel : PageModel
    {

		private UserService _userService;


		public CreateUserModel(UserService userService)
		{
			_userService = userService;
		}

		[BindProperty]
		public User user { get; set; }


		public IActionResult OnGet()
		{
			return Page();
		}

		public string errorMessage = "";

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				errorMessage = "Alle felter skal udfyldes korrekt";
				return Page();
			}
			await _userService.AddUser(user);
			await _userService.SaveUSer(user); // Antager denne metode håndterer dbContext.SaveChangesAsync()

			return RedirectToPage("Index");
		}
	}

}

