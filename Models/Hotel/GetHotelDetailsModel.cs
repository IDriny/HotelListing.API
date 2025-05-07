using System.ComponentModel.DataAnnotations.Schema;
using HotelListing.API.Models.Country;

namespace HotelListing.API.Models.Hotel
{
    public class GetHotelDetailsModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }


        public  string CountryName { get; set; }
    }
}
