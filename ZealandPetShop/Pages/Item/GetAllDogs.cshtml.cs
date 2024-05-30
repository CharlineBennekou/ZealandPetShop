using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Item
{
    public class GetAllDogsModel : PageModel
    {
        private ItemService _itemService; // Variabel til at håndtere item-relaterede operationer
        private CartService _cartService; // Variabel til at håndtere indkøbskurvsoperationer

        public List<Models.Shop.Item> Items { get; set; } // Liste over items vist på siden

        public GetAllDogsModel(ItemService itemService, CartService cartService)
        {
            _itemService = itemService; // Initialiserer ItemService for databehandling
            _cartService = cartService; // Initialiserer CartService for indkøbskurvshåndtering
        }

        // Håndterer HTTP GET-anmodning for at hente alle items
        public void OnGet()
        {
            Items = _itemService.GetItems(); // Henter alle items fra databasen
        }

        // Håndterer HTTP GET-anmodning for at sortere items efter pris i stigende rækkefølge
        public IActionResult OnGetSortByPrice()
        {
            Items = _itemService.SortByPrice().ToList(); // Sorterer items efter pris i stigende rækkefølge
            return Page(); // Returnerer siden med sorteret items
        }

        // Håndterer HTTP GET-anmodning for at sortere items efter pris i faldende rækkefølge
        public IActionResult OnGetSortByPriceDescending()
        {
            Items = _itemService.SortByPriceDescending().ToList(); // Sorterer items efter pris i faldende rækkefølge
            return Page(); // Returnerer siden med sorteret items
        }

        // Håndterer HTTP POST-anmodning for at tilføje en item til indkøbskurven
        public IActionResult OnPostAddToCart(int id)
        {
            _cartService.AddItemToCart(id); // Tilføjer valgt item til indkøbskurven
            return RedirectToPage("/Order/GetOrder"); // Omdirigerer til ordresiden
        }

        // Håndterer HTTP POST-anmodning for at se detaljer om en bestemt item
        public IActionResult OnPostSeeDetails(int id)
        {
            return RedirectToPage("/Item/ItemDetail"); // Omdirigerer til item-detaljesiden
        }

        [BindProperty]
        public string SearchString { get; set; } // Indeholder den indtastede søgestreng

        // Håndterer HTTP POST-anmodning for at udføre en søgning efter items
        public IActionResult OnPostSearch()
        {
            Items = _itemService.Search(SearchString).ToList(); // Søger efter items baseret på søgestrengen
            return Page(); // Returnerer siden med søgeresultater
        }
    }
}
