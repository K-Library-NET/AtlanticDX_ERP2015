using PrivilegeFramework;
using System;
using System.Collections.Generic;

namespace AtlanticDX.Model.Migrations
{
    public class CustomMigrationHelper
    {
        public static IEnumerable<CustomMigrationResult>
            InvokeCustomMigrations(ExtendedIdentityDbContext context, string customMigrationKey)
        {
            IEnumerable<ICustomMigrationHandler> handler =
                GetMigrationHandlers(customMigrationKey);

            List<CustomMigrationResult> results = new List<CustomMigrationResult>();
            foreach (var h in handler)
            {
                var res = h.CustomMigrationSeed(context, true, customMigrationKey);
                results.Add(res);
                if (res.Succeed == false && res.GoToNextWhenExceptionHappend == false)
                    break;
            }
            return results;
        }

        private static IEnumerable<ICustomMigrationHandler> GetMigrationHandlers(
            string customMigrationKey)
        {
            if (!string.IsNullOrEmpty(customMigrationKey)
                && customMigrationKey.Equals(Releases.ReleaseSeed.MIGRATION_KEY,
                StringComparison.InvariantCultureIgnoreCase))
            {
                return new ICustomMigrationHandler[] { new Releases.ReleaseSeed() };
            }
            return new ICustomMigrationHandler[]{
                new AtlanticDX.Model.Migrations.Debugs.DebugSeed()
            };
        }
    }
}
