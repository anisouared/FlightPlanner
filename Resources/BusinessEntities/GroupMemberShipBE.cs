using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessEntities
{
    public class GroupMemberShipBE : IdentityRole
    {
        public GroupMemberShipBE(string roleName)
        {
            base.Name = roleName;
        }
    }
}
