using ZealandPetShop.Models.Login;
using ZealandPetShop.MockData;
using ZealandPetShop.Models.Shop;
using ZealandPetShop.EFDbContext;

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
            _users = _dbService.GetObjectsAsync().Result.ToList();
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User GetUser(int id)
        {
            foreach (User user in _users)
            {
                if (user.Id == id)
                    return user;
            }
            return null;
        }
       
        public async Task SaveUSer(User user)
        {
            await _dbService.SaveObjects(new List<User>() { user });
        }


        public List<User> GetUsers() { return _users;
        }

        internal void AddUser(object user)
        {
            throw new NotImplementedException();
        }
    }
}
