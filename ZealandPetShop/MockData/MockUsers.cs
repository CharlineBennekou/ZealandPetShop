using Microsoft.AspNetCore.Identity;
using ZealandPetShop.Models.Login;

namespace ZealandPetShop.MockData
{
    
        public class MockUsers
        {
            private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>() {
              new User("admin", passwordHasher.HashPassword(null, "kode")),
              new User("kunde", passwordHasher.HashPassword(null, "kode"))
            };

        public static List<User> GetMockUsers()
        {
            return users;
        }


         }
      
        
    
}
