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

        public IActionResult OnGet(int id)
        {


            user = _userService.GetUser(id);

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