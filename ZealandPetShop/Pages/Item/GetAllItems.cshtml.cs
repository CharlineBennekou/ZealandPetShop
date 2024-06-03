using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using ZealandPetShop.MockData;
using ZealandPetShop.Services;
using System.Security.Claims;

namespace ZealandPetShop.Pages.Item
{
    public class GetAllItemsModel : PageModel
    {
        private ItemService _itemService; // Service til håndtering af item-relaterede operationer.
        private CartService _cartService; // Service til håndtering af indkøbskurv.
        public List<Models.Shop.Item> Items { get; set; } // Liste af items der vises på siden.

        // Konstruktor der initialiserer services.
        public GetAllItemsModel(ItemService itemService, CartService cartService)
        {
            _itemService = itemService;
            _cartService = cartService;
        }

        // Metode der hentes når siden loades.
        public void OnGet()
        {
            // Henter alle items fra item service.
            Items = _itemService.GetItems();
        }

        // Metode til at sortere items efter pris i stigende rækkefølge.
        public IActionResult OnGetSortByPrice()
        {
            // Sorterer items efter pris og opdaterer listen.
            Items = _itemService.SortByPrice().ToList();
            return Page(); // Returnerer den opdaterede side.
        }

        // Metode til at sortere items efter pris i faldende rækkefølge.
        public IActionResult OnGetSortByPriceDescending()
        {
            // Sorterer items efter pris (faldende) og opdaterer listen.
            Items = _itemService.SortByPriceDescending().ToList();
            return Page(); // Returnerer den opdaterede side.
        }


        // Metode til at tilføje et item til indkøbskurven.
        public async Task<IActionResult> OnPostAddToCart(int id)
        {
            Console.WriteLine("OnPostAddToCart"); // Debugging output.                                    
          await  _cartService.AddItemToCart(id);  // Tilføjer item til indkøbskurven via cart service.
            // Redirecter til ordre siden.
            return RedirectToPage("/Order/GetOrder");
        }

        // Metode til at se detaljer om et item.
        public IActionResult OnPostSeeDetails(int id)
        {
            // Redirecter til item detalje siden.
            return RedirectToPage("/Item/ItemDetail");
        }

        [BindProperty]
        public string SearchString { get; set; } // Bind property for søgestrengen.

        // Metode til at søge efter items baseret på en søgestreng.
        public IActionResult OnPostSearch()
        {
            // Søger efter items der matcher søgestrengen og opdaterer listen.
            Items = _itemService.Search(SearchString).ToList();
            return Page(); // Returnerer den opdaterede side.
        }
    }
}
