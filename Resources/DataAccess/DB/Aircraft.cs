using System;
using System.Collections.Generic;

namespace DataAccess.DB
{
    public partial class Aircraft
    {
        public Aircraft()
        {
            Flight = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public double? FuelConsumptionTakeoff { get; set; }
        public string ConsumptionUnit { get; set; }

        public ICollection<Flight> Flight { get; set; }
    }
}
