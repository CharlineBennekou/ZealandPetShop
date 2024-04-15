﻿using Microsoft.AspNetCore.Identity;
using ZealandPetShop.Models.Login;

namespace ZealandPetShop.MockData
{
    
        public class MockUsers
        {
            private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>() {
            new User(0, "admin", passwordHasher.HashPassword(null,"kode"), "Admin", "Zealand", "12344321", "Addresse"),
            new User(1, "Anna@gmail.com", passwordHasher.HashPassword(null,"kode"), "Anna", "Frigaard", "12344321", "Addresse"),
            new User(2, "Joe@gmail.com", passwordHasher.HashPassword(null,"kode"), "Joe", "Jensen", "12344321", "Addresse")
            };

        public static List<User> GetMockUsers()
        {
            return users;
        }


         }
      
        
    
}
    