using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace YuShang.ERP.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<TestObject> TestObjects { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override System.Data.Entity.Validation.DbEntityValidationResult
            ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = new DbEntityValidationResult(entityEntry,
                new List<DbValidationError>());

            if (entityEntry.Entity is TestObject && entityEntry.State == EntityState.Added)
            {
                TestObject obj = entityEntry.Entity as TestObject;
                //check for uniqueness of post title 
                if (TestObjects.Any(m => m.Name == obj.Name))
                {
                    result.ValidationErrors.Add(
                            new System.Data.Entity.Validation.DbValidationError(
                                "Name", "TestObject Name 必须唯一。"));
                }
            }

            if (result.ValidationErrors.Count > 0)
            {
                return result;
            }
            else
            {
                return base.ValidateEntity(entityEntry, items);
            }
        }
    }

    public class TestObject : IValidatableObject
    {
        public string Id
        {
            get;
            set;
        }

        [Required]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public string Name
        {
            get;
            set;
        }

        [MaxLength(256)]
        public string Description
        {
            get;
            set;
        }


        public System.Collections.Generic.IEnumerable<ValidationResult>
            Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (this.Id == this.Name)
            {
                results.Add(new ValidationResult("ID和Name不能一样。",
                    new string[] { "Id", "Name" }));
            }
            return results;
        }
    }
}