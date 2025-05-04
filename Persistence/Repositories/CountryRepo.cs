using HotelListing.API.Core.Domain;
using HotelListing.API.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Persistence.Repositories
{
    public class CountryRepo: GenericRepo<Country>,ICountryRepo
    {
        private readonly HotelDbContext _context;

        public CountryRepo(HotelDbContext context) : base(context)
        {
            _context = context;
        }
        //secific operations or business rule
        //public async Task<Country> GetAsync(int id)
        //{
        //    var country = await 
        //}
        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(c => c.Hotels).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
