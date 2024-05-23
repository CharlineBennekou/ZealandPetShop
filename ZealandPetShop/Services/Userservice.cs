using ZealandPetShop.Models.Login;
using ZealandPetShop.MockData;
using ZealandPetShop.Models.Shop;
using Microsoft.AspNetCore.Identity;
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
            _users = _dbService.GetAllObjectsAsync().Result.ToList();
        }

        //public void AddUser(User user)
        //{
        //    _users.Add(user);
        //}
        //public User GetUser(int id)
        //{
        //    foreach (User user in _users)
        //    {
        //        if (user.Id == id)
        //            return user;
        //    }
        //    return null;
        //}


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


        public async Task AddUser(User user)
        {
            _users.Add(user);
            //_jsonFileService.SaveJsonObjects(Users);
            await _dbService.AddObjectAsync(user);
        }



        public async Task UpdateUser(User user)
        {
            var existingUser = await _dbService.GetObjectByIdAsync(user.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }



            if (existingUser.Id == user.Id)
            {
                var passwordHasher = new PasswordHasher<string>();

                existingUser.Email = user.Email;
                existingUser.Password = passwordHasher.HashPassword(null, user.Password);
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Phone = user.Phone;
                existingUser.Address = user.Address;

                await _dbService.UpdateObjectAsync(existingUser);
            }
        }

        public async Task<User> DeleteUser(User user)
        {
            var existingUser = await _dbService.GetObjectByIdAsync(user.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found");
            }

            if (existingUser.Id == user.Id)
            {
                await _dbService.DeleteObjectAsync(existingUser);
                return existingUser;
            }
            return null;
        }

    }
}
