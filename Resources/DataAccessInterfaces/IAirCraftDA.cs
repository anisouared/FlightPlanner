using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;


namespace DataAccessInterfaces
{
    public interface IAirCraftDA : IDisposable
    {
        Task<List<AirCraftBE>> GetAircrafts();
        Task<AirCraftBE> GetAircraftById(int idAircraft);
        Task AddAircraft(AirCraftBE aircraft, CancellationToken token);
        Task EditAircraft(AirCraftBE aircraft, CancellationToken token);
        Task DeleteAircraft(AirCraftBE aircraft, CancellationToken token);
        Task Save(CancellationToken token);
    }
}