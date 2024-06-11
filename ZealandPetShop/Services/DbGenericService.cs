

using Microsoft.EntityFrameworkCore;
using ZealandPetShop.EFDbContext;

namespace ZealandPetShop.Services
{
    public class DbGenericService<T> : IService<T> where T : class
    {
       

        public async Task<IEnumerable<T>> GetAllObjectsAsync() //håndtere sql query til databasen
        {
            using (var context = new ItemDbContext()) //ny instant af itemDbContext
            {
                return await context.Set<T>().AsNoTracking().ToListAsync(); //retunere vores liste af objektet -  - fra databasen 
                    
            }
        }

        public async Task AddObjectAsync(T obj)
        {
            using (var context = new ItemDbContext()) //ny instant af itemDbContext
            {
                context.Set<T>().Add(obj); //håndtere sql query til databasen
                await context.SaveChangesAsync(); //gemmer de nye ændriger i vores database
            }
        }

        public async Task DeleteObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Remove(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateObjectAsync(T obj)
        {
            using (var context = new ItemDbContext()) //laver en istanst af ItemDbContext
            {
                context.Set<T>().Update(obj); //laver sql query til vores database for at opdatere den
                await context.SaveChangesAsync(); //gemmer de nye ændriger i vores database
            }
        }

        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new ItemDbContext()) //laver en ny istans af itemDbcontext (bliver altid oprettet ny - transient)
            {
                return await context.Set<T>().FindAsync(id); //sql query til databasen
            } //retunere den med den rigtige id (den fra metodens parameter)
        }

        public async Task SaveObjects(List<T> objs)
        {
            using (var context = new ItemDbContext())
            {
                foreach (T obj in objs)
                {
                    context.Set<T>().Add(obj);
                    context.SaveChanges();
                }

                context.SaveChanges();
            }
        }
    }
}
