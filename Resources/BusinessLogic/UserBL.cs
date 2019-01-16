using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DB;
using BusinessEntities;
using Core;
using Core.Interfaces;
using DataAccess;
using DataAccessInterfaces;
using BusinessLogicInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Identity.Core;

using Newtonsoft.Json.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;



namespace BusinessLogic
{
    public class UserBL : IUserBL
    {
        private IUserDA _data = null;
        private ILogError _logError;

        public UserBL(IUserDA data, ILogError logError)
        {
            _data = data;
            _logError = logError;
        }

        public async Task<List<UserBE>> GetUsersBL()
        {
            try
            {
                var users = await _data.GetUsers();
                return users;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new List<UserBE>();
            }
        }

        public async Task<UserBE> FindByEmailAsync(string normalizedEmail, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
    
            try
            {
                var user = await _data.GetUserByEmail(normalizedEmail, token);
                return user;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return null;                  
            }
        }

        public Task<string> GetPasswordHashAsync(UserBE user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(UserBE user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(UserBE user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetEmailAsync(UserBE user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(UserBE user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(UserBE user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(UserBE user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedEmailAsync(UserBE user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(UserBE user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public async Task<UserBE> FindByIdAsync(string userIdString, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var user = await _data.GetUserById(userIdString);
                return user;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return null;                  
            }
        }
 
        public async Task<UserBE> FindByNameAsync(string normalizedUserName, CancellationToken token)
        {
            try
            {
                var user = await _data.GetUserByNormalizedUserName(normalizedUserName, token);
                return user;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return null;
            }
        }

        public async Task<UserBE> GetUserByUserNameAndEmailBL(string userName, string email, CancellationToken token)
        {
            try
            {
                var user = await _data.GetUserByUserNameAndEmail(userName, email, token);
                return user;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return new UserBE();
            }
        }

        public async Task<UserBE> GetUserByUserNameBL(string userName, CancellationToken token)
        {
            try
            {
                var user = await _data.GetUserByUserName(userName, token);
                return user;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return new UserBE();
            }
        }

        public async Task<IdentityResult> CreateAsync(UserBE user, CancellationToken token)
        {
            try
            {
                if( string.IsNullOrEmpty(user.Email)
                    || user.Email.Count() > 100
                    || string.IsNullOrEmpty(user.UserName)
                    || user.UserName.Count() > 20
                )
                {
                    throw new ArgumentException();
                }

                await _data.AddUser(user, token);
                return IdentityResult.Success;
            } 
            catch(Exception ex)
            {
                _logError.Log(ex);
                return IdentityResult.Failed(new IdentityError { Description = ex.ToString()});
            }
        }

        public async Task<IdentityResult> UpdateAsync(UserBE user, CancellationToken token = default(CancellationToken))
        {
            try
            {
                if( string.IsNullOrEmpty(user.Email)
                    || user.Email.Count() > 100
                    || string.IsNullOrEmpty(user.UserName)
                    || user.UserName.Count() > 20
                )
                {
                    throw new ArgumentException();
                }

                await _data.EditUser(user, token);
                return IdentityResult.Success;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                return IdentityResult.Failed(new IdentityError { Description = ex.ToString()});
            }
        }

        public async Task<IdentityResult> DeleteAsync(UserBE user, CancellationToken token)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Id))
                {
                    throw new ArgumentException();
                }

                await _data.DeleteUser(user, token);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return IdentityResult.Failed(new IdentityError { Description = ex.ToString()});
            }
        }

        public Task SetNormalizedUserNameAsync(UserBE user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (normalizedName == null)
            {
                throw new ArgumentNullException(nameof(normalizedName));
            }

            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public async Task<string> GetNormalizedUserNameAsync(UserBE user, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(UserBE user, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
                cancellationToken.ThrowIfCancellationRequested();

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                if (userName == null)
                {
                    throw new ArgumentNullException(nameof(userName));
                }

                user.UserName = userName;

                return Task.CompletedTask;
        }

        public Task<string> GetUserNameAsync(UserBE user, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.UserName);
        }

        public Task<string> GetUserIdAsync(UserBE user, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Id.ToString());
        }

        public void Dispose()
        {
            try
            {
                if(_data != null)
                {
                    _data.Dispose();
                }
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
            }
        }

        public async Task AddToRoleAsync(UserBE user, string roleName, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserBE user, string roleName, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(UserBE user, CancellationToken token)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            try
            {
                IList<string> result = new List<string>();
                result.Add("Admin");
                result.Add("User");
                return result;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return null;
            }
        }

        public async Task<bool> IsInRoleAsync(UserBE user, string roleName, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        
        public Task<IList<UserBE>> GetUsersInRoleAsync(string roleName, CancellationToken token)
        {
            throw new NotImplementedException();
        }

    }
}