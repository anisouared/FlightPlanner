using System;
using System.Collections.Generic;

namespace DataAccess.DB
{
    public partial class Flight
    {
        public int Id { get; set; }
        public int FkDeparture { get; set; }
        public int FkArrival { get; set; }
        public string FkUser { get; set; }
        public int FkAircraft { get; set; }
        public double? Distance { get; set; }
        public string DistanceUnit { get; set; }
        public double? TripFuelConsumption { get; set; }

        public Aircraft FkAircraftNavigation { get; set; }
        public Airport FkArrivalNavigation { get; set; }
        public Airport FkDepartureNavigation { get; set; }
        public User FkUserNavigation { get; set; }
    }
}
