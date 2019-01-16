using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLogicInterfaces
{
    public interface IAirCraftBL : IDisposable
    {
        Task<List<AirCraftBE>> GetAircraftsBL();
        Task<AirCraftBE> GetAircraftByIdBL(int idAircraft);
        Task AddAircraftBL(AirCraftBE aircraft, CancellationToken token);
        Task EditAircraftBL(AirCraftBE aircraft, CancellationToken token);
        Task DeleteAircraftBL(AirCraftBE aircraft, CancellationToken token);
    }
}