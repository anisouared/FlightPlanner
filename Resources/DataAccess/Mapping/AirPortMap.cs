using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataAccess.DB;

namespace DataAccess.Mapping
{
    public class AirPortMap
    {
        internal static List<AirPortBE> Map(List<Airport> airports)
        {
            if(airports == null || airports.Count() == 0)
            {
                return new List<AirPortBE>();
            }

            List<AirPortBE> result = new List<AirPortBE>();

            foreach(Airport airport in airports)
            {
                result.Add(Map(airport));
            }

            return result;
        }

        internal static AirPortBE Map(Airport airport)
        {
            if (airport == null)
            {
                return null;
            }

            AirPortBE result = new AirPortBE();

            result.Id = airport.Id;
            result.Code = airport.Code;
            result.Name = airport.Name;
            result.CityCode = airport.CityCode;
            result.CityName = airport.CityName;
            result.CountryName = airport.CountryName;
            result.CountryCode = airport.CountryCode;
            result.TimeZone = airport.TimeZone;
            result.Latitude = airport.Lat;
            result.Longitude = airport.Lon;
            result.NumAirports = airport.NumAirports;
            result.City = airport.City;

            return result;
        }

        internal static Airport Map(AirPortBE airportBE, Airport result = null)
        {
            if(result == null)
            {
                result = new Airport();
            }

            result.Id = airportBE.Id;
            result.Code = airportBE.Code;
            result.Name = airportBE.Name;
            result.CityCode = airportBE.CityCode;
            result.CityName = airportBE.CityName;
            result.CountryName = airportBE.CountryName;
            result.CountryCode = airportBE.CountryCode;
            result.TimeZone = airportBE.TimeZone;
            result.Lat = airportBE.Latitude;
            result.Lon = airportBE.Longitude;
            result.NumAirports = airportBE.NumAirports;
            result.City = airportBE.City;

            return result;
        }
    }
}