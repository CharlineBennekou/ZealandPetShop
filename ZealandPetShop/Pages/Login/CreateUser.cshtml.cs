using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
		public Models.Login.User user { get; set; }


		public IActionResult OnGet()
        {
           return Page();
        }

        public string errorMessage = "";

        public async Task <IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid) 
            {
                 errorMessage= "Alle felter skal udfyldes";
                return Page();
            }
            _userService.AddUser(user);
            _userService.SaveUSer(user).Wait();

            return RedirectToPage("/Item/GetAllItems");
        }
    }
}
