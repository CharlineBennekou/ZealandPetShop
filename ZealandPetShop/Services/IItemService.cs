using ZealandPetShop.Models.Shop;

namespace ZealandPetShop.Services
{
    public interface IItemService
    {
        List<Item> GetItems();
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Item GetItem(int id);
        Task<Item> DeleteItemAsync(int? itemId);
        IEnumerable<Item> NameSearch(string str);
        IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0);
        IEnumerable<Item> SortById();
        IEnumerable<Item> SortByIdDescending();
        IEnumerable<Item> SortByPrice();
        IEnumerable<Item> SortByPriceDescending();
        IEnumerable<Item> SortByName();
        IEnumerable<Item> SortByNameDescending();
    }

}
