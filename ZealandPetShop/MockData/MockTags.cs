﻿using ZealandPetShop.Models.Shop;

namespace ZealandPetShop.MockData
{
    public class MockTags
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

        private static List<Tag> _tags = new List<Tag>()
        { new Tag("Kat"),
            new Tag("Hund"),
            new Tag("Kanin"),
            new Tag("Legetøj"),
            new Tag("Accessories")


        };

    }
}
