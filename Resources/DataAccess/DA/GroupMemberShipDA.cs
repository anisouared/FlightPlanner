using DataAccess.Mapping;
using DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Security;
//using System.Data.Entity;
using BusinessEntities;
using System.Threading;
using DataAccess.DB;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.DA
{
    public class GroupMemberShipDA : IGroupMemberShipDA
    {
        private FlightPlannerDBContext _flightplanner_entities = null;

        public GroupMemberShipDA(FlightPlannerDBContext context)
        {
            _flightplanner_entities = context;
        }

        public async Task<string[]> GetGroupsForUser(string username)
        {
            /* int IdFromString;

            bool result = Int32.TryParse(username, out IdFromString);

            if(result)
            {
                var GroupSingle = await _flightplanner_entities.User.Where(x => x.Id == IdFromString).SingleOrDefaultAsync();
                string Group = GroupSingle.FkGroupMembership.TypeFr;
                string[] Groups = { Group };
                return Groups;
            }*/

            return null;
        }

        public async Task<List<GroupMemberShipBE>> GetGroups()
        {
            var groups = await _flightplanner_entities.GroupMembership.ToListAsync();
            return GroupMemberShipMap.Map(groups);
        }

        public async Task<GroupMemberShipBE> GetGroupById(int idGroup)
        {
            var group = await _flightplanner_entities.GroupMembership.Where(x => x.Id == idGroup.ToString()).FirstOrDefaultAsync();
            return GroupMemberShipMap.Map(group);
        }

        //public async Task<GroupMemberShipBE> GetGroupInFr(string typeFr)
        //{
        //    var group = await _flightplanner_entities.GroupMembership.Where(x => x.TypeFr == typeFr).FirstOrDefaultAsync();
        //    return GroupMemberShipMap.Map(group);
        //}

        //public async Task<GroupMemberShipBE> GetGroupInEn(string typeEn)
        //{
        //    var group = await _flightplanner_entities.GroupMembership.Where(x => x.TypeEn == typeEn).FirstOrDefaultAsync();
        //    return GroupMemberShipMap.Map(group);
        //}

        public async Task<GroupMemberShipBE> GetRoleByNormalizedRoleName(string normalizedRoleName, CancellationToken token)
        {
            var role = await _flightplanner_entities.GroupMembership.Where(x => x.NormalizedName == normalizedRoleName).FirstOrDefaultAsync(token);
            return GroupMemberShipMap.Map(role);
        }

        public async Task AddGroup(GroupMemberShipBE group, CancellationToken token)
        {
            _flightplanner_entities.GroupMembership.Add(GroupMemberShipMap.Map(group));
            await Save(token);
        }

        public async Task EditGroup(GroupMemberShipBE group, CancellationToken token)
        {
            var existingGroup = await _flightplanner_entities.GroupMembership.Where(x => x.Id == group.Id).FirstOrDefaultAsync();

            if(existingGroup != null)
            {
                GroupMemberShipMap.Map(group, existingGroup);
                await Save(token);
            }
        }

        public async Task DeleteGroup(GroupMemberShipBE group, CancellationToken token)
        {
            var existingGroup = await _flightplanner_entities.GroupMembership.Where(x => x.Id == group.Id).FirstOrDefaultAsync();

            if(existingGroup != null)
            {
                _flightplanner_entities.GroupMembership.Remove(existingGroup);
                await Save(token);
            }
        }

        public async Task Save(CancellationToken token)
        {
            await _flightplanner_entities.SaveChangesAsync(token);
        }


        public void Dispose()
        {
            if (_flightplanner_entities != null)
            {
                _flightplanner_entities.Dispose();
            }

        }
    }
}
