namespace ZealandPetShop.Models.Order
{
    public class Order
    {
        
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }

        public Order(int id, int userId)
        {
            Id = id;
            CreatedDate = DateTime.Now;
            UserId = userId;
            Status = "Processing";
        }

        public Order()
        { }
    }
}
