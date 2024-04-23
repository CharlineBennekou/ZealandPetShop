using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;

namespace ZealandPetShop.Pages.Item
{
    public class GetAllItemsModel : PageModel
    {
        private ItemService _itemService;
        public List<Models.Shop.Item> Items { get; set; }

        public GetAllItemsModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public void OnGet()
        {
            Items = _itemService.GetItems();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            // Here, you can implement the logic to add the item with the specified id to the shopping cart
            // This is just a placeholder method
            // For example:
            // ShoppingCart.Add(id);

            // Redirect to a different page after adding to cart
            return RedirectToPage("/Order/GetOrder");

        }

        public IActionResult OnPostSeeDetails(int id)
        {

            return RedirectToPage("/Item/ItemDetail");

        }

    }
}
