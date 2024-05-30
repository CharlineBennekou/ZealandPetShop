using ItemRazorV1.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandPetShop.Pages.Item
{
    public class UpdateItemModel : PageModel
    {
        private ItemService _itemService; // Service til håndtering af item-relaterede operationer.
        private IWebHostEnvironment _webHostEnvironment; // Miljø til håndtering af web hosting, fx filsystem.

        [BindProperty]
        public Models.Shop.Item Item { get; set; } // Binder Item model til viewet.

        // Konstruktor der initialiserer ItemService og IWebHostEnvironment.
        public UpdateItemModel(ItemService itemService, IWebHostEnvironment webHost)
        {
            _itemService = itemService;
            _webHostEnvironment = webHost;
        }

        // Metode der hentes når siden loades.
        public IActionResult OnGet(int Id)
        {
            Console.WriteLine("OnGet");
            Item = _itemService.GetItem(Id); // Henter det item der skal opdateres.
            if (Item == null)
            {
                return NotFound(); // Returnerer NotFound hvis item ikke findes.
            }
            return Page(); // Returnerer siden for at vise item opdateringsformularen.
        }

        [BindProperty]
        public IFormFile? Photo { get; set; } // Binder det nye billede til viewet.

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

        // Metode der håndterer POST anmodningen når et item opdateres.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            if (Photo != null && Item != null) // Tjekker om der er uploadet et nyt billede og om der er et item at opdatere.
            {
                if (Item.ImagePath != null) // Tjekker om item allerede har et billede.
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images", Item.ImagePath);
                    System.IO.File.Delete(filePath); // Sletter det gamle billede fra serveren.
                }

                // Behandler og gemmer det nye billede.
                Item.ImagePath = ProcessUploadedFile();
            }

            if (Item != null) // Tjekker om der er et item at opdatere.
            { _itemService.UpdateItem(Item); } // Opdaterer item ved at kalde UpdateItem metoden fra ItemService.

            return RedirectToPage("/Item/GetAllItems"); // Omdirigerer brugeren til siden med alle items.
        }
    }
}
