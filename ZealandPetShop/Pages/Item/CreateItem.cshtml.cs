using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Item
{
    public class CreateItemModel : PageModel
    {
        private ItemService _itemService; // Service til håndtering af item-relaterede operationer.
        private IWebHostEnvironment _webHostEnvironment; // Miljø til håndtering af web hosting, fx filsystem.

        [BindProperty]
        public Models.Shop.Item Item { get; set; } // Binder Item model til viewet.

        [BindProperty]
        public IFormFile Photo { get; set; } // Binder Photo uploadet fra brugeren til viewet.

        // Konstruktor der initialiserer ItemService og IWebHostEnvironment.
        public CreateItemModel(ItemService itemService, IWebHostEnvironment webHost)
        {
            _itemService = itemService;
            _webHostEnvironment = webHost;
        }

        // Metode der hentes når siden loades.
        public IActionResult OnGet()
        {
            return Page();
        }

        // Metode til at behandle den uploadede fil.
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null; // Variabel til at holde det unikke filnavn.
            if (Photo != null) // Tjekker om der er en fil uploadet.
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images"); // Finder stien til upload mappen.
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName; // Genererer et unikt filnavn.

                string filePath = Path.Combine(uploadsFolder, uniqueFileName); // Kombinerer stien og det unikke filnavn.

                // Opretter en filstream og kopierer filen til serveren.
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
                uniqueFileName = "/images/" + uniqueFileName; // Sætter stien til billedet.
            }
            return uniqueFileName;
        }

        // Metode der håndterer POST anmodningen når brugeren opretter et item.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Hvis en ny fil er uploadet, behandles denne.
            if (Photo != null)
            {
                // Hvis der allerede er en filsti, slettes den gamle fil.
                if (Item.ImagePath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images", Item.ImagePath);
                    System.IO.File.Delete(filePath); // Sletter den gamle fil.
                }

                // Behandler og gemmer det nye billede.
                Item.ImagePath = ProcessUploadedFile();
            }

            // Hvis der ikke er uploadet et billede, bevares den eksisterende ImagePath værdi.

            _itemService.AddItem(Item); // Tilføjer det nye item ved at kalde AddItem metoden fra ItemService.
            return RedirectToPage("/Item/GetAllItems"); // Omdirigerer brugeren til siden med alle items.
        }
    }

}

