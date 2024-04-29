using ZealandPetShop.Models.Login;
using ZealandPetShop.MockData;
using ZealandPetShop.Models.Shop;
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
       

        public List<User> GetUsers() { return _users;
        }
    }
}
