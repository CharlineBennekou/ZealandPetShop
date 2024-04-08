using ZealandPetShop.Models.Login;
using ZealandPetShop.MockData;
namespace ZealandPetShop.Services
{
    public class Userservice
    {
        public List<User> _users { get; }

        public Userservice()
        {

            _users = MockUsers.GetMockUsers();
       
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
