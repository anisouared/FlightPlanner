using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BusinessEntities;

namespace UI.ViewModels
{
    public class FlightToAddViewModel
    {
        public string CodeFlightDeparture { get; set; }
        public string CodeFlightArrival { get; set; }
        public int FkAirCraft { get; set; }
        public List<AirCraftBE> AirCrafts { get; set; }       
    }
}