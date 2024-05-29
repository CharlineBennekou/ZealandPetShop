using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZealandPetShop.Models.Order;

namespace ZealandPetShop.Models.Shop
{
    public class Item
    {
        // Enum for forskellige dyrearter, som produkter kan være relateret til.
        public enum DyreArt
        {
            Hund = 0,
            Kat = 1,
            Kanin = 2,
        }

        // Primær nøgle for 'Item' entiteten. ID'et genereres automatisk.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Item ID")]
        [Range(typeof(int), "0", "10000", ErrorMessage = "ID skal være mellem (1) og (2)")]
        public int? Id { get; set; }

        // Sti til billedet af varen.
        [Display(Name = "ImagePath")]
        public string ImagePath { get; set; }

        // Navnet på varen. Skal udfyldes.
        [Display(Name = "Item Navn")]
        [Required(ErrorMessage = "Item skal have et navn")]
        public string? Name { get; set; }

        // Beskrivelse af varen. Skal udfyldes.
        [Display(Name = "Item desc")]
        [Required(ErrorMessage = "Item skal have en beskrivelse")]
        public string Description { get; set; }

        // Prisen på varen. Skal udfyldes.
        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Der skal angives en pris")]
        public double? Price { get; set; }

        // Dyrearten som varen er relateret til.
        public DyreArt Art { get; set; }

        // Navigation property til 'OrderItem' entiteten.
        public ICollection<OrderItem> OrderItems { get; set; }

        // Standard konstruktør
        public Item()
        {
        }

        // Overbelastet konstruktør til initialisering af et nyt 'Item' objekt.
        public Item(string name, string description, double price, DyreArt art)
        {
            Name = name;
            Description = description;
            Price = price;
            Art = art;
        }
    }
}
