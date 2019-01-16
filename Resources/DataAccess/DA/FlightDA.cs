using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using BusinessEntities;
using DataAccessInterfaces;
using DataAccess.Mapping;
using DataAccess.DB;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.DA
{
    public class FlightDA : IFlightDA
    {
        private FlightPlannerDBContext _flightplanner_entities = null;

        public FlightDA(FlightPlannerDBContext context)
        {
            _flightplanner_entities = context;
        }

        public async Task<List<FlightBE>> GetFlights()
        {
            var flights = await _flightplanner_entities.Flight.ToListAsync();
            return FlightMap.Map(flights);
        }

        public async Task<FlightBE> GetFlightById(int idFlight)
        {
            var flight = await _flightplanner_entities.Flight.Where(x => x.Id == idFlight).FirstOrDefaultAsync();
            return FlightMap.Map(flight);
        }

        public Task AddFlight(FlightBE flight, CancellationToken token)
        {
            _flightplanner_entities.Flight.Add(FlightMap.Map(flight));
            _flightplanner_entities.SaveChanges();
            return null;
            //await Save(token);
        }

        public async Task EditFlight(FlightBE flight, CancellationToken token)
        {
            var existingFlight = await _flightplanner_entities.Flight.Where(x => x.Id == flight.Id).FirstOrDefaultAsync();

            if(existingFlight != null)
            {
                FlightMap.Map(flight, existingFlight);
                await Save(token);
            }
        }

        public async Task DeleteFlight(FlightBE flight, CancellationToken token)
        {
            var existingFlight = await _flightplanner_entities.Flight.Where(x => x.Id == flight.Id).FirstOrDefaultAsync();

            if (existingFlight != null)
            {
                _flightplanner_entities.Flight.Remove(existingFlight);
                await Save(token);
            }
        }

        public async Task Save(CancellationToken token)
        {
            await _flightplanner_entities.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            if(_flightplanner_entities != null)
            {
                _flightplanner_entities.Dispose();
            }
        }
    }
}