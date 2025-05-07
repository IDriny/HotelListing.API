namespace HotelListing.API.Models.Hotel;

public abstract class BaseHotelModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }

    public int CountryId { get; set; }
}