using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLogicInterfaces
{
    public interface IFlightBL : IDisposable
    {
        Task<List<FlightBE>> GetFlightsBL();
        Task<FlightBE> GetFlightByIdBL(int idFlight);
        Task AddFlightBL(FlightBE flight, CancellationToken token);
        Task EditFlightBL(FlightBE flight, CancellationToken token);
        Task DeleteFlightBL(FlightBE flight, CancellationToken token);
    }
}