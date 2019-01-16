using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLogicInterfaces
{
    public interface IAirPortBL : IDisposable
    {
        Task<List<AirPortBE>> GetAirportsBL();
        Task<AirPortBE> GetAirportByIdBL(int idAirport);
        Task<AirPortBE> GetAirportByCodeBL(string codeAirport);
        Task AddAirportBL(AirPortBE airport, CancellationToken token);
        Task EditAirportBL(AirPortBE airport, CancellationToken token);
        Task DeleteAirportBL(AirPortBE airport, CancellationToken token);     
    }
}