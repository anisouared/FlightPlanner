using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;

namespace DataAccessInterfaces
{
    public interface IUserDA : IDisposable
    {
        Task<List<UserBE>> GetUsers();
        Task<UserBE> GetUserById(string idUser);
        Task<UserBE> GetUserByUserNameAndEmail(string userName, string email, CancellationToken token);
        Task<UserBE> GetUserByUserName(string userName, CancellationToken token);
        Task<UserBE> GetUserByNormalizedUserName(string normalizedUserName, CancellationToken token);
        Task<UserBE> GetUserByEmail(string normalizedEmail, CancellationToken token);
        Task AddUser(UserBE user, CancellationToken token);
        Task EditUser(UserBE user, CancellationToken token);
        Task DeleteUser(UserBE user, CancellationToken token);
        Task Save(CancellationToken token);
    }
}