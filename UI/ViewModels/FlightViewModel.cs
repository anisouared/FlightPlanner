using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BusinessEntities;

namespace UI.ViewModels
{
    public class FlightViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Aeroport depart :")]
        public string DepartureAirPortName { get; set; }
        [Display(Name = "Aeroport arrivee :")]
        public string ArrivalAirPortName { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string AddedBy { get; set; }
        public string ModelPlane { get; set; }
        public double DistanceByKM { get; set; }
        public double FuelByKg { get; set; }
        [Display(Name = "Code aeroport depart :")]
        public string DepartureAirPortCode { get; set; }
        [Display(Name = "Code aeroport arrivee :")]
        public string ArrivalAirPortCode { get; set; }
        public List<AirCraftBE> AirCrafts { get; set; }
        [Display(Name = "Type d'avion :")]
        public int AirCraftId { get; set; }
       
    }
}