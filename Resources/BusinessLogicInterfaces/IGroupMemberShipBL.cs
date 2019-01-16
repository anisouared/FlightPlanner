using BusinessEntities;
using Core;
using DataAccess;
using DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;


namespace BusinessLogicInterfaces
{
    public interface IGroupMemberShipBL : IDisposable, IRoleStore<GroupMemberShipBE>
    {
        Task<List<GroupMemberShipBE>> GetGroups();
    }
}

