using ItemRazorV1.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Item
{
    public class UpdateItemModel : PageModel
    {
        private ItemService _itemService;

        [BindProperty]
        public Models.Shop.Item Item { get; set; }

        public UpdateItemModel(ItemService itemService)
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

		[BindProperty]
		public IFormFile? Photo { get; set; }

		public IActionResult OnPost()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }
            _itemService.UpdateItem(Item);
            return RedirectToPage("/Item/GetAllItems");
        }
    }
}
