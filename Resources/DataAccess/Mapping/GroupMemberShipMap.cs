using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DB;

namespace DataAccess.Mapping
{
    public static class GroupMemberShipMap
    {
        internal static List<GroupMemberShipBE> Map(List<GroupMembership> groupsmembership)
        {
            if(groupsmembership == null || groupsmembership.Count == 0)
            {
                return new List<GroupMemberShipBE>();
            }

            List<GroupMemberShipBE> result = new List<GroupMemberShipBE>();

            foreach (GroupMembership group in groupsmembership)
            {
                result.Add(Map(group));
            }

            return result;
        }

        internal static GroupMemberShipBE Map(GroupMembership groupmembership)
        {
            if(groupmembership == null)
            {
                return null;
            }

            GroupMemberShipBE result = new GroupMemberShipBE(groupmembership.Name);

            result.Id = groupmembership.Id;
            result.Name = groupmembership.Name;
            result.NormalizedName = groupmembership.NormalizedName;
            result.ConcurrencyStamp = groupmembership.ConcurrencyStamp;

            return result;
        }

        internal static GroupMembership Map(GroupMemberShipBE groupmembershipBE, GroupMembership result = null)
        {
            if(result == null)
            {
                result = new GroupMembership();
            }

            result.Id = groupmembershipBE.Id;
            result.Name = groupmembershipBE.Name;
            result.NormalizedName = groupmembershipBE.NormalizedName;
            result.ConcurrencyStamp = groupmembershipBE.ConcurrencyStamp;

            return result;
        }
    }
}
