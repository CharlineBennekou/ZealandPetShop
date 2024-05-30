using ZealandPetShop.MockData;
using ZealandPetShop.Models.Shop;
using ZealandPetShop.Services;

namespace ItemRazorV1.Service
{
    public class ItemService
    {
        private List<Item> _items; // Liste der indeholder alle items.
        private DbGenericService<Item> _dbService; // Generisk database service til CRUD operationer.

        // Konstruktor der initialiserer database servicen og henter alle items fra databasen.
        public ItemService(DbGenericService<Item> dbService)
        {
            // Bruger ikke mock items længere.
            //_items = MockItems.GetMockItems();

            _dbService = dbService;
            ////dbService.SaveObjects(_items); // Gemmer mock items i databasen (kommenteret ud).

            // Henter alle items fra databasen og konverterer til liste.
            _items = _dbService.GetAllObjectsAsync().Result.ToList();
        }

        // Metode til at tilføje et nyt item (ubrugt i den nuværende kode).
        public void AddItem(Item item)
        {
            // Tilføjer item til listen.
            _items.Add(item);
            // Tilføjer item til databasen.
            _dbService.AddObjectAsync(item);
        }

        // Metode til at hente et item baseret på dets ID.
        public Item GetItem(int id)
        {
            // Itererer gennem listen af items.
            foreach (Item item in _items)
            {
                // Returnerer item hvis ID matcher.
                if (item.Id == id)
                    return item;
            }

            // Returnerer null hvis item ikke findes.
            return null;
        }

        // Metode til at opdatere et eksisterende item (ubrugt i den nuværende kode).
        public void UpdateItem(Item item)
        {
            if (item != null)
            {
                // Itererer gennem listen af items.
                foreach (Item i in _items)
                {
                    // Finder det item der skal opdateres.
                    if (i.Id == item.Id)
                    {
                        // Opdaterer itemets egenskaber.
                        i.Name = item.Name;
                        i.Art = item.Art;
                        i.Price = item.Price;
                        i.Description = item.Description;
                        i.ImagePath = item.ImagePath;
                    }
                }
                // Opdaterer item i databasen.
                _dbService.UpdateObjectAsync(item);
            }
        }

        // Metode til at slette et item baseret på dets ID.
        public Item DeleteItem(int? itemId)
        {
            Item itemToBeDeleted = null;
            // Finder itemet der skal slettes.
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
                // Fjerner item fra listen.
                _items.Remove(itemToBeDeleted);
                // Sletter item fra databasen.
                _dbService.DeleteObjectAsync(itemToBeDeleted);
            }

            // Returnerer det slettede item.
            return itemToBeDeleted;
        }

        // Metode til at hente alle items.
        public List<Item> GetItems()
        {
            return _items;
        }

        // Metode til at søge efter items baseret på navn.
        public IEnumerable<Item> NameSearch(string str)
        {
            if (string.IsNullOrEmpty(str)) return _items;
            // Filtrerer items hvis navn indeholder søgestrengen (case insensitive).
            return from item in _items
                   where item.Name.ToLower().Contains(str.ToLower())
                   select item;
        }

        // Metode til at filtrere items baseret på pris.
        public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)
        {
            // Filtrerer items baseret på prisintervallet.
            return from item in _items
                   where (minPrice == 0 && item.Price <= maxPrice) ||
                         (maxPrice == 0 && item.Price >= minPrice) ||
                         (item.Price >= minPrice && item.Price <= maxPrice)
                   select item;
        }

        // Metode til at sortere items efter ID i stigende rækkefølge.
        public IEnumerable<Item> SortById()
        {
            return from item in _items
                   orderby item.Id
                   select item;
        }

        // Metode til at sortere items efter ID i faldende rækkefølge.
        public IEnumerable<Item> SortByIdDescending()
        {
            return from item in _items
                   orderby item.Id descending
                   select item;
        }

        // Metode til at sortere items efter navn i stigende rækkefølge.
        public IEnumerable<Item> SortByName()
        {
            return from item in _items
                   orderby item.Name
                   select item;
        }

        // Metode til at sortere items efter navn i faldende rækkefølge.
        public IEnumerable<Item> SortByNameDescending()
        {
            return from item in _items
                   orderby item.Name descending
                   select item;
        }

        // Metode til at sortere items efter pris i stigende rækkefølge.
        public IEnumerable<Item> SortByPrice()
        {
            return from item in _items
                   orderby item.Price
                   select item;
        }

        // Metode til at sortere items efter pris i faldende rækkefølge.
        public IEnumerable<Item> SortByPriceDescending()
        {
            return from item in _items
                   orderby item.Price descending
                   select item;
        }

        // Metode til at søge efter items baseret på et søgeterm.
        public IEnumerable<Item> Search(string searchTerm)
        {
            List<Item> searchResults = new List<Item>();

            // Itererer gennem listen af items.
            foreach (Item i in _items)
            {
                // Tilføjer items til resultatlisten hvis søgetermen findes i navn eller beskrivelse (case insensitive).
                if (string.IsNullOrEmpty(searchTerm) ||
                    i.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    i.Description.ToLower().Contains(searchTerm.ToLower()))
                {
                    searchResults.Add(i);
                }
            }

            // Returnerer resultatlisten.
            return searchResults;
        }
    }
}
