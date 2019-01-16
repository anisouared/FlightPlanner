using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BusinessEntities;

namespace BusinessLogicInterfaces
{
    public interface IUserBL : IDisposable, IUserStore<UserBE>, IUserEmailStore<UserBE>, IUserPasswordStore<UserBE>, IUserRoleStore<UserBE>
    {
        Task<List<UserBE>> GetUsersBL();
        Task<UserBE> GetUserByUserNameAndEmailBL(string userName, string email, CancellationToken token);
        Task<UserBE> GetUserByUserNameBL(string userName, CancellationToken token);     
    }
}