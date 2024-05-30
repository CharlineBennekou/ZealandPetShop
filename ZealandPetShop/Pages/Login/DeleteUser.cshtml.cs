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

        [BindProperty]
        public User EUser { get; set; }

        [BindProperty]
        public string Email { get; set; }

        //[BindProperty, DataType(DataType.Password)]
        //public string Password { get; set; }



        public DeleteUserModel(UserService userService/*, UserManager<User> userManager*/)
        {
            _userService = userService;
            //_userManager = userManager;
        }


        /// <summary>
        /// Henter brugeren med det angivende Id med GetUser(int Id)
        /// Hvis brugen ikke findes, retunere den NotFound ellers retunere den siden hvis breugern findes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            EUser = await _userService.GetUser(id);
            if (User == null) return NotFound();

            return Page();

        }


        /// <summary>
        /// 1. Tjekker om EUser er null eller om EUser.Id er mindre end eller lig med 0.
        /// 2.Hvis valideringen af EUserId fejler, returneres der en BadRequest exception respons med meddelelsen (Invalid user data)
        /// 3. hvis EUser.Id er den rette id, prøver DeleteUser-metoden at slette brugeren når metoden er kaldt på fra _userService.
        /// 4. Hvis der opstår en Exception under metoden, fanges exceptioen og en BadRequest-respons returneres med en (exception message)
        /// 5. Hvis brugen slettes uden undtagelser, omdirigeres der til siden.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            //Validere om brugerdata er den rette
            if (EUser == null || EUser.Id <= 0)
            {
                
                return BadRequest("Invalid user data");
            }
            try
            {
                await _userService.DeleteUser(EUser);
            }
            catch (Exception ex)
            {
                // Log exception
                return BadRequest(ex.Message);
            }
            return RedirectToPage("./Item/GetAllItems");
        }
    }
}

