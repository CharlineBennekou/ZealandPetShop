using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Item
{
    public class DeleteItemModel : PageModel
    {
        private ItemService _itemService;

        [BindProperty]
        public Models.Shop.Item Item { get; set; } // Egenskab til at holde det produkt, der skal slettes

        public DeleteItemModel(ItemService itemService)
        {
            _itemService = itemService; // Initialiserer ItemService for at kunne udføre operationer på produkter
        }

        // HTTP GET-metode til at hente produktet, der skal slettes
        public IActionResult OnGet(int Id)
        {
            Console.WriteLine("OnGet");
            Item = _itemService.GetItem(Id); // Henter det specifikke produkt baseret på dets ID
            if (Item == null)
            {
                return NotFound(); // Returnerer NotFound-resultat, hvis produktet ikke findes
            }
            return Page(); // Returnerer siden til visning af oplysninger om produktet, der skal slettes
        }

        // HTTP POST-metode til at slette produktet
        public IActionResult OnPost()
        {
            Models.Shop.Item deletedItem = _itemService.DeleteItem(Item.Id); // Sletter det valgte produkt baseret på dets ID
            if (deletedItem == null)
            {
                return NotFound(); // Returnerer NotFound-resultat, hvis produktet ikke kunne slettes
            }
            return RedirectToPage("/Item/GetAllItems"); // Omdirigerer til siden med alle produkter efter sletning
        }
    }
}
