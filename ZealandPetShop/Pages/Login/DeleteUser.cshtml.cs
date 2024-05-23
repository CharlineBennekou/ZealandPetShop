using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Login
{
    public class DeleteUserModel : PageModel
    {
        public UserService UserService { get; set; }

        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {
            
        }



        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await UserService.DeleteUser(User);
                return RedirectToPage("./Item/GetAllItems"); // Omdiriger til en liste over brugere efter sletning
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred deleting the user.");
                return Page();
            }
        }

    }
}
