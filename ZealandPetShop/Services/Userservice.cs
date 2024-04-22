using ZealandPetShop.DAO;
using ZealandPetShop.Models.Login;
using ZealandPetShop.MockData;

namespace ZealandPetShop.Services
{
    public class UserService
    {
        //private DbService dbService;
        //public List<User> _users { get; }

        public List<User> Users { get; }
        private JsonFileService<User> _jsonFileService;
        private UserDbService _dbService;


        //public UserService(DbService dbService)
        //{
        //    _users = MockUsers.GetMockUsers();
        //    this.dbService = dbService;
        //}
        public UserService(JsonFileService<User> jsonFileService, UserDbService dbService)
        {
            _jsonFileService = jsonFileService;
            _dbService = dbService;
            //Users = MockUsers.GetMockUsers();
            //Users = _jsonFileService.GetJsonObjects().ToList();
            //_jsonFileService.SaveJsonObjects(Users);
            //_dbService.SaveObjects(Users);
            Users = _dbService.GetObjectsAsync().Result.ToList();

        }

        //public async void AddUser(User user)
        //{
        //    _users.Add(user);
        //    await _dbService.AddObjectAsync(user);
        //}

        public async Task AddUserAsync(User user)
        {
            Users.Add(user);
            //_jsonFileService.SaveJsonObjects(Users);
            await _dbService.AddObjectAsync(user);
        }



        //public User GetUser(int id)
        //{
        //    foreach (User user in _users)
        //    {
        //        if (user.Id == id)
        //            return user;
        //    }

        //    return null;
        //}

        public User GetUserByUserName(string username)
        {
            //return DbService.GetObjectByIdAsync(username).Result;
            return Users.Find(user => user.UserName == username);
        }


        public async Task<User> GetUserOrdersAsync(User user)
        {
            return await _dbService.GetOrdersByUserIdAsync(user.UserId);
        }

    }
}
