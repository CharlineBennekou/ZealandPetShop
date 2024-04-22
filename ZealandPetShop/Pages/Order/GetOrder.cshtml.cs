using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;

namespace ZealandPetShop.Pages.Order
{
    public class GetOrderModel : PageModel
    {

        //private ItemService _itemService;

        [BindProperty]
        public Models.Shop.Item Item { get; set; }

        public List<Models.Shop.Item> Items { get; set; }

        public void OnGet()
        {
            Items = MockItems.GetMockItems();
        }








    }
}
