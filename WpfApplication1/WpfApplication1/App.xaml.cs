using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication1.Models;

namespace WpfApplication1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            using (WXPstudioDbContext context = new WXPstudioDbContext())
            {
                var select_Salesmen = from one in context.Salesmans
                                      where one.SalesmanId == 123 && one.CompanyID == 456
                                      select one;

                var salesman = new Salesman();
                context.Salesmans.Add(salesman);//new
                context.SaveChanges();
                salesman.Address = "AAAA";// update
                context.SaveChanges();
                context.Salesmans.Remove(salesman);
                context.SaveChanges();//delete
            }
        }
    }
}
