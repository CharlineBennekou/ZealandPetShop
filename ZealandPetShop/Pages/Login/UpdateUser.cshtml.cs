using ItemRazorV1.Service;
using Microsoft.AspNetCore.Identity;
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
        //private UserManager<User> _userManager;



        [BindProperty]
        public User EUser { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }



        public UpdateUserModel(UserService userService/*, UserManager<User> userManager*/)
        {
            _userService = userService;
            //_userManager = userManager;
        }

        

        public async Task<IActionResult> OnGetAsync()
        {
            int userId = GetUserIdFromClaims();
            EUser = await _userService.GetUser(userId);
            if (User == null) return NotFound();

            return Page();

            //Console.WriteLine("OnGet");
            //EUser = await _userService.GetUser(id);
            //if (EUser == null)
            //{
            //    return NotFound();
            //}
            //return Page();
            //var userId = UserService.GetUser(); // Hent brugerens ID baseret på den indloggede session
            //EUser = await _userService.GetUser(int.Parse(userId));
            //if (EUser == null)
            //    return NotFound();

            //return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid) 
            //{
            //    return Page();
            //}

            await _userService.UpdateUser(EUser);
            return RedirectToPage("./LoggedInUser");


            //if (!ModelState.IsValid)
            //    return Page();

            //var userId = _userService.GetUser(id);
            //EUser.Id = int.Parse(userId);

            //var existingUser = await _userService.GetUser(EUser.Id);
            //if (existingUser == null)
            //    return NotFound("Bruger ikke fundet.");

            //existingUser.FirstName = EUser.FirstName;
            //existingUser.LastName = EUser.LastName;
            //existingUser.Email = EUser.Email;
            //existingUser.Phone = EUser.Phone;
            //// Optional: Hash new password before saving it if password field is not empty
            //if (!string.IsNullOrWhiteSpace(EUser.Password))
            //{
            //    existingUser.Password = _userService.PasswordHasher.HashPassword(existingUser, EUser.Password);
            //}

            //if (User !=null) 
            //{
            //    await _userService.UpdateUser(EUser);
            //}
            
            

            //return RedirectToPage("Profile"); // Redirect til profil siden efter opdatering
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
