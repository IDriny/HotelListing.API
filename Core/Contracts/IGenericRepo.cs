using HotelListing.API.Core.Domain;

namespace HotelListing.API.Core.Repository
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> GetAsync(int? id);

        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<bool> Exists(int id);
    }

    
}
