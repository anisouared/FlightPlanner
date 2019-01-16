using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class AirCraftBE
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }       
        public string ConsumptionUnit { get; set; }
        public double? FuelConsumptionTakeoff { get; set; }
    }
}