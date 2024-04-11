namespace ZealandPetShop.Models.Shop
{
    public class Item
    {
        static private int NextId = 0;

        //[Display(Name = "Item ID")]
        //[Required(ErrorMessage = "Der skal angives et ID til Item")]
        //[Range(typeof(int), "0", "10000", ErrorMessage = "ID skal være mellem (1) og (2)")]
        public int? Id { get; set; }

        public string ImagePath { get; set; }

        //[Display(Name = "Item Navn")]
        //[Required(ErrorMessage = "Item skal have et navn")]
        public string? Name { get; set; }

        public string Description { get; set; }

        //[Display(Name = "Pris")]
        //[Required(ErrorMessage = "Der skal angives en pris")]
        public double? Price { get; set; }

        public Item()
        {
            Id = NextId++; 
        }

        public Item(string name, string imagePath, string description, double price)
        {
            Id = NextId++;
            Name = name;
            ImagePath = imagePath;
            Description = description;
            Price = price;
        }
    }
}
