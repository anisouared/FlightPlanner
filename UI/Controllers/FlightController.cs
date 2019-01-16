using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core;
using Core.Interfaces;
using DataAccessInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BusinessLogicInterfaces;
using DataAccess.DB;
using UI.ViewModels;
using BusinessEntities;
using BusinessLogic;

namespace UI.Controllers
{
    public class FlightController : Controller
    {
        private ILogError _logError;
        private IFlightDA _flightData;
        private IAirPortDA _airportData;
        private IAirCraftDA _aircraftData;
        private IUserDA _userData;

        private IAirPortBL _airportBL;
        private IAirCraftBL _aircraftBL;
        private IFlightBL _flightBL;
        private IUserBL _userBL;

        public FlightController(IFlightDA flightData, IAirPortDA airportData, IAirCraftDA aircraftData, IUserDA userData, ILogError logError)
        {
            _logError = logError;
            _flightData = flightData;
            _airportData = airportData;
            _aircraftData = aircraftData;
            _userData = userData;

            _flightBL = new FlightBL(_flightData, _logError);
            _airportBL = new AirPortBL(_airportData, _logError);
            _aircraftBL = new AirCraftBL(_aircraftData, _logError);
            _userBL = new UserBL(_userData, _logError);
        }

        [Authorize]
        public async Task<IActionResult> AddFlight()
        {
            FlightToAddViewModel flightToAddViewModel = new FlightToAddViewModel();
            flightToAddViewModel.AirCrafts = await _aircraftBL.GetAircraftsBL();
            return View(flightToAddViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFlight(FlightToAddViewModel flightToAddViewModel, CancellationToken token)
        {
            AirPortBE departureAirport = new AirPortBE();
            AirPortBE arrivalAirport = new AirPortBE();
            AirCraftBE aircraftUsed = new AirCraftBE();

            FlightBE flightToAdd = new FlightBE();

            string idCurrentUserString;
            double distanceTrip;
            double tripfuelConsumption;

            idCurrentUserString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            departureAirport = await _airportBL.GetAirportByCodeBL(flightToAddViewModel.CodeFlightDeparture);
            arrivalAirport = await _airportBL.GetAirportByCodeBL(flightToAddViewModel.CodeFlightArrival);
            aircraftUsed = await _aircraftBL.GetAircraftByIdBL(flightToAddViewModel.FkAirCraft);
            
            /* We are calculating the distance between two airports using the latitude/longitude of those points */
            distanceTrip = distance(double.Parse(departureAirport.Latitude, System.Globalization.CultureInfo.InvariantCulture), double.Parse(departureAirport.Longitude), double.Parse(arrivalAirport.Latitude), double.Parse(arrivalAirport.Longitude), 'K');

            /* We have the information of the consumption of each aircraft in Kilogram by 1000KM in the database,
             so we are doing a simple rule of three to have the consomation for the distance traveled,
             in addition to that we add the additional fuel consumption during the takeoff for each aircraft */
            tripfuelConsumption = ( (aircraftUsed.FuelConsumption * distanceTrip) / 1000 ) + (double)aircraftUsed.FuelConsumptionTakeoff;

            flightToAdd.FkDeparture = departureAirport.Id;
            flightToAdd.FkArrival = arrivalAirport.Id;
            flightToAdd.FkUser = idCurrentUserString;
            flightToAdd.FkAirCraft = aircraftUsed.Id;
            flightToAdd.Distance = distanceTrip;
            flightToAdd.TripFuelConsumption = tripfuelConsumption;
            flightToAdd.DistanceUnit = "KM"; // To put in a config file

            await _flightBL.AddFlightBL(flightToAdd, token);

            return RedirectToAction("AllFlights", "Flight");
        }

        [Authorize]
        public async Task<IActionResult> UserFlights(CancellationToken token)
        {
            string idCurrentUserString;
            List<FlightViewModel> flightsViewModelList = new List<FlightViewModel>();
            idCurrentUserString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                idCurrentUserString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                idCurrentUserString = null;
            }

            if(idCurrentUserString != null)
            {
                var flightsFromBL = await _flightBL.GetFlightsBL();
                List<FlightBE> flightsFromBLFiltredByIdUser = flightsFromBL.Where(x => x.FkUser == idCurrentUserString).ToList();

                foreach(var flight in flightsFromBLFiltredByIdUser)
                {
                    FlightViewModel flightViewModel = new FlightViewModel();

                    flightViewModel.Id = flight.Id;
                    flightViewModel.DepartureAirPortName = (await _airportBL.GetAirportByIdBL(flight.FkDeparture)).Name;
                    flightViewModel.ArrivalAirPortName = (await _airportBL.GetAirportByIdBL(flight.FkArrival)).Name;
                    flightViewModel.DepartureCity = (await _airportBL.GetAirportByIdBL(flight.FkDeparture)).CityName; 
                    flightViewModel.ArrivalCity = (await _airportBL.GetAirportByIdBL(flight.FkArrival)).CityName;
                    flightViewModel.AddedBy = (await _userBL.FindByIdAsync(idCurrentUserString, token)).UserName;
                    flightViewModel.ModelPlane = (await _aircraftBL.GetAircraftByIdBL(flight.FkAirCraft)).Model;
                    flightViewModel.DistanceByKM = (double)(await _flightBL.GetFlightByIdBL(flight.Id)).Distance;
                    flightViewModel.FuelByKg = (double)(await _flightBL.GetFlightByIdBL(flight.Id)).TripFuelConsumption;

                    flightsViewModelList.Add(flightViewModel);
                }
            }
            
            return View(flightsViewModelList);
        }


        [Authorize]
        public async Task<IActionResult> EditFlight(int id, CancellationToken token)
        { 
            FlightViewModel flightviewmodel = new FlightViewModel();
            flightviewmodel.AirCrafts = await _aircraftBL.GetAircraftsBL();

            string idCurrentUserString;
            idCurrentUserString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                idCurrentUserString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                idCurrentUserString = null;
            }

            if(idCurrentUserString != null)
            {
                var flightsFromBL = await _flightBL.GetFlightsBL();
                var flight = flightsFromBL.Where(x => x.Id == id).FirstOrDefault();

                flightviewmodel.Id = flight.Id;
                flightviewmodel.DepartureAirPortName = (await _airportBL.GetAirportByIdBL(flight.FkDeparture)).Name;
                flightviewmodel.ArrivalAirPortName = (await _airportBL.GetAirportByIdBL(flight.FkArrival)).Name;
                flightviewmodel.DepartureCity = (await _airportBL.GetAirportByIdBL(flight.FkDeparture)).CityName; 
                flightviewmodel.ArrivalCity = (await _airportBL.GetAirportByIdBL(flight.FkArrival)).CityName;
                flightviewmodel.AddedBy = (await _userBL.FindByIdAsync(idCurrentUserString, token)).UserName;
                flightviewmodel.ModelPlane = (await _aircraftBL.GetAircraftByIdBL(flight.FkAirCraft)).Model;
                flightviewmodel.DistanceByKM = (double)(await _flightBL.GetFlightByIdBL(flight.Id)).Distance;
                flightviewmodel.FuelByKg = (double)(await _flightBL.GetFlightByIdBL(flight.Id)).TripFuelConsumption;
                flightviewmodel.DepartureAirPortCode = (await _airportBL.GetAirportByIdBL(flight.FkDeparture)).Code;
                flightviewmodel.ArrivalAirPortCode = (await _airportBL.GetAirportByIdBL(flight.FkArrival)).Code;
                flightviewmodel.AirCraftId = (await _aircraftBL.GetAircraftByIdBL(flight.FkAirCraft)).Id;
            }

            return View(flightviewmodel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditFlight(FlightViewModel newflightviewmodel, CancellationToken token)
        {
            newflightviewmodel.AirCrafts = await _aircraftBL.GetAircraftsBL();

            FlightBE flightToEdit = new FlightBE();
            double newDistanceTrip;
            double newTripfuelConsumption;
            string idCurrentUserString;

            AirPortBE newDepartureAirport = new AirPortBE();
            AirPortBE newArrivalAirPort = new AirPortBE();
            AirCraftBE newAirCraft = new AirCraftBE();
            idCurrentUserString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;



            newDepartureAirport = await _airportBL.GetAirportByCodeBL(newflightviewmodel.DepartureAirPortCode);
            newArrivalAirPort = await _airportBL.GetAirportByCodeBL(newflightviewmodel.ArrivalAirPortCode);
            newAirCraft = await _aircraftBL.GetAircraftByIdBL(newflightviewmodel.AirCraftId);

            
            newDistanceTrip = distance(double.Parse(newDepartureAirport.Latitude, System.Globalization.CultureInfo.InvariantCulture), double.Parse(newDepartureAirport.Longitude), double.Parse(newArrivalAirPort.Latitude), double.Parse(newArrivalAirPort.Longitude), 'K');

            newTripfuelConsumption = ( (newAirCraft.FuelConsumption * newDistanceTrip) / 1000 ) + (double)newAirCraft.FuelConsumptionTakeoff;

            flightToEdit.Id = newflightviewmodel.Id;
            flightToEdit.FkDeparture = newDepartureAirport.Id;
            flightToEdit.FkArrival = newArrivalAirPort.Id;
            flightToEdit.FkUser = idCurrentUserString;
            flightToEdit.FkAirCraft = newAirCraft.Id;
            flightToEdit.Distance = newDistanceTrip;
            flightToEdit.TripFuelConsumption = newTripfuelConsumption;
            flightToEdit.DistanceUnit = "KM"; // To put in a config file

            await _flightBL.EditFlightBL(flightToEdit, token);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> AllFlights(CancellationToken token)
        { 
            string idCurrentUserString;

            List<FlightViewModel> flightsViewModelList = new List<FlightViewModel>();

            try
            {
                idCurrentUserString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                idCurrentUserString = null;
            }
            
            if(idCurrentUserString != null)
            {
                var flightsFromBL = await _flightBL.GetFlightsBL();

                foreach(var flight in flightsFromBL)
                {
                    FlightViewModel flightViewModel = new FlightViewModel();

                    flightViewModel.DepartureAirPortName = (await _airportBL.GetAirportByIdBL(flight.FkDeparture)).Name;
                    flightViewModel.ArrivalAirPortName = (await _airportBL.GetAirportByIdBL(flight.FkArrival)).Name;
                    flightViewModel.DepartureCity = (await _airportBL.GetAirportByIdBL(flight.FkDeparture)).CityName; 
                    flightViewModel.ArrivalCity = (await _airportBL.GetAirportByIdBL(flight.FkArrival)).CityName;
                    flightViewModel.AddedBy = (await _userBL.FindByIdAsync(flight.FkUser, token)).UserName;
                    flightViewModel.ModelPlane = (await _aircraftBL.GetAircraftByIdBL(flight.FkAirCraft)).Model;
                    flightViewModel.DistanceByKM = (double)(await _flightBL.GetFlightByIdBL(flight.Id)).Distance;
                    flightViewModel.FuelByKg = (double)(await _flightBL.GetFlightByIdBL(flight.Id)).TripFuelConsumption;

                    flightsViewModelList.Add(flightViewModel);
                }
            }

            return View(flightsViewModelList);
        }

        //::  This fonction calculates the distance between two points using the latitude/longitude of those points
        private double distance(double lat1, double lon1, double lat2, double lon2, char unit) 
        {
            if ((lat1 == lat2) && (lon1 == lon2)) 
            {
                return 0;
            }
            else 
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K') {
                dist = dist * 1.609344;
                } else if (unit == 'N') {
                dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        //::  This function converts decimal degrees to radians
        private double deg2rad(double deg) 
        {
            return (deg * Math.PI / 180.0);
        }

        //::  This function converts radians to decimal degrees
        private double rad2deg(double rad) 
        {
            return (rad / Math.PI * 180.0);
        }        
        
    }
}