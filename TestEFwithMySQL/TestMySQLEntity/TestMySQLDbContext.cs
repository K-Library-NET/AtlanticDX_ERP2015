using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMySQLEntity
{
    public class TestMySQLDbContext : DbContext
    {
        public TestMySQLDbContext()
            : base("name=DefaultConnection")
        {

        }

        public DbSet<TestEntity1> TestEntity1s
        {
            get;
            set;
        }
    }
}
