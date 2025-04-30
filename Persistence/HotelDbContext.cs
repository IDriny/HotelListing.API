using HotelListing.API.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Persistence
{
    public class HotelDbContext : DbContext
    {
        //we can also use primary constractor
        public HotelDbContext(DbContextOptions<HotelDbContext>options):
            base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
