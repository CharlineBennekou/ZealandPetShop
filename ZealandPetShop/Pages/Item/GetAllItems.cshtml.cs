using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandPetShop.MockData;

namespace ZealandPetShop.Pages.Item
{
    public class GetAllItemsModel : PageModel
    {

        public List<Models.Shop.Item> Items { get; set; }

        public void OnGet()
        {
            Items = MockItem.GetMockItems();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            // Here, you can implement the logic to add the item with the specified id to the shopping cart
            // This is just a placeholder method
            // For example:
            // ShoppingCart.Add(id);

            // Redirect to a different page after adding to cart
            return RedirectToPage("/Cart");

        }

        public IActionResult OnPostSeeDetails(int id)
        {

            return RedirectToPage("/Item/ItemDetail");

        }

    }
}
