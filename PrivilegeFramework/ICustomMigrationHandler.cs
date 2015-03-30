using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeFramework
{
    public interface ICustomMigrationHandler
    {
        CustomMigrationResult CustomMigrationSeed(
            PrivilegeFramework.ExtendedIdentityDbContext context,
             bool isExternalSeeding, string customMigrationKey);

        string MigrationKey
        {
            get;
        }
    }

    public class CustomMigrationResult
    {
        public CustomMigrationResult(string migrationKey, bool succeed)
        {
            this.Succeed = succeed;
            this.MigrationKey = migrationKey;
        }

        public string MigrationKey
        {
            get;
            set;
        }

        public bool? Succeed
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public Exception HappenedException
        {
            get;
            set;
        }

        public object Result
        {
            get;
            set;
        }

        public bool? GoToNextWhenExceptionHappend
        {
            get;
            set;
        }
    }
}
