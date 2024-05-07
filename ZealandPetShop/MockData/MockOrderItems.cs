using ZealandPetShop.Models.Order;

namespace ZealandPetShop.MockData
{
    public class MockOrderItems
    {
        private static List<OrderItem> _items = new List<OrderItem>()
        {
            new OrderItem(1,1, 17, 2) 

        };

       public static List<OrderItem> GetMockOrderItems() { return _items; }
    }
}
