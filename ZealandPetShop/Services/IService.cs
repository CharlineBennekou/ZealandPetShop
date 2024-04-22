namespace ZealandPetShop.Services
{
    public interface IService<T>
    {

        public interface IService<T>
        {
            Task<IEnumerable<T>> GetObjectsAsync();
            Task AddObjectAsync(T obj);
            Task DeleteObjectAsync(T obj);
            Task UpdateObjectAsync(T obj);
            Task<T> GetObjectByIdAsync(int id);
        }
    }
}
