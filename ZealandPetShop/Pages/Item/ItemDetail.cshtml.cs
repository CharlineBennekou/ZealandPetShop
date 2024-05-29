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
        private ItemService _itemService; // Service til håndtering af item-relaterede operationer.

        // Bind property til at binde og validere mængden af produktet der ønskes.
        [BindProperty]
        [Required(ErrorMessage = "Indtast venligst mængden af produktet der ønskes")]
        public int Count { get; set; }

        public Models.Shop.Item Item { get; set; } // Property til at holde det aktuelle item.

        // Konstruktor der initialiserer ItemService.
        public ItemDetailModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        // Metode der hentes når siden loades.
        public IActionResult OnGet(int Id)
        {
            Console.WriteLine("OnGet"); // Debugging output.
                                        // Henter item baseret på Id fra ItemService.
            Item = _itemService.GetItem(Id);
            if (Item == null)
            {
                // Returnerer en 404-fejl hvis item ikke findes.
                return NotFound();
            }
            // Returnerer den opdaterede side med item detaljer.
            return Page();
        }
    }
}
