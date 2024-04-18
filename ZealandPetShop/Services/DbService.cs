using ZealandPetShop.Models.Login;
using ZealandPetShop.Models.Shop;
using ZealandPetShop.EFDbContext;
using Microsoft.EntityFrameworkCore;

namespace ZealandPetShop.Services
{
        public class DbService
        {

            public async Task<List<Item>> GetItems() //Get list of items fra db
            {
                using (var context = new ItemDbContext())
                {
                    return await context.Items.ToListAsync<Item>();
                }
            }
            public async Task AddItem(Item item) //Add item til db
            {
                using (var context = new ItemDbContext())
                {
                    context.Items.Add(item);
                    context.SaveChanges();
                }
            }
            public async Task SaveItems(List<Item> items) //Gemmer listen af items
            {
                using (var context = new ItemDbContext())
                {
                    foreach (Item item in items)
                    {
                        context.Items.Add(item);
                        context.SaveChanges();
                    }

                    context.SaveChanges();
                }
            }

            public async Task SaveUsers(List<User> users) //Gemmer listen af users
            {
                using (var context = new ItemDbContext())
                {
                    foreach (User user in users)
                    {
                        context.Users.Add(user);

                    }
                    context.SaveChanges();
                }
            }
        }
}
