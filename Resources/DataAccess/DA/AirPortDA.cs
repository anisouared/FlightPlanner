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
    public class AirPortDA : IAirPortDA
    {
        private FlightPlannerDBContext _flightplanner_entities = null;

        public AirPortDA(FlightPlannerDBContext context)
        {
            _flightplanner_entities = context;
        }

        public async Task<List<AirPortBE>> GetAirports()
        {
            var airports = await _flightplanner_entities.Airport.ToListAsync();
            return AirPortMap.Map(airports);
        }

        public async Task<AirPortBE> GetAirportById(int idAirport)
        {
            var airport = await _flightplanner_entities.Airport.Where(x => x.Id == idAirport).FirstOrDefaultAsync();
            return AirPortMap.Map(airport);
        }

        public async Task<AirPortBE> GetAirportByCode(string codeAirport)
        {
            var airport = await _flightplanner_entities.Airport.Where(x => x.Code == codeAirport).FirstOrDefaultAsync();
            return AirPortMap.Map(airport);
        }

        public async Task AddAirport(AirPortBE airport, CancellationToken token)
        {
            _flightplanner_entities.Airport.Add(AirPortMap.Map(airport));
            await Save(token);
        }

        public async Task EditAirport(AirPortBE airport, CancellationToken token)
        {
            var existingAirport = await _flightplanner_entities.Airport.Where(x => x.Id == airport.Id).FirstOrDefaultAsync();

            if(existingAirport != null)
            {
                AirPortMap.Map(airport, existingAirport);
                await Save(token);
            }
        }

        public async Task DeleteAirport(AirPortBE airport, CancellationToken token)
        {
            var existingAirport = await _flightplanner_entities.Airport.Where(x => x.Id == airport.Id).FirstOrDefaultAsync();

            if (existingAirport != null)
            {
                _flightplanner_entities.Airport.Remove(existingAirport);
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