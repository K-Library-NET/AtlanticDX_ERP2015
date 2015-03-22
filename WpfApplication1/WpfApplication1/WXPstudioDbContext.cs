using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Models;

namespace WpfApplication1
{
    public class WXPstudioDbContext : DbContext
    {
        public WXPstudioDbContext()
            : base("name=defaultConnStr")
        {
            Database.SetInitializer<WXPstudioDbContext>(new DropCreateDbWhenModelChange());

        }

        public DbSet<CoreAccount> CoreAccounts
        {
            get;
            set;
        }

        public DbSet<CoreCompany> CoreCompanies
        {
            get;
            set;
        }

        public DbSet<CoreConfig> CoreConfigs
        {
            get;
            set;
        }

        public DbSet<SaleBook> SaleBooks
        {
            get;
            set;
        }

        public DbSet<Salesman> Salesmans
        {
            get;
            set;
        }

        public DbSet<SalesmanScore> SalesmanScores
        {
            get;
            set;
        }
    }
}
