using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UI.ViewModels
{
    public class FlightsPerUserViewModel
    {
         public List<FlightViewModel> Flights { get; set; }       
    }
}