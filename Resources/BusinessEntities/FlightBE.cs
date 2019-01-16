using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class FlightBE
    {
        [Key]
        public int Id { get; set; }
        public int FkDeparture { get; set; }
        public int FkArrival { get; set; }
        public string FkUser { get; set; }
        public int FkAirCraft { get; set; }
        public double? Distance { get; set; }
        public double? TripFuelConsumption { get; set; }
        public string DistanceUnit { get; set; }
    }
}