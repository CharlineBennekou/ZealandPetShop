using Microsoft.AspNetCore.Identity;
using ZealandPetShop.Models.Login;

namespace ZealandPetShop.MockData
{
    
        public class MockUsers
        {
            private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>() {
            new User("admin@zealanddyr.com", passwordHasher.HashPassword(null,"kode"), "Admin", "Zealand", "12344321", "Addresse"),
            new User("admin@zealanddyr.com", passwordHasher.HashPassword(null,"kode"), "Admin", "Zealand", "12344321", "Addresse"),
            new User("Joe@gmail.com", passwordHasher.HashPassword(null,"kode"), "Admin", "Zealand", "12344321", "Addresse")
            };

        public static List<User> GetMockUsers()
        {
            return users;
        }


         }
      
        
    
}
