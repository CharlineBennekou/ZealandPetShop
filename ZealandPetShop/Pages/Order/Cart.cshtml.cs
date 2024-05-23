using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;
using ZealandPetShop.Services;
using ZealandPetShop.Models.Shop;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ZealandPetShop.Pages.Order
{
    public class CartModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor; //bruges til at få fat i userId
        private ItemService _itemService; //bruges til at få fat i alle items
        private CartService _cartService; //bruges til at få fat i kundens orderitems i cart
        public List<Models.Order.OrderItem> OrderItems {  get; set; }
       
        public List<Models.Shop.Item> Items { get; set; }

        public CartModel(CartService cartService, ItemService itemService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _itemService = itemService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnGet()
        {
            //Finder logget ind bruger's userid
            int userid = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Items = _itemService.GetItems(); //Henter alle items
            OrderItems = await _cartService.GetCartItemsByUserId(userid); //henter alle cart items vha. userid

        }

        public Models.Shop.Item GetItemById(int itemId) //_itemService.GetItem(); er privat, så vi laver den her metode for at bruge den i html
        {
            return _itemService.GetItem(itemId);
        }
    }

}

