using ZealandPetShop.Models.Shop;

namespace ZealandPetShop.MockData
{
    public class MockItems
    {
        // Statisk liste der indeholder nogle eksempler på Item objekter.
        private static List<Item> _items = new List<Item>()
    {
        // Initialisering af Item objekter med navn, beskrivelse, pris og dyreart.
        new Item("Kattebakke", "Grå platik kattebakke", 150.00, Item.DyreArt.Kat),
        new Item("Halsbånd", "Sort hunde halsbånd", 45.00, Item.DyreArt.Hund),
        new Item("Kaninbur", "Kaninbur med blå bund", 300.00, Item.DyreArt.Kanin),
        new Item("Børste", "Grøn børste", 35.00, Item.DyreArt.Kat)
    };

        // Statisk metode der returnerer listen af mock items.
        public static List<Item> GetMockItems()
        {
            return _items;
        }
    }
}
