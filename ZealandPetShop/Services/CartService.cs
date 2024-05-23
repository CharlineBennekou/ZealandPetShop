using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using ZealandPetShop.Migrations;
using ZealandPetShop.MockData;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Models.Order;
namespace ZealandPetShop.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor; //Bruges til at finde userId på logget ind bruger
        private readonly DbGenericService<OrderItem> _dbService; //Generic db service
        private readonly OrderService _orderService; //OrderService til at oprette eller finde ordre til vores orderitems
        

        public async Task<List<OrderItem>> GetOrderItems()
        { 
        return _dbService.GetObjectsAsync().Result.ToList();
        }

        public async Task<List<OrderItem>> GetCartItemsByUserId(int userId)
        {
            var orderItems = await _dbService.GetObjectsAsync();
            var cartItems = orderItems.Where(orderItem => orderItem.UserId == userId && orderItem.State == Order.Status.Cart).ToList(); //LINQ sortering for at finde alle orderitems som matcher userid og er i Cart state
            return cartItems;
        }


        public CartService(IHttpContextAccessor httpContextAccessor, DbGenericService<OrderItem> dbservice, OrderService orderService)
        {
            //Dependency injection
            _httpContextAccessor = httpContextAccessor;
            _dbService = dbservice;
            _orderService = orderService;
            
        }
        public async Task AddItemToCart(int itemId) //Metode til at tilføje items
        {
            // Finder UserId på bruger som er logget ind og laver det om til en int
            var customerId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            //Get Cart returnerer eksisterende cart eller laver en ny cart som den returnerer
            var cart = await GetCartAsync(customerId);

            // Tjekker om den valgte item allerede findes i brugerens cart
            var existingCartItem = (await _dbService.GetObjectsAsync())
                .FirstOrDefault(c => c.UserId == customerId && c.ItemId == itemId && c.State == Order.Status.Cart);

            if (existingCartItem != null)
            {
                // Hvis brugeren allerede har den valgte item i cart, så bliver der lagt en til quantity
                existingCartItem.Quantity++;
                await _dbService.UpdateObjectAsync(existingCartItem);
            }
            else
            {
                // Hvis den valgte item ikke findes i cart endnu, så laves der en ny orderitem
                var newCartItem = new OrderItem
                {
                    OrderId = cart.Id,
                    UserId = customerId,
                    ItemId = itemId,
                    Quantity = 1,
                    State = Order.Status.Cart
                };

                await _dbService.AddObjectAsync(newCartItem);
            
            }

        }
        private async Task<Order> GetCartAsync(int userId) //hjælpe metode til AddToCart
        {
            Order cart; 
            var orders = _orderService.GetOrdersAsync().Result.ToList(); //Henter orders vha. orderservice

            cart = orders.FirstOrDefault(o => o.UserId == userId && o.State == Order.Status.Cart); //LINQ sortering til at finde ordre med vores userid og cart state

            if (cart == null) //Hvis den ik fandt en ordre laves en ny ordre
            {
                cart = new Order { State = Order.Status.Cart, UserId = userId };
                await _orderService.CreateOrderAsync(cart);
            }

            return cart; //Returnerer enten den ordre som blev fundet vha. LINQ eller den ordre som blev skabt
        }
    }
}
