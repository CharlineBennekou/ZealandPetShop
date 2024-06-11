using ZealandPetShop.Models.Shop;
using Microsoft.EntityFrameworkCore;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Models.Order;

namespace ZealandPetShop.EFDbContext
{
    public class ItemDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) //her er vores connection string til databasen 
        {
            options.UseSqlServer(@"Data Source=mssql17.unoeuro.com;Initial Catalog=databaseprojekt_dk_db_smp_database;User ID=databaseprojekt_dk;Password=5RtGzhep3Fgdrfn4AcbE;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        //Encrypt=False

        public DbSet<Item> Items { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Order { get; set; } //når den har taget fra tabellen laves den om til DbSet (gælder for alle)
    }// DbSet får en sql quary som gemmer recordes fra databasen ned i DbSet
    //ORM (object relation mapper) håndtere database forbindelser 
}
