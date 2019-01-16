using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataAccess.DB;

namespace DataAccess.Mapping
{
    public class FlightMap
    {
        internal static List<FlightBE> Map(List<Flight> flights)
        {
            if(flights == null || flights.Count() == 0)
            {
                return new List<FlightBE>();
            }

            List<FlightBE> result = new List<FlightBE>();

            foreach(Flight flight in flights)
            {
                result.Add(Map(flight));
            }

            return result;
        }

        internal static FlightBE Map(Flight flight)
        {
            if (flight == null)
            {
                return null;
            }

            FlightBE result = new FlightBE();

            result.Id = flight.Id;
            result.FkDeparture = flight.FkDeparture;
            result.FkArrival = flight.FkArrival;
            result.FkUser = flight.FkUser;
            result.FkAirCraft = flight.FkAircraft;
            result.Distance = flight.Distance;
            result.DistanceUnit = flight.DistanceUnit;
            result.TripFuelConsumption = flight.TripFuelConsumption;

            return result;
        }

        internal static Flight Map(FlightBE flightBE, Flight result = null)
        {
            if(result == null)
            {
                result = new Flight();
            }

            result.Id = flightBE.Id;
            result.FkDeparture = flightBE.FkDeparture;
            result.FkArrival = flightBE.FkArrival;
            result.FkUser = flightBE.FkUser;
            result.FkAircraft = flightBE.FkAirCraft;
            result.Distance = flightBE.Distance;
            result.DistanceUnit = flightBE.DistanceUnit;
            result.TripFuelConsumption = flightBE.TripFuelConsumption;

            return result;
        }
    }
}