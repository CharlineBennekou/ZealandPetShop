using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Login
{
    public class DeleteUserModel : PageModel
    {
        private UserService _userService;
        //private UserManager<User> _userManager;



        [BindProperty]
        public User EUser { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }



        public DeleteUserModel(UserService userService/*, UserManager<User> userManager*/)
        {
            _userService = userService;
            //_userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EUser = await _userService.GetUser(id);
            if (User == null) return NotFound();

            return Page();


        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _userService.DeleteUser(EUser);

            return RedirectToPage("/Logout");
        }
    }
}
