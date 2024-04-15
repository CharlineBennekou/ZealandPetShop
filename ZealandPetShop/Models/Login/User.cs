namespace ZealandPetShop.Models.Login
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Test { get; set; }
        public User(string userName, string password)

        {
            UserName = userName;
            Password = password;
        }

        public User()
        {
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(UserName)}={UserName}, {nameof(Password)}={Password}}}";
        }
    }
}
