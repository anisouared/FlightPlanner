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
    public class AirCraftDA : IAirCraftDA
    {
        private FlightPlannerDBContext _flightplanner_entities = null;

        public AirCraftDA(FlightPlannerDBContext context)
        {
            _flightplanner_entities = context;
        }

        public async Task<List<AirCraftBE>> GetAircrafts()
        {
            var aircrafts = await _flightplanner_entities.Aircraft.ToListAsync();
            return AirCraftMap.Map(aircrafts);
        }

        public async Task<AirCraftBE> GetAircraftById(int idAircraft)
        {
            var aircraft = await _flightplanner_entities.Aircraft.Where(x => x.Id == idAircraft).FirstOrDefaultAsync();
            return AirCraftMap.Map(aircraft);
        }

        public async Task AddAircraft(AirCraftBE aircraft, CancellationToken token)
        {
            _flightplanner_entities.Aircraft.Add(AirCraftMap.Map(aircraft));
            await Save(token);
        }

        public async Task EditAircraft(AirCraftBE aircraft, CancellationToken token)
        {
            var existingAircraft = await _flightplanner_entities.Aircraft.Where(x => x.Id == aircraft.Id).FirstOrDefaultAsync();

            if(existingAircraft != null)
            {
                AirCraftMap.Map(aircraft, existingAircraft);
                await Save(token);
            }
        }

        public async Task DeleteAircraft(AirCraftBE aircraft, CancellationToken token)
        {
            var existingAircraft = await _flightplanner_entities.Aircraft.Where(x => x.Id == aircraft.Id).FirstOrDefaultAsync();

            if (existingAircraft != null)
            {
                _flightplanner_entities.Aircraft.Remove(existingAircraft);
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