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
    public class AirCraftBL : IAirCraftBL
    {
        private ILogError _logError;
        private IAirCraftDA _data = null;

        public AirCraftBL(IAirCraftDA data, ILogError logError)
        {
            _data = data;
            _logError = logError;
        }

        public async Task<List<AirCraftBE>> GetAircraftsBL()
        {
            try
            {
                var aircrafts = await _data.GetAircrafts();
                return aircrafts;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new List<AirCraftBE>();
            }
        }

        public async Task<AirCraftBE> GetAircraftByIdBL(int idAircraft)
        {
            try
            {
                var aircraft = await _data.GetAircraftById(idAircraft);
                return aircraft;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new AirCraftBE();
            }
        }

        public async Task AddAircraftBL(AirCraftBE aircraft, CancellationToken token)
        {
            try
            {
                if(string.IsNullOrEmpty(aircraft.Model) || aircraft.FuelConsumption == null)
                {
                    throw new ArgumentException();
                }

                _data.AddAircraft(aircraft, token);
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }

        public async Task EditAircraftBL(AirCraftBE aircraft, CancellationToken token)
        {
            try
            {
                if(string.IsNullOrEmpty(aircraft.Model) || aircraft.FuelConsumption == null)
                {
                    throw new ArgumentException();
                }

                await _data.EditAircraft(aircraft, token);
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }

        public async Task DeleteAircraftBL(AirCraftBE aircraft, CancellationToken token)
        {
            try
            {
                await _data.DeleteAircraft(aircraft, token);
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