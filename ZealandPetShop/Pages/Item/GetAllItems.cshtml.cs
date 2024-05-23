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

        public async Task OnPostAddToCartAsync(int id)
        {
            Items = _itemService.GetItems();
            await _cartService.AddItemToCart(id);

            RedirectToPage("/Order/Cart");
        }

 
        //public IActionResult OnPostAddToCart(int id)
        //{
        //    _cartService.AddItemToCart(id);
        //    return RedirectToPage("/Order/Cart");


        //}

        public IActionResult OnPostSeeDetails(int id)
        {

            return RedirectToPage("/Item/ItemDetail");

        }

    }
}
