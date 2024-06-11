using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;
using ZealandPetShop.Services;
using ZealandPetShop.Models.Shop;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ZealandPetShop.Models.Order;
using ZealandPetShop.Models.Login;

namespace ZealandPetShop.Pages.Order
{
    public class CartModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor; //bruges til at få fat i userId
        private ItemService _itemService; //bruges til at få fat i alle items
        private CartService _cartService; //bruges til at få fat i kundens orderitems i cart
        private OrderService _orderService; //Bruges til at få fat i kundens order
        public List<Models.Order.OrderItem> OrderItems {  get; set; }
       
        public List<Models.Shop.Item> Items { get; set; }
        public Models.Order.Order Order { get; set; }

        public CartModel(CartService cartService, ItemService itemService, IHttpContextAccessor httpContextAccessor, OrderService orderService)
        {
            _cartService = cartService;
            _itemService = itemService;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
        }

        public async Task OnGet()
        {
            //Finder logget ind bruger's userid
            int userid = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Items = _itemService.GetItems(); //Henter alle items
            Order = _orderService.GetOrderCartByUserIdAsync(userid).Result;
            OrderItems = _cartService.GetOrderItemsByOrderIdAsync(Order.Id).Result;


        }

        public async Task<IActionResult> OnPostDeleteOrderItem(int id)
        {
           await _cartService.DeleteOrderItemAsync(id);

            //Vi bliver nødt til at assign vores Items, order og orderitems med data før vi redirecter, da der ellers kommer null exception.
            int userid = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Items = _itemService.GetItems(); //Henter alle items
            Order = _orderService.GetOrderCartByUserIdAsync(userid).Result;
            OrderItems = _cartService.GetOrderItemsByOrderIdAsync(Order.Id).Result;
            return Page();

        }
        public async Task<IActionResult> OnPostBestil(int id) //id på kundens order
        {
            _orderService.MarkAsOrdered(id); //metode fra orderService
            return Page();
        }
 

        public Models.Shop.Item GetItemById(int itemId) //_itemService.GetItem(); er privat, så vi laver den her metode for at bruge den i html
        {
            return _itemService.GetItem(itemId);
        }
    }

}

