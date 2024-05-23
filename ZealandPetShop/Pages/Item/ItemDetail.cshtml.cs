using ZealandPetShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ZealandPetShop.Models.Shop;
using ItemRazorV1.Service;
using ZealandPetShop.Models;

namespace ZealandPetShop.Pages.Item
{
    public class ItemDetailModel : PageModel
    {
        private ItemService _itemService;

        [BindProperty]
        [Required(ErrorMessage = "Indtast venligst mængden af produktet der ønskes")]
        public int Count { get; set; }

        public Models.Shop.Item Item { get; set; }


        public ItemDetailModel(ItemService itemService)
        { 
            _itemService = itemService;
        }


        public IActionResult OnGet(int Id)
        {
            
            Console.WriteLine("OnGet");
            Item = _itemService.GetItem(Id);
            if (Item == null)
            {
                return NotFound();
            }
            return Page();
           
        }


    }
}
