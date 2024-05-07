using System.Security.Claims;
using ZealandPetShop.Models.Order;
namespace ZealandPetShop.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbGenericService<OrderItem> _dbService;

        public CartService(IHttpContextAccessor httpContextAccessor, DbGenericService<OrderItem> dbservice)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbService = dbservice;
        }
        public async Task AddItemToCart(int itemId)
        {
            // Get the current user's ID from the HttpContext
            var customerId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Check if the item already exists in the user's cart
            var existingCartItem = (await _dbService.GetAllObjectsAsync())
                .FirstOrDefault(c => c.UserId == customerId && c.ItemId == itemId);

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
                    //CustomerId = customerId,
                    ItemId = itemId,
                    Quantity = 1 // Start with quantity 1
                };

                //await _cartItemService.AddObjectAsync(newCartItem);
            }
        }
    }
}
