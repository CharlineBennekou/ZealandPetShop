using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Item
{
    public class DeleteItemModel : PageModel
    {

        private ItemService _itemService;

        [BindProperty]
        public Models.Shop.Item Item { get; set; }

        public DeleteItemModel(ItemService itemService)
        {
            _itemService = itemService;           
        }

        public IActionResult OnGet(int Id)
        {
            Console.WriteLine("OnGet");
            Item = _itemService.GetItem(Id);
            if (Item == null)
            {
                return NotFound();
            }
            return Page();

        }

        public IActionResult OnPost()
        {
            Models.Shop.Item deletedItem = _itemService.DeleteItem(Item.Id);
            if (deletedItem == null) 
            {
                return NotFound();
            }
            return RedirectToPage("/Item/GetAllItems");
        }

    }
}
