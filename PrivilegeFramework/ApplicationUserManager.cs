using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using PrivilegeFramework.PrivilegeImpl;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UtilityFramework;
using YuShang.ERP.Entities.Privileges;

namespace PrivilegeFramework
{
    // 配置此应用程序中使用的应用程序用户管理器。UserManager 在 ASP.NET Identity 中定义，并由此应用程序使用。
    public class ApplicationUserManager : UserManager<YuShang.ERP.Entities.Privileges.SysUser, int>,
        IUserManagerImplWrap
    {
        public ApplicationUserManager(
            IUserStore<YuShang.ERP.Entities.Privileges.SysUser, int> store,
            ExtendedIdentityDbContext db)
            : base(store)
        {
            if (db == null)
                db = new ExtendedIdentityDbContext(
                    PrivilegeFramework.ExtendedIdentityDbContext.GetNameOrConnectionByConfig());
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db = new ExtendedIdentityDbContext(
                    PrivilegeFramework.ExtendedIdentityDbContext.GetNameOrConnectionByConfig());
            this.DbContext = db;
            this.DbContext.Configuration.ProxyCreationEnabled = false;
            this.DbContext.Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var db = context.Get<PrivilegeFramework.ExtendedIdentityDbContext>();
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
            {
                db = new ExtendedIdentityDbContext(
                     PrivilegeFramework.ExtendedIdentityDbContext.GetNameOrConnectionByConfig());
            }

            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            var manager = new ApplicationUserManager(
                new UserStore<YuShang.ERP.Entities.Privileges.SysUser, SysRole,
                    int, SysUserLogin, SysUserRole, SysUserClaim>(
                    db), db);

            (db as IObjectContextAdapter).ObjectContext.AcceptAllChanges();

            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<SysUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false,
            };

            // 配置用户锁定默认值
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并将其插入到此处。
            manager.RegisterTwoFactorProvider("电话代码", new PhoneNumberTokenProvider<SysUser, int>
            {
                MessageFormat = "你的安全代码是 {0}"
            });
            manager.RegisterTwoFactorProvider("电子邮件代码", new EmailTokenProvider<SysUser, int>
            {
                Subject = "安全代码",
                BodyFormat = "你的安全代码是 {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<SysUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(SysUser user)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await this.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }

        internal ExtendedIdentityDbContext DbContext { get; set; }

        internal IUserStore<YuShang.ERP.Entities.Privileges.SysUser, int> UserStore
        {
            get
            {
                return this.Store;
            }
        }

        #region override for DbUpdateConcurrencyException in Entity Framework with MySQL

        /// <summary>
        /// 添加用户到某个角色。
        /// override的作用：处理DbUpdateConcurrencyException，在EF+MySQL的时候都会发生，
        /// 跟MySQL的乐观锁机制有关，因为EF默认会跟踪对象状态所以存在这种问题，理论上来说去掉
        /// EF DbContext的Tracking也可以做到
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async override Task<IdentityResult> AddToRoleAsync(int userId, string role)
        {
            AddToRoleAsyncImpl impl = new AddToRoleAsyncImpl(this as IUserManagerImplWrap, this as UserManager<SysUser, int>);

            return HandleAndAction(impl as IPrivilegeAsyncImpl,
                new object[] { userId, role });

            //return this.HandleAndAction(new object[] { userId, role },
            //    this.AddToRoleCoreAsync,
            //     this.AddToRoleAsyncStringHandler1,
            //    this.AddToRoleAsyncStringHandler2);
        }

        /*
        internal string AddToRoleAsyncStringHandler1(object[] paras, DbUpdateConcurrencyException db)
        {
            return string.Format("添加用户ID {0} 到角色 {1} 时出错："
             + db.Message + "\t\t", paras[0], paras[1]);
        }

        internal string AddToRoleAsyncStringHandler2(object[] paras)
        {
            return string.Format(
                "添加用户ID {0} 到角色 {1} 时出错，有可能是无法解决DbUpdateConcurrencyException导致。"
               , paras[0], paras[1]);
        }

        internal override Task<IdentityResult> AddToRoleCoreAsync(object[] paras)
        {
            if (paras != null && paras.Length >= 2)
            {
                int userId = Convert.ToInt32(paras[0]);
                string role = paras[1].ToString();

                //如果本来已经是在角色之内，那么直接返回True
                if (this.IsInRole(userId, role))
                    return Task.Run<IdentityResult>(
                        () => { return IdentityResult.Success; }
                        );

                return base.AddToRoleAsync(userId, role);
            }
            return Task.Run<IdentityResult>(
                () => { return IdentityResult.Failed("AddToRoleCoreAsync参数不正确。"); }
                );
        }
         */

        public async override Task<IdentityResult> CreateAsync(SysUser user, string password)
        {
            CreateUserAsyncImpl impl = new CreateUserAsyncImpl(this as IUserManagerImplWrap, this as UserManager<SysUser, int>);

            return HandleAndAction(impl as IPrivilegeAsyncImpl,
                new object[] { user, password });

            //return base.CreateAsync(user, password);
        }

        //添加更多的发生DbUpdateConcurrencyException的方法

        #endregion override for DbUpdateConcurrencyException in Entity Framework with MySQL

        /// <summary>
        /// 调用基础类的方法，避免递归死循环
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityResult BaseAddToRole(int userId, string roleName)
        {
            Task<IdentityResult> taskResult = base.AddToRoleAsync(userId, roleName);
            taskResult.Wait();
            return taskResult.Result;
        }

        /// <summary>
        /// 调用基础类的方法，避免递归死循环
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IdentityResult BaseCreate(SysUser user, string password)
        {
            Task<IdentityResult> taskResult = base.CreateAsync(user, password);
            taskResult.Wait();
            return taskResult.Result;
        }

        internal IdentityResult HandleAndAction(IPrivilegeAsyncImpl asyncImpl, object[] paras)
        {
            CoreActionEventHandler coreAction = asyncImpl.CoreAction;
            BuildDbUpdateConcurrencyExceptionFormatStringEventHandler stringHandler1
                = asyncImpl.DbUpdateConcurrencyExceptionFormatStringBuilder;
            BuildErrorFormatStringEventHandler stringHandler2 = asyncImpl.ErrorFormatStringBuilder;
            IdentityResult taskResult = null;

            int counter = 5;
            //通过配置，设定一个尝试的初始值，暂时写死5
            while (taskResult == null && counter > 0)
            {
                try
                {
                    IdentityResult identResult = coreAction.Invoke(paras);

                    //Task<IdentityResult> tresult = coreAction.Invoke(paras);
                    //tresult.Wait();
                    var result = identResult;// tresult.Result;

                    if (result != null && result.Succeeded)
                    {
                        taskResult = IdentityResult.Success;
                        //Task<IdentityResult>.Run(
                        //() =>
                        //{//保证返回非空的Task结果对象
                        //    return IdentityResult.Success;
                        //}
                        //);
                        ////如果到这里都没有报错，说明已经添加成功无异常，可以直接返回
                        break;
                    }
                }
                catch (DbUpdateConcurrencyException db)
                {
                    HandleDbUpdateConcurrencyExceptionReload(paras, stringHandler1, db);
                }
                catch (AggregateException taskException)
                {
                    LogHelper.Error("ApplicationUserManager.HandleAndAction Exception: ", taskException);
                    if (taskException.InnerException != null)
                    {
                        LogHelper.Error("ApplicationUserManager.HandleAndAction InnerException: ", taskException.InnerException);

                        if (taskException.InnerException is DbUpdateConcurrencyException)
                        {
                            HandleDbUpdateConcurrencyExceptionReload(paras, stringHandler1,
                                taskException.InnerException as DbUpdateConcurrencyException);
                        }
                    }
                }
                //在这里出来就是要么没有异常，要么异常已经被处理，无论如何先减Counter避免死循环
                counter--;//如果Counter未为0，则继续尝试直到不爆这个异常
            }

            if (taskResult == null)
            {
                return HandleNonCatchedExceptionReturn(paras, stringHandler2);
            }

            return taskResult;
        }

        private IdentityResult HandleNonCatchedExceptionReturn(object[] paras,
            BuildErrorFormatStringEventHandler stringHandler2)
        {
            //极有可能是DbUpdateConcurrencyException最终无法解决，需要记录一个关键错误，
            //并且返回不要为空，否则外层使用await则会异常
            string temp2 = stringHandler2.Invoke(paras);

            LogHelper.Fatal(temp2, new Exception(temp2));
            LogHelper.Error(temp2);
            LogHelper.Debug(temp2);

            //return 
            //Task<IdentityResult>.Run(
            //    () =>
            //    {//保证返回非空的结果对象
            return IdentityResult.Failed(temp2);
            //}
            //);
        }

        private void HandleDbUpdateConcurrencyExceptionReload(object[] paras,
            BuildDbUpdateConcurrencyExceptionFormatStringEventHandler stringHandler1, DbUpdateConcurrencyException db)
        {
            //先记录Debug日志
            string temp = stringHandler1.Invoke(paras, db);

            LogHelper.Debug(temp);
            System.Diagnostics.Debug.WriteLine(temp);

            //发现并发异常，先通过异常对象抓到哪个对象的当前值与数据库不一致
            if (db != null && db.Entries != null && db.Entries.Count() > 0)
            {//对所有不一致的对象，都把他们的“原始值”置为数据库的值
                var entries = db.Entries.ToArray();
                foreach (var entry in entries)
                {
                    var dbvalues = entry.GetDatabaseValues();
                    var originValues = entry.OriginalValues;
                    var currentValues = entry.CurrentValues;
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    entry.Reload();//最后重新加载对象，变成unchanged状态
                }
            }
        }
    }

    //internal delegate Task<IdentityResult> CoreActionEventHandler(object[] paras);

    //internal delegate string BuildErrorFormatStringEventHandler(object[] paras);

    //internal delegate string BuildDbUpdateConcurrencyExceptionFormatStringEventHandler(
    //    object[] paras, DbUpdateConcurrencyException dbException);
}
