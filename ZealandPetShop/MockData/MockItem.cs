using ZealandPetShop.Models.Shop;

namespace ZealandPetShop.MockData
{
    public class MockItem
    {
        private static List<Item> _items = new List<Item>()
        {
            new Item("Kattebakke", "/images/greyLitterBox.png" ,"Grå platik kattebakke", 150.00),
            new Item("Halsbånd", "/images/hundeHalsbånd.png" ,"Sort hunde halsbånd", 45.00),
            new Item("Kaninbur", "/images/kaninburBlå.png" ,"Kaninbur med blå bund", 300.00),
            new Item("Børste", "/images/grønBørste.png" ,"Grøn børste", 35.00),
            new Item("Madskål", "/images/gulMadskål.png" ,"Gul madskål", 20.00),
            new Item("Guld Halsbånd", "/images/gulMadskål.png" ,"Gul madskål", 20.00)
        };

        public static List<Item> GetMockItems()
        { return _items; }

    }
}
