namespace ZealandPetShop.Models.Order
{
    public class OrderItem
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
    }
}
