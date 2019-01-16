using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DataAccessInterfaces
{
    public interface IGroupMemberShipDA : IDisposable
    {
        Task<string[]> GetGroupsForUser(string username);
        Task<List<GroupMemberShipBE>> GetGroups();
        Task<GroupMemberShipBE> GetGroupById(int idGroup);
        Task<GroupMemberShipBE> GetRoleByNormalizedRoleName(string normalizedRoleName, CancellationToken token);
        Task AddGroup(GroupMemberShipBE group, CancellationToken token);
        Task EditGroup(GroupMemberShipBE group, CancellationToken token);
        Task DeleteGroup(GroupMemberShipBE group, CancellationToken token);
        Task Save(CancellationToken token);
    }
}
