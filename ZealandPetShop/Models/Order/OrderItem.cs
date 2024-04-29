using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZealandPetShop.Models.Shop;

namespace ZealandPetShop.Models.Order
{
    public class OrderItem
    {
        
        public int ItemId { get; set; }
        
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        


        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
