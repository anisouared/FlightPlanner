using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using BusinessEntities;
using DataAccessInterfaces;
using DataAccess.Mapping;
using DataAccess.DB;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.DA
{
    public class UserDA : IUserDA
    {
        private FlightPlannerDBContext _flightplanner_entities = null;
 
        public UserDA(FlightPlannerDBContext context)
        {
            _flightplanner_entities = context;
        }

        public async Task<List<UserBE>> GetUsers()
        {
            var userslist = await _flightplanner_entities.User.ToListAsync();
            return UserMap.Map(userslist);
        }

        public async Task<UserBE> GetUserById(string idUser)
        {
            var user = await _flightplanner_entities.User.Where(x => x.Id == idUser).FirstOrDefaultAsync();
            return UserMap.Map(user);
        }

        public async Task<UserBE> GetUserByUserNameAndEmail(string userName, string email, CancellationToken token)
        {
            var user = await _flightplanner_entities.User.Where(x => x.UserName == userName && x.Email == email).FirstOrDefaultAsync(token);
            return UserMap.Map(user);
        }

        public async Task<UserBE> GetUserByUserName(string userName, CancellationToken token)
        {
            var user = await _flightplanner_entities.User.Where(x => x.UserName == userName).FirstOrDefaultAsync(token);
            return UserMap.Map(user);
        }

        public async Task<UserBE> GetUserByEmail(string normalizedEmail, CancellationToken token)
        {
            var user = await _flightplanner_entities.User.Where(x => x.NormalizedEmail == normalizedEmail).FirstOrDefaultAsync(token);
            return UserMap.Map(user);
        }

        public async Task<UserBE> GetUserByNormalizedUserName(string normalizedUserName, CancellationToken token)
        {
            var user = await _flightplanner_entities.User.Where(x => x.NormalizedUserName == normalizedUserName).FirstOrDefaultAsync(token);
            return UserMap.Map(user);
        }

        public async Task AddUser(UserBE user, CancellationToken token)
        {
            _flightplanner_entities.User.Add(UserMap.Map(user));
            await Save(token);
        }

        public async Task EditUser(UserBE user, CancellationToken token)
        {
            var existingUser = await _flightplanner_entities.User.Where(x => x.Id == user.Id).FirstOrDefaultAsync();

            if(existingUser != null)
            {
                UserMap.Map(user, existingUser);
                await Save(token);
            }
        }

        public async Task DeleteUser(UserBE user, CancellationToken token)
        {
            var existingUser = await _flightplanner_entities.User.Where(x => x.Id == user.Id).FirstOrDefaultAsync();

            if(existingUser != null)
            {
                _flightplanner_entities.User.Remove(existingUser);
                await Save(token);
            }
        }

        public async Task Save(CancellationToken token)
        {
            await _flightplanner_entities.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            if(_flightplanner_entities != null)
            {
                _flightplanner_entities.Dispose();
            }
        }       
    }
}