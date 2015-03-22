using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntities
{
    public class TestEntityDbContext : DbContext
    {
        public TestEntityDbContext()
            : base("name=defaultConnStr")
        {

        }

        public DbSet<CoreConfig> CoreConfigs
        {
            get;
            set;
        }
    }
}
