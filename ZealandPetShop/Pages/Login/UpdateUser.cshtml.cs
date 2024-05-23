using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Login
{
    public class UpdateUserModel : PageModel
    {
        private UserService _userService;

        [BindProperty] 
        public User EUser { get; set; }


        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }


        public UpdateUserModel(UserService userService)
        {
            _userService = userService;
        }

       public async Task <IActionResult> OnGetAsync()
        {
            //int userId = GetUserIdFromClaims();
            EUser = await _userService.GetUser(EUser.Id);
            if (User == null) return NotFound();

            return Page();
        }



    }
}
