using ItemRazorV1.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Item
{
    public class UpdateItemModel : PageModel
    {
        private ItemService _itemService;
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Models.Shop.Item Item { get; set; }

        public UpdateItemModel(ItemService itemService, IWebHostEnvironment webHost)
        {
            _itemService = itemService;
            _webHostEnvironment = webHost;
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

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
                uniqueFileName = "/images/" + uniqueFileName;
            }
            return uniqueFileName;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            if (Photo != null && Item != null)
            {
                if (Item.ImagePath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images", Item.ImagePath);
                    System.IO.File.Delete(filePath);
                }

                // Process and save the new image  
                Item.ImagePath = ProcessUploadedFile();
            }

            if (Item != null) 
            { _itemService.UpdateItem(Item); }


            return RedirectToPage("/Item/GetAllItems");
        }
    }
}
