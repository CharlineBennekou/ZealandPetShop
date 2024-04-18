using ZealandPetShop.Models.Login;
using ZealandPetShop.Models.Shop;
namespace ZealandPetShop.Models.Order
{
    public class Order
    {

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public string Status { get; set; }

        public Order(int id, int userId)
        {
            Id = id;
            CreatedDate = DateTime.Now;
            UserId = userId;
            Status = "Processing";
        }

        public Order()
        {
            CreatedDate = DateTime.Now;
            Status = "Processing";
        }
    }
}
