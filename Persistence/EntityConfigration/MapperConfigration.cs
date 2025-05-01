using AutoMapper;
using HotelListing.API.Core.Domain;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Persistence.EntityConfigration
{
    public class MapperConfigration:Profile
    {
        public MapperConfigration()
        {
            CreateMap<Country, CreateCountryModel>().ReverseMap();
            CreateMap<Country, GetCountryModel>().ReverseMap();
            CreateMap<Country, GetCountryDetailsModel>().ReverseMap();
            CreateMap<Country, UpdateCountryModel>().ReverseMap();


            CreateMap<Hotel, CreateHotelModel>().ReverseMap();
            CreateMap<Hotel, GetHotelModel>().ReverseMap();
            CreateMap<Hotel, GetHotelDetailsModel>().ReverseMap();
            CreateMap<Hotel, UpdateHotelModel>().ReverseMap();
        }
    }
}
