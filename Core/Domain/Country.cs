﻿using Microsoft.Build.Framework;

namespace HotelListing.API.Core.Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public IList<Hotel> Hotels { get; set; } = [];



    }
}
