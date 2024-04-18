using ZealandPetShop.Models.Login;
using ZealandPetShop.MockData;
namespace ZealandPetShop.Services
{
    public class UserService
    {
        private DbService dbService;
        public List<User> _users { get; }

        public UserService(DbService dbService)
        {
            _users = MockUsers.GetMockUsers();
            this.dbService = dbService;
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
    }
}
