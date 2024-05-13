using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;
using ZealandPetShop.Services;
using ZealandPetShop.Models.Shop;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace ZealandPetShop.Pages.Order
{
    public class GetOrderModel : PageModel
    {
        private ItemService _itemService;
        private CartService _cartService;
        public List<Models.Order.OrderItem> OrderItems {  get; set; }
       
        public List<Models.Shop.Item> Items { get; set; }

        public GetOrderModel(CartService cartService, ItemService itemService)
        {
            _cartService = cartService;
            _itemService = itemService;
        }

        public async Task OnGet()
        {
            int UserId;
            Items = _itemService.GetItems();
            OrderItems = await _cartService.GetOrderItems();

        }

        public Models.Shop.Item GetItemById(int itemId)
        {
            return _itemService.GetItem(itemId);
        }
    }

}

