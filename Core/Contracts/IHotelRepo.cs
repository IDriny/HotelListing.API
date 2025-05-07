using HotelListing.API.Core.Domain;
using HotelListing.API.Core.Repository;

namespace HotelListing.API.Core.Contracts
{
    
    public interface IHotelRepo:IGenericRepo<Hotel>
    {
        public Task<Hotel> GetHotelAsync (int id);
    }
}
