using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;
using ZealandPetShop.Services;

namespace ZealandPetShop.Pages.Order
{
    public class GetOrderModel : PageModel
    {
        //private ItemService _itemService;
        private CartService _cartService;
        public List<Models.Order.OrderItem> orderItems {  get; set; }
       
        public List<Models.Shop.Item> Items { get; set; }

        public GetOrderModel(CartService cartService)
        {
            _cartService = cartService;
        }

        public void OnGet()
        {
            Items = MockItems.GetMockItems();
            //orderItems = _cartService.GetOrderItems();
        }

    }
}
