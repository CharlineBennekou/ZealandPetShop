namespace ZealandPetShop.DAO
{
    public class OrderDAO
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Count { get; set; }
    }
}

