using ZealandPetShop.Models.Shop;

namespace ZealandPetShop.MockData
{
    public class MockItems
    {
        private static List<Item> _items = new List<Item>()
        {
            new Item("Kattebakke" ,"Grå platik kattebakke", 150.00),
            new Item("Halsbånd" ,"Sort hunde halsbånd", 45.00),
            new Item("Kaninbur" ,"Kaninbur med blå bund", 300.00),
            new Item("Børste" ,"Grøn børste", 35.00),
            new Item("Madskål" ,"Gul madskål", 20.00)
        };

        public static List<Item> GetMockItems()
        { return _items; }

    }
}
