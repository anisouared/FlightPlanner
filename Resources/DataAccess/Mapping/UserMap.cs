using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataAccess.DB;

namespace DataAccess.Mapping
{
    public class UserMap
    {
        internal static List<UserBE> Map(List<User> users)
        {
            if(users == null || users.Count == 0)
            {
                return new List<UserBE>();
            }

            List<UserBE> result = new List<UserBE>();

            foreach (User user in users)
            {
                result.Add(Map(user));
            }

            return result;
        }

        internal static UserBE Map(User user)
        {
            if(user == null)
            {
                return null;
            }

            UserBE result = new UserBE();

            result.Id = user.Id;
            result.UserName = user.UserName;
            result.Email = user.Email;
            result.NormalizedUserName = user.NormalizedUserName;
            result.NormalizedEmail = user.NormalizedEmail;
            result.EmailConfirmed = user.EmailConfirmed;
            result.PasswordHash = user.PasswordHash;
            result.SecurityStamp = user.SecurityStamp;
            result.ConcurrencyStamp = user.ConcurrencyStamp;
            result.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            result.TwoFactorEnabled = user.TwoFactorEnabled;
            result.LockoutEnd = user.LockoutEnd;
            result.LockoutEnabled = user.LockoutEnabled;
            result.AccessFailedCount = user.AccessFailedCount;

            return result;
        }

        internal static User Map(UserBE userBE, User result = null)
        {
            if(result == null)
            {
                result = new User();
            }

            result.Id = userBE.Id;
            result.Email = userBE.Email;
            result.UserName = userBE.UserName;
            result.NormalizedUserName = userBE.NormalizedUserName;
            result.NormalizedEmail = userBE.NormalizedEmail;
            result.EmailConfirmed = userBE.EmailConfirmed;
            result.PasswordHash = userBE.PasswordHash;
            result.SecurityStamp = userBE.SecurityStamp;
            result.ConcurrencyStamp = userBE.ConcurrencyStamp;
            result.PhoneNumberConfirmed = userBE.PhoneNumberConfirmed;
            result.TwoFactorEnabled = userBE.TwoFactorEnabled;
            result.LockoutEnd = userBE.LockoutEnd;
            result.LockoutEnabled = userBE.LockoutEnabled;
            result.AccessFailedCount = userBE.AccessFailedCount;

            return result;
        }
    }
}