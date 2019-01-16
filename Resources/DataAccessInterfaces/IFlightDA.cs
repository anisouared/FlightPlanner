using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;

namespace DataAccessInterfaces
{
    public interface IFlightDA : IDisposable
    {
        Task<List<FlightBE>> GetFlights();
        Task<FlightBE> GetFlightById(int idFlight);
        Task AddFlight(FlightBE flight, CancellationToken token);
        Task EditFlight(FlightBE flight, CancellationToken token);
        Task DeleteFlight(FlightBE flight, CancellationToken token);
        Task Save(CancellationToken token);
    }
}