using ZealandPetShop.MockData;
using ZealandPetShop.Models.Shop;
using ZealandPetShop.Services;

namespace ItemRazorV1.Service
{
    public class ItemService
    {
        private List<Item> _items;

        private DbGenericService<Item> _dbService;

        public ItemService(DbGenericService<Item> dbService)
        {
            //_items = MockItems.GetMockItems();
            _dbService = dbService;
            //dbService.SaveItems(_items);
            _items = _dbService.GetAllObjectsAsync().Result.ToList();
        }

        public void AddItem(Item item) //Ubrugt
        {
            _items.Add(item);
            _dbService.AddObjectAsync(item);
        }

        public Item GetItem(int id)
        {
            foreach (Item item in _items)
            {
                if (item.Id == id)
                    return item;
            }

            return null;
        }

        public void UpdateItem(Item item) //Ubrugt
        {
            if (item != null)
            {
                foreach (Item i in _items)
                {
                    if (i.Id == item.Id)
                    {
                        i.Name = item.Name;
                        i.Price = item.Price;
                        i.Description = item.Description;
                        i.ImagePath = item.ImagePath;
                    }
                }
                _dbService.UpdateObjectAsync(item);
            }
        }

        public Item DeleteItem(int? itemId)
        {
            Item itemToBeDeleted = null;
            foreach (Item item in _items)
            {
                if (item.Id == itemId)
                {
                    itemToBeDeleted = item;
                    break;
                }
            }
            if (itemToBeDeleted != null)
            {
                _items.Remove(itemToBeDeleted);
                _dbService.DeleteObjectAsync(itemToBeDeleted);
            }

            return itemToBeDeleted;
        }

        public List<Item> GetItems() { return _items; }


        public IEnumerable<Item> NameSearch(string str)
        {
            if (string.IsNullOrEmpty(str)) return _items;
            return from item in _items where item.Name.ToLower().Contains(str.ToLower()) select item;
        }



        public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)
        {
            return from item in _items
                   where (minPrice == 0 && item.Price <= maxPrice) ||
                     (maxPrice == 0 && item.Price >= minPrice) ||
                     (item.Price >= minPrice && item.Price <= maxPrice)
                   select item;
        }


        public IEnumerable<Item> SortById()
        {
            return from item in _items
                   orderby item.Id
                   select item;
        }

        public IEnumerable<Item> SortByIdDescending()
        {
            return from item in _items
                   orderby item.Id descending
                   select item;
        }


        public IEnumerable<Item> SortByName()
        {
            return from item in _items
                   orderby item.Name
                   select item;
        }

        public IEnumerable<Item> SortByNameDescending()
        {
            return from item in _items
                   orderby item.Name descending
                   select item;
        }


        public IEnumerable<Item> SortByPrice()
        {
            return from item in _items
                   orderby item.Price
                   select item;
        }

        public IEnumerable<Item> SortByPriceDescending()
        {
            return from item in _items
                   orderby item.Price descending
                   select item;
        }
    }
}
