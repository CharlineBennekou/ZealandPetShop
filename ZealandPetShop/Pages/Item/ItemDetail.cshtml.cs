using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ZealandPetShop.MockData;
using ZealandPetShop.Models.Shop;

namespace ZealandPetShop.Pages.Item
{
    public class ItemDetailModel : PageModel
    {

        
        [BindProperty]
        [Required(ErrorMessage = "Indtast venligst mængden af produktet der ønskes")]
        public int Count { get; set; }


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
