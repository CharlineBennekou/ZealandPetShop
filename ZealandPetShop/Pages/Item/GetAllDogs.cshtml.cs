using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Item
{
    public class GetAllDogsModel : PageModel
    {
        private ItemService _itemService; // Variabel til at h�ndtere item-relaterede operationer
        private CartService _cartService; // Variabel til at h�ndtere indk�bskurvsoperationer

        public List<Models.Shop.Item> Items { get; set; } // Liste over items vist p� siden

        public GetAllDogsModel(ItemService itemService, CartService cartService)
        {
            _itemService = itemService; // Initialiserer ItemService for databehandling
            _cartService = cartService; // Initialiserer CartService for indk�bskurvsh�ndtering
        }

        // H�ndterer HTTP GET-anmodning for at hente alle items
        public void OnGet()
        {
            Items = _itemService.GetItems(); // Henter alle items fra databasen
        }

        // H�ndterer HTTP GET-anmodning for at sortere items efter pris i stigende r�kkef�lge
        public IActionResult OnGetSortByPrice()
        {
            Items = _itemService.SortByPrice().ToList(); // Sorterer items efter pris i stigende r�kkef�lge
            return Page(); // Returnerer siden med sorteret items
        }

        // H�ndterer HTTP GET-anmodning for at sortere items efter pris i faldende r�kkef�lge
        public IActionResult OnGetSortByPriceDescending()
        {
            Items = _itemService.SortByPriceDescending().ToList(); // Sorterer items efter pris i faldende r�kkef�lge
            return Page(); // Returnerer siden med sorteret items
        }

        // H�ndterer HTTP POST-anmodning for at tilf�je en item til indk�bskurven
        public IActionResult OnPostAddToCart(int id)
        {
            _cartService.AddItemToCart(id); // Tilf�jer valgt item til indk�bskurven
            return RedirectToPage("/Order/GetOrder"); // Omdirigerer til ordresiden
        }

        // H�ndterer HTTP POST-anmodning for at se detaljer om en bestemt item
        public IActionResult OnPostSeeDetails(int id)
        {
            return RedirectToPage("/Item/ItemDetail"); // Omdirigerer til item-detaljesiden
        }

        [BindProperty]
        public string SearchString { get; set; } // Indeholder den indtastede s�gestreng

        // H�ndterer HTTP POST-anmodning for at udf�re en s�gning efter items
        public IActionResult OnPostSearch()
        {
            Items = _itemService.Search(SearchString).ToList(); // S�ger efter items baseret p� s�gestrengen
            return Page(); // Returnerer siden med s�geresultater
        }
    }
}
