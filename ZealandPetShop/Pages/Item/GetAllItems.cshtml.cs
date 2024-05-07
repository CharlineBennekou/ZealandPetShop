using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Item
{
    public class GetAllItemsModel : PageModel
    {
        private ItemService _itemService;
        private CartService _cartService;
        public List<Models.Shop.Item> Items { get; set; }

        public GetAllItemsModel(ItemService itemService,CartService cartService)
        {
            _itemService = itemService;
            _cartService = cartService;
        }

        public void OnGet()
        {
            Items = _itemService.GetItems();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            Console.WriteLine("OnPostAddToCart"); //debug
            _cartService.AddItemToCart(id);
            return RedirectToPage("/Order/GetOrder");

        }

        public IActionResult OnPostSeeDetails(int id)
        {

            return RedirectToPage("/Item/ItemDetail");

        }

    }
}
