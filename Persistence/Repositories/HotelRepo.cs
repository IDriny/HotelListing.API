using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Persistence.Repositories
{
    public class HotelRepo :GenericRepo<Hotel>,IHotelRepo
    {
        private readonly HotelDbContext _context;

        public HotelRepo(HotelDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Hotel> GetHotelAsync(int id)
        {
            return await _context.Hotels.Include(h => h.Country).FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
