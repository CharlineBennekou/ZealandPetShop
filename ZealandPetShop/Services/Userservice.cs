﻿using ZealandPetShop.Models.Login;
using ZealandPetShop.MockData;
using ZealandPetShop.Models.Shop;
using ZealandPetShop.EFDbContext;
using Microsoft.EntityFrameworkCore;

namespace ZealandPetShop.Services
{
    public class UserService
    {
        private DbGenericService<User> _dbService;
        public List<User> _users { get; }

        public UserService(DbGenericService<User> dbService)
        {
            //_users = MockUsers.GetMockUsers();
            _dbService = dbService;
            //_dbService.SaveObjects(_users);
            //_users = _dbService.GetObjectsAsync().Result;
        }

        public async Task AddUser(User user)
        {
            await _dbService.AddObjectAsync(user);
        }

        public async Task SaveUSer(User user)
        {
            await _dbService.SaveObjects(new List<User> { user });
        }


        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbService.GetAllObjectsAsync();
        }


        public async Task<User> GetUser(int id)
        { 
            return await _dbService.GetObjectByIdAsync(id); 
        }


        internal void AddUser(object user)
        {
            throw new NotImplementedException();
        }




        public async Task UpdateUser(User user)
        {
           // Tjekker om brugeren eksistere i databasen først
            var existingUser = await _dbService.GetObjectByIdAsync(user.Id);
            if (existingUser != null)
            {
                // Opdater eksisterende bruger
                await _dbService.UpdateObjectAsync(user);
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }
        }

    }
}

