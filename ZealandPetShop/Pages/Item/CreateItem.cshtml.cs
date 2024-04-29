using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Item
{
    public class CreateItemModel : PageModel
    {

        private ItemService _itemService;
		
		private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Models.Shop.Item Item { get; set; }

		[BindProperty]
		public IFormFile? Photo { get; set; }


		public CreateItemModel(ItemService itemService, IWebHostEnvironment webHost) 
        {
            _itemService = itemService;
			_webHostEnvironment = webHost;
		}

        public IActionResult OnGet()
        {
            return Page();
        }


		private string ProcessUploadedFile()
		{
			string uniqueFileName = null;
			if (Photo != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;

				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{ Photo.CopyTo(fileStream); }
			}
			return uniqueFileName;
		}




		public string errorMessage = "";

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				errorMessage = "Alle felter er ikke belvet udfyldt";
				return Page();
			}
			if (Photo != null) 
			{
				if(Item.ImagePath != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Item.ImagePath);
					System.IO.File.Delete(filePath);
				}

				Item.ImagePath = ProcessUploadedFile();
			}

			_itemService.AddItem(Item);
			return RedirectToPage("/Item/GetAllItems");
		}

	}
}
