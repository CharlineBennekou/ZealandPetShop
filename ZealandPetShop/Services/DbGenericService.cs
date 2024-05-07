

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ZealandPetShop.EFDbContext;

namespace ZealandPetShop.Services
{
    public class DbGenericService<T> : IService<T> where T : class
    {
        private readonly ItemDbContext _context;


        


        public async Task<IEnumerable<T>> GetAllObjectsAsync()
        {
            using (var context = new ItemDbContext()) 
            {
                return await context.Set<T>().ToListAsync();
            }
        }

        //public async Task<IEnumerable<T>> GetObjectsAsync()
        //{
        //    using (var context = new ItemDbContext())
        //    {
        //        return await context.Set<T>().AsNoTracking().ToListAsync();
                    
        //    }
        //}

        public async Task AddObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
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
            
                _context.Set<T>().Update(obj);
                await _context.SaveChangesAsync();
            
        }

        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new ItemDbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
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
