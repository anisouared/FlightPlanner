using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataAccess.DB;

namespace DataAccess.Mapping
{
    public class AirCraftMap
    {
        internal static List<AirCraftBE> Map(List<Aircraft> aircrafts)
        {
            if(aircrafts == null || aircrafts.Count() == 0)
            {
                return new List<AirCraftBE>();
            }

            List<AirCraftBE> result = new List<AirCraftBE>();

            foreach(Aircraft aircraft in aircrafts)
            {
                result.Add(Map(aircraft));
            }

            return result;
        }

        internal static AirCraftBE Map(Aircraft aircraft)
        {
            if (aircraft == null)
            {
                return null;
            }

            AirCraftBE result = new AirCraftBE();

            result.Id = aircraft.Id;
            result.Model = aircraft.Model;
            result.FuelConsumption = aircraft.FuelConsumption;
            result.FuelConsumptionTakeoff = aircraft.FuelConsumptionTakeoff;
            result.ConsumptionUnit = aircraft.ConsumptionUnit;

            return result;
        }

        internal static Aircraft Map(AirCraftBE aircraftBE, Aircraft result = null)
        {
            if(result == null)
            {
                result = new Aircraft();
            }

            result.Id = aircraftBE.Id;
            result.Model = aircraftBE.Model;
            result.FuelConsumption = aircraftBE.FuelConsumption;
            result.FuelConsumptionTakeoff = aircraftBE.FuelConsumptionTakeoff;
            result.ConsumptionUnit = aircraftBE.ConsumptionUnit;

            return result;
        }
    }
}