using ZealandPetShop.Models.Order;

namespace ZealandPetShop.Services
{
    public class OrderService
    {
        private readonly DbGenericService<Order> _dbService;

        public OrderService(DbGenericService<Order> orderDbService)
        {
            _dbService = orderDbService;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _dbService.GetAllObjectsAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _dbService.AddObjectAsync(order);
            return order;
        }

        public async Task<Order> GetOrderCartByUserIdAsync(int userId)
        {
            IEnumerable<Order> orders = await _dbService.GetAllObjectsAsync();
            Order order = orders.FirstOrDefault(o => o.UserId == userId && o.State == Order.Status.Cart);

            if (order == null)
            {
                // Opretter en ny ordre hvis ordre ikke findes
                order = new Order
                {
                    UserId = userId,
                    State = Order.Status.Cart,
                    CreatedDate = DateTime.Now
                                              
                };

                await _dbService.AddObjectAsync(order);
            }

            return order;
        }

        public async Task MarkAsOrdered(int orderId)
        {
            // Henter Order vha. OrderId
            Order order = await _dbService.GetObjectByIdAsync(orderId);

            if (order != null && order.State == Order.Status.Cart)
            {
                // Markerer som ordered
                order.State = Order.Status.Ordered;

                // Opdaterer CreatedDate til at være nu
                order.CreatedDate = DateTime.Now;

                // Gemmer i db
                await _dbService.UpdateObjectAsync(order);
            }
       
            else
            {
                throw new ArgumentException("Ordre er enten null eller ikke en indkøbskurv.");
            }
        }

    }
}
