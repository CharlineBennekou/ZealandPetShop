using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZealandPetShop.Models.Order;

namespace ZealandPetShop.Models.Login
{
    public class User
    {
            private string _email;
            private string _password;
            private string _firstName;
            private string _lastName;
            private string _phone;
            private string _address;

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public ICollection<Order.Order> Orders { get; set; }

            public string Email
            {
                get => _email;
                set
                {
                    if (!IsValidEmail(value))
                    {
                        throw new ArgumentException("Invalid email format.");
                    }
                    _email = value;
                }
            }

            public string Password
            {
                get => _password;
                set => _password = value; // No constraints for demo
            }

            public string FirstName
            {
                get => _firstName;
                set
                {
                    if (!IsValidName(value))
                    {
                        throw new ArgumentException("First name can only contain letters.");
                    }
                    _firstName = value;
                }
            }

            public string LastName
            {
                get => _lastName;
                set
                {
                    if (!IsValidName(value))
                    {
                        throw new ArgumentException("Last name can only contain letters.");
                    }
                    _lastName = value;
                }
            }

            public string Phone
            {
                get => _phone;
                set
                {
                    if (!IsValidPhone(value))
                    {
                        throw new ArgumentException("Invalid phone number.");
                    }
                    _phone = value;
                }
            }

            public string Address
            {
                get => _address;
                set => _address = value; // No constraints for demo
            }

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

            public User(string email, string password, string firstName, string lastName, string phone, string address)
            {
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

            private bool IsValidEmail(string email) //tjekker om email er valid
            {
                return !string.IsNullOrEmpty(email) && email.Contains("@"); //Email må ikk være empty, skal indeholde @.
            }

            private bool IsValidName(string name) //Tjekker om first- og lastname er valid
            {

                return !string.IsNullOrEmpty(name) && System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-ZæøåÆØÅ]+$"); //Navn må ik være null/empty og skal bruge det normale alfabet inklusiv danske bogstaver
            }

            private bool IsValidPhone(string phone) //Tjekker om telefon nr er valid
            {

                return !string.IsNullOrEmpty(phone) && phone.Length == 8 && System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]+$"); //Tlf må ikke være null/empty og skal være 8 cifre lang, og kun tal.
            }
        }


    
}
