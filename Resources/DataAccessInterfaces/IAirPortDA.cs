using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;


namespace DataAccessInterfaces
{
    public interface IAirPortDA : IDisposable
    {
        Task<List<AirPortBE>> GetAirports();
        Task<AirPortBE> GetAirportById(int idAirport);
        Task<AirPortBE> GetAirportByCode(string codeAirport);
        Task AddAirport(AirPortBE airport, CancellationToken token);
        Task EditAirport(AirPortBE airport, CancellationToken token);
        Task DeleteAirport(AirPortBE airport, CancellationToken token);
        Task Save(CancellationToken token);
    }
}