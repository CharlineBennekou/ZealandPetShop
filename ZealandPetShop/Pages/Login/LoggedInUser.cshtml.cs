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

        public LoggedInUserModel(UserService userService)
        {
            _userService = userService;
        }

        public async Task <IActionResult> OnGetAsync(int id)
        {

            User = await _userService.GetUser(id);
            if (User == null) 
            { 
                return NotFound();
            }
            return Page();
           

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

    }
}
