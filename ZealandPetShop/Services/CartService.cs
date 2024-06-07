using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using ZealandPetShop.EFDbContext;
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
        return _dbService.GetAllObjectsAsync().Result.ToList();
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            IEnumerable<OrderItem> orderItems = await _dbService.GetAllObjectsAsync();
            List<OrderItem> orderItemsByOrderId = orderItems
                .Where(orderitem => orderitem.OrderId == orderId)
                .ToList();

            return orderItemsByOrderId;
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
            int customerId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            //Get Cart returnerer eksisterende cart eller laver en ny cart som den returnerer
            Order cart = await _orderService.GetOrderCartByUserIdAsync(customerId);

            // Tjekker om den valgte item allerede findes i brugerens cart
            OrderItem existingCartItem = (
                await _dbService.GetAllObjectsAsync()).
                FirstOrDefault(c => c.OrderId == cart.Id && c.ItemId == itemId);

            if (existingCartItem != null)
            {
                // Hvis brugeren allerede har den valgte item i cart, så bliver der lagt en til quantity
                existingCartItem.Quantity++;
                await _dbService.UpdateObjectAsync(existingCartItem);
            }
            else
            {
                // Hvis den valgte item ikke findes i cart endnu, så laves der en ny orderitem
                OrderItem newCartItem = new OrderItem
                {
                    OrderId = cart.Id,
                    ItemId = itemId,
                    Quantity = 1
                };

                await _dbService.AddObjectAsync(newCartItem);
            
            }

        }
        //private async Task<Order> GetCartAsync(int userId) //hjælpe metode til AddToCart
        //{
        //    Order cart; 
        //    var orders = _orderService.GetOrdersAsync().Result.ToList(); //Henter orders vha. orderservice

        //    cart = orders.FirstOrDefault(o => o.UserId == userId && o.State == Order.Status.Cart); //LINQ sortering til at finde ordre med vores userid og cart state

        //    if (cart == null) //Hvis den ik fandt en ordre laves en ny ordre
        //    {
        //        cart = new Order { State = Order.Status.Cart, UserId = userId };
        //        await _orderService.CreateOrderAsync(cart);
        //    }

        //    return cart; //Returnerer enten den ordre som blev fundet vha. LINQ eller den ordre som blev skabt
        //}


        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _dbService.GetObjectByIdAsync(id);
            if (orderItem != null)
            {
                await _dbService.DeleteObjectAsync(orderItem);
            }
        }


    }
}
