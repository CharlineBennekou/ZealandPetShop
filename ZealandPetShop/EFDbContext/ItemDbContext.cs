using ZealandPetShop.Models.Shop;
using Microsoft.EntityFrameworkCore;
using ZealandPetShop.Models.Login;

namespace ZealandPetShop.EFDbContext
{
    public class ItemDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=mssql17.unoeuro.com;Initial Catalog=databaseprojekt_dk_db_smp_database;User ID=databaseprojekt_dk;Password=5RtGzhep3Fgdrfn4AcbE; Integrated Security=True; Connect Timeout=30; Encrypt=False");
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
