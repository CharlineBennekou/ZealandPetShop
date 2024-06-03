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
        private ItemService _itemService; // Service til h�ndtering af item-relaterede operationer.
        private CartService _cartService; // Service til h�ndtering af indk�bskurv.
        public List<Models.Shop.Item> Items { get; set; } // Liste af items der vises p� siden.

        // Konstruktor der initialiserer services.
        public GetAllItemsModel(ItemService itemService, CartService cartService)
        {
            _itemService = itemService;
            _cartService = cartService;
        }

        // Metode der hentes n�r siden loades.
        public void OnGet()
        {
            // Henter alle items fra item service.
            Items = _itemService.GetItems();
        }

        // Metode til at sortere items efter pris i stigende r�kkef�lge.
        public IActionResult OnGetSortByPrice()
        {
            // Sorterer items efter pris og opdaterer listen.
            Items = _itemService.SortByPrice().ToList();
            return Page(); // Returnerer den opdaterede side.
        }

        // Metode til at sortere items efter pris i faldende r�kkef�lge.
        public IActionResult OnGetSortByPriceDescending()
        {
            // Sorterer items efter pris (faldende) og opdaterer listen.
            Items = _itemService.SortByPriceDescending().ToList();
            return Page(); // Returnerer den opdaterede side.
        }


        // Metode til at tilf�je et item til indk�bskurven.
        public async Task<IActionResult> OnPostAddToCart(int id)
        {
            Console.WriteLine("OnPostAddToCart"); // Debugging output.                                    
          await  _cartService.AddItemToCart(id);  // Tilf�jer item til indk�bskurven via cart service.
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
        public string SearchString { get; set; } // Bind property for s�gestrengen.

        // Metode til at s�ge efter items baseret p� en s�gestreng.
        public IActionResult OnPostSearch()
        {
            // S�ger efter items der matcher s�gestrengen og opdaterer listen.
            Items = _itemService.Search(SearchString).ToList();
            return Page(); // Returnerer den opdaterede side.
        }
    }
}
