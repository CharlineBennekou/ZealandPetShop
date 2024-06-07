using ZealandPetShop.Models.Login;
using ZealandPetShop.MockData;
using ZealandPetShop.Models.Shop;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace ZealandPetShop.Services
{
    public class UserService
    {
        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        //en privat PasswordHasher for User-objekter
        public PasswordHasher<UserService> passwordHasher;
        //en privat database service objekt
        private DbGenericService<User> _dbService;
        //Gemmer en liste af User-objekter
        public List<User> _users { get; }

        

        // Constructor for UserService
        public UserService(/*UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager*/ DbGenericService<User> dbService)
        {
            //_userManager = userManager;
            //_signInManager = signInManager;
            //_roleManager = roleManager;

            // Initialiserer DbServicen
            _dbService = dbService;

            // Henter alle brugerobjekter fra databasen og sætter dem ind til en liste
            _users = _dbService.GetAllObjectsAsync().Result.ToList();
        }

        //public async Task<bool> EmailExistingAsync(string email)
        //{
        //    return await _userManager.Users.AnyAsync(u => u.Email == email);
        //}
        

        // Asynkron metode til at gemme en bruger
        public async Task SaveUSer(User user)
        {
            // Gemmer brugeren i databasen
            await _dbService.SaveObjects(new List<User> { user });
        }

        // Asynkron metode til at hente alle brugere
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            // Returnerer alle brugere fra databasen
            return await _dbService.GetAllObjectsAsync();
        }

        // Asynkron metode til at hente en bruger efter (id)
        
        public async Task<User> GetUser(int id)
        {
            // Returnerer brugeren med det specifikke (id) fra databasen
            return await _dbService.GetObjectByIdAsync(id);
        }

        // Asynkron metode til at tilføje en bruger
        public async Task AddUser(User user)
        {
            //Tilføjer brugeren til listen
            _users.Add(user);

            //_jsonFileService.SaveJsonObjects(Users);

            // Tilføjer brugeren til databasen
            await _dbService.AddObjectAsync(user);
        }


        // Asynkron metode til at opdatere en bruge
        public async Task UpdateUser(User user)
        {
            // Henter den eksisterende bruger fra databasen
            var existingUser = await _dbService.GetObjectByIdAsync(user.Id);
            // Hvis brugeren ikke findes, 
            if (existingUser == null)
           
            {
                // Kastes der en exception.
                throw new InvalidOperationException("User not found.");
            }


            // Hvis brugernes Id'et matcher
            if (existingUser.Id == user.Id)
            {
                // Initialiserer en PasswordHasher
                var passwordHasher = new PasswordHasher<string>();
                // Alle de informationer brugeren kan opdatere
                existingUser.Email = user.Email;
                existingUser.Password = passwordHasher.HashPassword(null, user.Password);
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Phone = user.Phone;
                existingUser.Address = user.Address;

                // Den eksisterende bruger opdatateres i databasen.
                await _dbService.UpdateObjectAsync(existingUser);
            }
        }

        // Asynkron metode til at slette en bruger
        public async Task<User> DeleteUser(User user)
        {
            if (user == null || user.Id <= 0)
            {
                throw new ArgumentException("Invalid user or user Id!");
            }

            // Henter den eksisterende bruger fra databasen
            var existingUser = await _dbService.GetObjectByIdAsync(user.Id);
            // Hvis brugeren ikke findes, 
            if (existingUser == null)
            {
                //Kastes der en exception
                throw new InvalidOperationException("User not found");
            }

            // Hvis brugernes id'et matcher
            if (existingUser.Id == user.Id)
            {
                // Slettes brugeren fra databasen
                await _dbService.DeleteObjectAsync(existingUser);
                //Retunere den ekisterende bruger
                return existingUser;
            }
            //Returnerer null hvis ID'er ikke matcher
            return null;
        }

        public Task<bool> EmailInUseAsync(string email) //Metode bruges i create user til at tjekke om en bruger allerede bruger den indtastede email
        {
            return Task.FromResult(_users.Any(u => u.Email == email)); //Returnerer true hviss den finder en user med den indtastede email


        }

    }
}
