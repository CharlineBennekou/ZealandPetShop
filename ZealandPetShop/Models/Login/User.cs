using System.ComponentModel.DataAnnotations;

namespace ZealandPetShop.Models.Login
{
    public class User
    {
        [Required]
        [Key]
        public int Id { get; set; }
        //public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
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
