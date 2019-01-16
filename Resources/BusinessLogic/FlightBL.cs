using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessLogicInterfaces;
using Core;
using Core.Interfaces;
using DataAccess;
using DataAccessInterfaces;

namespace BusinessLogic
{
    public class FlightBL : IFlightBL
    {
        private ILogError _logError;
        private IFlightDA _data = null;

        public FlightBL(IFlightDA data, ILogError logError)
        {
            _data = data;
            _logError = logError;
        }

        public async Task<List<FlightBE>> GetFlightsBL()
        {
            try
            {
                var flights = await _data.GetFlights();
                return flights;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new List<FlightBE>();
            }
        }

        public async Task<FlightBE> GetFlightByIdBL(int idFlight)
        {
            try
            {
                var flight = await _data.GetFlightById(idFlight);
                return flight;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new FlightBE();
            }
        }

        public async Task AddFlightBL(FlightBE flight, CancellationToken token)
        {
            try
            {
                if(flight.FkDeparture == null || flight.FkArrival == null || flight.FkUser == null || flight.FkAirCraft == null)
                {
                    throw new ArgumentException();
                }

                _data.AddFlight(flight, token);
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }

        public async Task EditFlightBL(FlightBE flight, CancellationToken token)
        {
            try
            {
                if(flight.FkDeparture == null || flight.FkArrival == null || flight.FkUser == null || flight.FkAirCraft == null)
                {
                    throw new ArgumentException();
                }

                await _data.EditFlight(flight, token);
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }

        public async Task DeleteFlightBL(FlightBE flight, CancellationToken token)
        {
            try
            {
                await _data.DeleteFlight(flight, token);
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }

        public void Dispose()
        {
            try
            {
                if(_data != null)
                {
                    _data.Dispose();
                }
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }       
    }
}