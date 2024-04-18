using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;

namespace ZealandPetShop.Pages.Item
{
    public class ItemDetailModel : PageModel
    {

        public List<Models.Shop.Item> Items { get; set; }

        //public MockData.MockItem MockItem { get; set; }

        public Models.Shop.Item Item { get; set; }



        public Models.Shop.Item GetItemId(int id)
        {
            foreach (Models.Shop.Item item in Items)
            {
                if (item.Id == id)
                { return item; }
            }
            return null;
        }

        public IActionResult OnGet(int Id)
        {
            Items = MockItems.GetMockItems();
            Item = GetItemId(Id);
            if (Item == null)
            { return NotFound(); }
            return Page();
        }


    }
}
