namespace ZealandPetShop.Models.Login
{
    public class User
    {
        public int Id { get; set; }
        //public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public User(int id, string email, string password, string firstName, string lastName, string phone, string address)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Address = address;
        }

        public User()
        {
        }

       
    }
}
