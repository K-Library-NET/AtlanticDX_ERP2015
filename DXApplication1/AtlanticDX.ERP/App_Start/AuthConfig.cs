using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PrivilegeFramework;
using System.Collections.Generic;
using YuShang.ERP.Entities.Privileges;

namespace AtlanticDX.Model
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }

    public class IdentityManager
    {
        public IdentityManager()
        {
            this._db = new AtlanticDXContext();
            this._userManager =
                new ApplicationUserManager(
                    new UserStore<YuShang.ERP.Entities.Privileges.SysUser, SysRole,
                            int, SysUserLogin, SysUserRole, SysUserClaim>(
                            new AtlanticDXContext()), new AtlanticDXContext());
            this._roleManager =
                new ApplicationRoleManager(
                    new AtlanticDXContext());
        }

        public IdentityManager(PrivilegeFramework.ExtendedIdentityDbContext db)
        {
            this._db = db;
            this._userManager =
                new ApplicationUserManager(
                    new UserStore<YuShang.ERP.Entities.Privileges.SysUser, SysRole,
                            int, SysUserLogin, SysUserRole, SysUserClaim>(
                            this._db), this._db);
            this._roleManager = new ApplicationRoleManager(this._db);
        }

        PrivilegeFramework.ExtendedIdentityDbContext _db = null;

        // Swap ApplicationRole for IdentityRole:
        ApplicationRoleManager _roleManager = null;

        ApplicationUserManager _userManager = null;

        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager;
            }
        }

        public bool CreateRole(string name, int parentId = 0, int privilegeLevel = 50)
        {
            // Swap ApplicationRole for IdentityRole:
            var idResult = _roleManager.Create(new SysRole()
            {
                Name = name,
                ParentId = parentId,
                PrivilegeLevel = privilegeLevel
            });
            return idResult.Succeeded;
        }


        public bool CreateUser(SysUser user, string password)
        {
            var idResult = _userManager.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(int userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(int userId)
        {
            var user = _userManager.FindById(userId);
            var currentRoles = new List<SysUserRole>();

            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                _userManager.RemoveFromRole(userId,
                    _roleManager.FindById(role.RoleId).Name);
            }
        }
    }
}