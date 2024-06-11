using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZealandPetShop.Models.Shop;
using ZealandPetShop.Models.Order;
using ZealandPetShop.Pages.Item;

namespace ZealandPetShop.Models.Order
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]

        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }

   
        public OrderItem() //bruger tomme til at oprette nyt orderitem
        {
        }

        
        //nav prop
        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
