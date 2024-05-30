using ZealandPetShop.Models.Order;

namespace ZealandPetShop.Services
{
    public class OrderService
    {
        private readonly DbGenericService<Order> _orderDbService;

        public OrderService(DbGenericService<Order> orderDbService)
        {
            _orderDbService = orderDbService;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderDbService.GetAllObjectsAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _orderDbService.AddObjectAsync(order);
            return order;
        }

    }
}
