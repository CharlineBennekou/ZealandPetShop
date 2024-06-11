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
            IEnumerable<Order> orders = await _dbService.GetAllObjectsAsync(); //laver en liste af orders, den assignest til at være alle orders i vores database
            Order order = orders.FirstOrDefault(o => o.UserId == userId && o.State == Order.Status.Cart); //finder den order hvor userId matcher og status == cart 

            if (order == null) //null tjek - hvis kunden ikke allerede har en kurv
            {
                // Opretter en ny ordre hvis ordre(kurv) ikke findes
                order = new Order //bruger denne konstroktor 
                {
                    UserId = userId, //UserId skal være den vi fik fra vores cartService
                    State = Order.Status.Cart,
                    CreatedDate = DateTime.Now
                                              
                };

                await _dbService.AddObjectAsync(order); //tilføjer til databasen
            }

            return order; //Vores order bliver nu retuneret til vores cartService
        }

        public async Task MarkAsOrdered(int orderId) //id på kundes order
        {
            // Henter Order vha. OrderId
            Order order = await _dbService.GetObjectByIdAsync(orderId); 

            if (order != null && order.State == Order.Status.Cart) //tjekker
            {
                // Markerer som ordered
                order.State = Order.Status.Ordered;

                // Opdaterer CreatedDate til at være nu
                order.CreatedDate = DateTime.Now;

                // Gemmer i db
                await _dbService.UpdateObjectAsync(order);
            }
       
            else //hvis if tjekket fejlede får vi execption
            {
                throw new ArgumentException("Ordre er enten null eller ikke en indkøbskurv.");
            }
        }

    }
}
