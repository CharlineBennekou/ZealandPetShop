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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbGenericService<OrderItem> _dbService;
        private readonly OrderService _orderService;
        private List<OrderItem> _orderitems;

        public async Task<List<OrderItem>> GetOrderItems()
        { 
        return _dbService.GetObjectsAsync().Result.ToList();
        }

        public async Task<List<OrderItem>> GetCartItemsByUserId(int userId)
        {
            var orderItems = await _dbService.GetObjectsAsync();
            var cartItems = orderItems.Where(orderItem => orderItem.UserId == userId && orderItem.State == Order.Status.Cart).ToList();
            return cartItems;
        }


        public CartService(IHttpContextAccessor httpContextAccessor, DbGenericService<OrderItem> dbservice, OrderService orderService)
        {
            
            _httpContextAccessor = httpContextAccessor;
            _dbService = dbservice;
            //_dbService.SaveObjects(_orderitems);
            _orderService = orderService;
            _orderitems = _dbService.GetObjectsAsync().Result.ToList();
        }
        public async Task AddItemToCart(int itemId)
        {
            // Get the current user's ID from the HttpContext
            var customerId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            //Get Cart returnerer cart eller laver en cart
            var cart = await GetCartAsync(customerId);

            // Check if the item already exists in the user's cart
            var existingCartItem = (await _dbService.GetObjectsAsync())
                .FirstOrDefault(c => c.UserId == customerId && c.ItemId == itemId && c.State == Order.Status.Cart);

            if (existingCartItem != null)
            {
                // If the item already exists, increment its quantity
                existingCartItem.Quantity++;
                await _dbService.UpdateObjectAsync(existingCartItem);
            }
            else
            {
                // If the item doesn't exist, create a new cart item
                var newCartItem = new OrderItem
                {
                    OrderId = cart.Id,
                    UserId = customerId,
                    ItemId = itemId,
                    Quantity = 1, // Start with quantity 1
                    State = Order.Status.Cart
                };

                await _dbService.AddObjectAsync(newCartItem);
                _orderitems.Add(newCartItem);
            }

        }
        private async Task<Order> GetCartAsync(int userId)
        {
            Order cart;
            // Retrieve the user's cart or create one if it doesn't exist
            var orders = _orderService.GetOrdersAsync().Result.ToList();
            if (orders == null)
                {
                cart = new Order { State = Order.Status.Cart, UserId = userId };
                await _orderService.CreateOrderAsync(cart);
                return cart;

            }
            cart = orders.FirstOrDefault(o => o.UserId == userId && o.State == Order.Status.Cart);

            if (cart == null)
            {
                cart = new Order { State = Order.Status.Cart, UserId = userId };
                await _orderService.CreateOrderAsync(cart);
            }

            return cart;
        }
    }
}
