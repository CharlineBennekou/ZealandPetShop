using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZealandPetShop.Models.Order;

namespace ZealandPetShop.Models.Shop
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Item ID")]
        [Required(ErrorMessage = "Der skal angives et ID til Item")]
        [Range(typeof(int), "0", "10000", ErrorMessage = "ID skal være mellem (1) og (2)")]
        public int? Id { get; set; }
        [Display(Name = "ImagePath")]
        [Required(ErrorMessage = "Item skal have et billede")]

        public string ImagePath { get; set; }

        [Display(Name = "Item Navn")]
        [Required(ErrorMessage = "Item skal have et navn")]
        public string? Name { get; set; }
        [Display(Name = "Item desc")]
        [Required(ErrorMessage = "Item skal have en beskrivelse")]

        public string Description { get; set; }

        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Der skal angives en pris")]
        public double? Price { get; set; }

        //Navigation property
        public ICollection<OrderItem> OrderItems { get; set; }

        public Item()
        {
        }

        public Item(string name, string imagePath, string description, double price)
        {
            Name = name;
            ImagePath = imagePath;
            Description = description;
            Price = price;
        }
    }
}
