using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Order
{
    public class GetOrderModel : PageModel
    {

        private ItemService _itemService;

        [BindProperty]
        public Models.Shop.Item Item { get; set; }







       
    }
}
