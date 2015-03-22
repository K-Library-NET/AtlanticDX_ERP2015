using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Devart.Common.Web.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;//Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    [Table("AspNetUsers")]
    public class ApplicationUser : Devart.Common.Web.Identity.IdentityUser<int>
    //IdentityUser<int, ApplicationUserLogin,
    //ApplicationUserRole, ApplicationUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            Microsoft.AspNet.Identity.UserManager<ApplicationUser, int> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this,
                Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }

        protected override int CreateKey()
        {
            if (!string.IsNullOrEmpty(this.UserName))
            {

            }
            return 0;
            // throw new System.NotImplementedException();
        }
    }

    [Table("AspNetRoles")]
    public class ApplicationRole : Devart.Common.Web.Identity.IdentityRole<int>
    //, ApplicationUserRole>
    {

        protected override int CreateKey()
        {
            if (!string.IsNullOrEmpty(this.Name))
            {

            }

            return 0;
            //throw new System.NotImplementedException();
        }
    }

    public class ApplicationDbContext : DbContext
    //Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext<
    //ApplicationUser>//, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")//, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<ApplicationRole> Roles { get; set; }

        public DbSet<ApplicationUserClaim> UserClaims { get; set; }

        public DbSet<ApplicationUserRole> UserRoles { get; set; }

        public DbSet<ApplicationUserLogin> UserLogins { get; set; }

        //internal static Devart.Data.MySql.Web.Identity.MySqlDataSource
        //    CreateMySqlDataSource(
        //    IdentityFactoryOptions<Devart.Data.MySql.Web.Identity.MySqlDataSource> options,
        //    IOwinContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        internal static ApplicationDataSource
            CreateMySqlDataSource2()
        {
            return new ApplicationDataSource();
        }
    }

    public class ApplicationDataSource : Devart.Data.MySql.Web.Identity.MySqlDataSource,
        IDisposable
    {
        public ApplicationDataSource()
            : base("name=DefaultConnection", "test")
        {

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }

    [Table("AspNetUserRoles")]
    public class ApplicationUserRole : Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>
    {
        [Key]
        [Column(Order = 2)]
        public override int RoleId
        {
            get
            {
                return base.RoleId;
            }
            set
            {
                base.RoleId = value;
            }
        }

        [Key]
        [Column(Order = 1)]
        public override int UserId
        {
            get
            {
                return base.UserId;
            }
            set
            {
                base.UserId = value;
            }
        }
    }


    [Table("AspNetUserClaims")]
    public class ApplicationUserClaim : Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>
    {

    }


    [Table("AspNetUserLogins")]
    public class ApplicationUserLogin : Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>
    {
        [Key]
        [Column(Order = 1)]
        public override string LoginProvider
        {
            get
            {
                return base.LoginProvider;
            }
            set
            {
                base.LoginProvider = value;
            }
        }

        [Key]
        [Column(Order = 2)]
        public override string ProviderKey
        {
            get
            {
                return base.ProviderKey;
            }
            set
            {
                base.ProviderKey = value;
            }
        }

        [Key]
        [Column(Order = 3)]
        public override int UserId
        {
            get
            {
                return base.UserId;
            }
            set
            {
                base.UserId = value;
            }
        }
    }
}