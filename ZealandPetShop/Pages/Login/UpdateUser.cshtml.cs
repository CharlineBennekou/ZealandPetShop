using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            int userId = GetUserIdFromClaims();
            EUser = await _userService.GetUser(id);
            if (User == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid) 
            //{
            //return Page();
            //}

            await _userService.UpdateUser(EUser);

            return RedirectToPage("/LoggedInUser");



        }



        private int GetUserIdFromClaims()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new InvalidOperationException("No claim found for user ID.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new InvalidOperationException("Invalid user ID claim.");
            }

            return userId;
        }

    }
}
