using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Models.Shop;
namespace ZealandPetShop.Models.Order
{
    public class Order
    {
        public enum Status
        {
            Cart=0,
            Ordered=1
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public Status State { get; set; }

        //Navigation property
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


        public Order()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
