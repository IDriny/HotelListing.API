using HotelListing.API.Core.Domain;

namespace HotelListing.API.Core.Repository
{
    public interface ICountryRepo : IGenericRepo<Country>
    {
        public Task<Country> GetDetails(int id);
    }
}
