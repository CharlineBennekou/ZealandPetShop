using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ZealandPetShop.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public User()
        {
            UserName = "";
            Password = "";
        }
    }
}
