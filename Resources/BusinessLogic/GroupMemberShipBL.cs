using BusinessEntities;
//using fwCore;
using DataAccess;
using DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicInterfaces;
using Microsoft.AspNetCore.Identity;
using Core.Interfaces;



namespace BusinessLogic
{
    public class GroupMemberShipBL : IGroupMemberShipBL
    {
        private IGroupMemberShipDA _data = null;
        private ILogError _logError;

        //public GroupMemberShipBL()
        //    :this(new GroupMemberShipDA(), null)
        //{
        //}

        public GroupMemberShipBL(IGroupMemberShipDA data, ILogError logError)
        {
            _data = data;
            _logError = logError;
        }

        public async Task<List<GroupMemberShipBE>> GetGroups()
        {
            try
            {
                var groups = await _data.GetGroups();
                return groups;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return new List<GroupMemberShipBE>();
            }
        }

        /* public async Task<GroupMemberShipBE> GetGroupInFr(string typeFr)
        {
            try
            {
                var group = await _data.GetGroupInFr(typeFr);
                return group;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return new GroupMemberShipBE(null);
            }
        }

        public async Task<GroupMemberShipBE> GetGroupInEn(string typeEn)
        {
            try
            {
                var group = await _data.GetGroupInEn(typeEn);
                return group;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return new GroupMemberShipBE(null);
            }
        }*/

        public async Task<GroupMemberShipBE> FindByIdAsync(String groupIdString, CancellationToken token = default(CancellationToken))
        {
            int groupId;

            bool resultParsing = Int32.TryParse(groupIdString, out groupId);

            if(resultParsing)
            {
                try
                {
                    var group = await _data.GetGroupById(groupId);
                    return group;
                }
                catch (Exception ex)
                {
                    _logError.Log(ex);
                    return null;
                }
            }
            return null;
        }

        public async Task<IdentityResult> CreateAsync(GroupMemberShipBE group, CancellationToken token)
        {
            try
            {
                //if(string.IsNullOrEmpty(group.TypeFr)
                //|| string.IsNullOrEmpty(group.TypeEn))
                //{
                //    throw new ArgumentException();
                //}

                await _data.AddGroup(group, token);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return IdentityResult.Failed(new IdentityError { Description = ex.ToString()});
            }
        }

        public async Task<IdentityResult> UpdateAsync(GroupMemberShipBE group, CancellationToken token)
        {
            try
            {
                await _data.EditGroup(group, token);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return IdentityResult.Failed(new IdentityError { Description = ex.ToString()});
            }
        }

        public async Task<IdentityResult> DeleteAsync(GroupMemberShipBE group, CancellationToken token)
        {
            try
            {
                await _data.DeleteGroup(group, token);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return IdentityResult.Failed(new IdentityError { Description = ex.ToString()});
            }
        }


        public async Task<GroupMemberShipBE> FindByNameAsync(String normalizedRoleName, CancellationToken token)
        {
            try
            {
                var role = await _data.GetRoleByNormalizedRoleName(normalizedRoleName, token);
                return role;
            }
            catch (Exception ex)
            {
                _logError.Log(ex);
                return null;
            }
        }

        public async Task<string> GetNormalizedRoleNameAsync(GroupMemberShipBE group, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRoleIdAsync(GroupMemberShipBE group, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(GroupMemberShipBE group, CancellationToken token)
        {
            return Task.FromResult(group.Name);
        }

        public Task SetNormalizedRoleNameAsync(GroupMemberShipBE group, string normalizedName, CancellationToken token)
        {
            group.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

         public async Task SetRoleNameAsync(GroupMemberShipBE group, String groupName, CancellationToken token)
        {
            throw new NotImplementedException();
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

    }
}
    /* 
    public class GroupMemberShipBL : RoleProvider, IDisposable
    {
        private IGroupMemberShipDA _data = null;

        public GroupMemberShipBL()
            : this(new GroupMemberShipDA())
        {

        }

        public GroupMemberShipBL(IGroupMemberShipDA data)
        {
            _data = data;
        }

        public async Task<List<GroupMemberShipBE>> GetGroups()
        {
            try
            {
                var groups = await _data.GetGroups();
                return groups;
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                return new List<GroupMemberShipBE>();
            }
        }
        
        public async Task<GroupMemberShipBE> GetGroupById(int idGroup)
        {
            try
            {
                var group = await _data.GetGroupById(idGroup);
                return group;
            }
            catch(Exception ex)
            {
                LogError.Log(ex);
                return new GroupMemberShipBE();
            }
        }

        public async Task<GroupMemberShipBE> GetGroupInFr(string typeFr)
        {
            try
            {
                var group = await _data.GetGroupInFr(typeFr);
                return group;
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                return new GroupMemberShipBE();
            }
        }

        public async Task<GroupMemberShipBE> GetGroupInEn(string typeEn)
        {
            try
            {
                var group = await _data.GetGroupInEn(typeEn);
                return group;
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                return new GroupMemberShipBE();
            }
        }

        public void AddGroup(GroupMemberShipBE group)
        {
            try
            {
                if(string.IsNullOrEmpty(group.TypeFr)
                || string.IsNullOrEmpty(group.TypeEn))
                {
                    throw new ArgumentException();
                }

                _data.AddGroup(group);
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
            }
        }

        public async Task EditGroup(GroupMemberShipBE group)
        {
            try
            {
                if (string.IsNullOrEmpty(group.TypeFr)
                || string.IsNullOrEmpty(group.TypeEn))
                {
                    throw new ArgumentException();
                }

                await _data.EditGroup(group);
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
            }
        }

        public async Task DeleteGroup(GroupMemberShipBE group)
        {
            try
            {
                if(group.Id < 1)
                {
                    throw new ArgumentException();
                }

                await _data.DeleteGroup(group);
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
            }
        }

        public async Task Save(CancellationToken token)
        {
            try
            {
                await _data.Save(token);
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var task = Task.Run(async () =>
            {
                try
                {
                    return await _data.GetGroupsForUser(username);
                }
                catch (Exception ex)
                {
                    LogError.Log(ex);
                    return null;
                }
            });

            var result = task.Result;
            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            try
            {
                _data.Dispose();
            }
            catch(Exception ex)
            {
                LogError.Log(ex);
            }
        }
    }
}*/
