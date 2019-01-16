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
    public class AirPortBL : IAirPortBL
    {
        private ILogError _logError;
        private IAirPortDA _data = null;

        public AirPortBL(IAirPortDA data, ILogError logError)
        {
            _data = data;
            _logError = logError;
        }

        public async Task<List<AirPortBE>> GetAirportsBL()
        {
            try
            {
                var airports = await _data.GetAirports();
                return airports;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new List<AirPortBE>();
            }
        }

        public async Task<AirPortBE> GetAirportByIdBL(int idAirport)
        {
            try
            {
                var airport = await _data.GetAirportById(idAirport);
                return airport;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new AirPortBE();
            }
        }

        public async Task<AirPortBE> GetAirportByCodeBL(string codeAirport)
        {
            try
            {
                var airport = await _data.GetAirportByCode(codeAirport);
                return airport;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new AirPortBE();
            }
        }

        public async Task AddAirportBL(AirPortBE airport, CancellationToken token)
        {
            try
            {
                if(string.IsNullOrEmpty(airport.Name) || string.IsNullOrEmpty(airport.CountryName))
                {
                    throw new ArgumentException();
                }

                _data.AddAirport(airport, token);
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }

        public async Task EditAirportBL(AirPortBE airport, CancellationToken token)
        {
            try
            {
                if(string.IsNullOrEmpty(airport.Name) || string.IsNullOrEmpty(airport.CountryName))
                {
                    throw new ArgumentException();
                }

                await _data.EditAirport(airport, token);
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }

        public async Task DeleteAirportBL(AirPortBE airport, CancellationToken token)
        {
            try
            {
                await _data.DeleteAirport(airport, token);
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