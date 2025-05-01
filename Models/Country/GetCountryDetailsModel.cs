using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Models.Country
{
    public class GetCountryDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public List<GetHotelModel> Hotels { get; set; }
    }
}
