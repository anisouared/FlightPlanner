using System;
using System.Collections.Generic;

namespace DataAccess.DB
{
    public partial class Airport
    {
        public Airport()
        {
            FlightFkArrivalNavigation = new HashSet<Flight>();
            FlightFkDepartureNavigation = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string TimeZone { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int? NumAirports { get; set; }
        public bool? City { get; set; }

        public ICollection<Flight> FlightFkArrivalNavigation { get; set; }
        public ICollection<Flight> FlightFkDepartureNavigation { get; set; }
    }
}
