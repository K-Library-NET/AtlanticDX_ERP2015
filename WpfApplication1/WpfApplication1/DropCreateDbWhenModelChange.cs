using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class DropCreateDbWhenModelChange : DropCreateDatabaseIfModelChanges<WXPstudioDbContext>
    {
        public static void Init()
        {
            Database.SetInitializer<WXPstudioDbContext>(new DropCreateDbWhenModelChange());
        }

        protected override void Seed(WXPstudioDbContext context)
        {
            var exists = context.Database.Exists();
            if (exists && context.Database.CompatibleWithModel(true))
            {
                return;
            }

            // If the database exists and doesn't match the model
            // then prompt for input
            if (exists)
            {
                //Console.WriteLine
                //  ("Existing database doesn't match the model!");
                //Console.Write
                //  ("Do you want to drop and create the database? (Y/N): ");
                //var res = Console.ReadKey();
                //Console.WriteLine();
                //if (!String.Equals(
                //  "Y",
                //  res.KeyChar.ToString(),
                //  StringComparison.OrdinalIgnoreCase))
                //{
                //    return;
                //}
                context.Database.Delete();
            }
            // Database either didn't exist or it didn't match
            // the model and the user chose to delete it
            context.Database.Create();

            var defConfig1 = context.CoreConfigs.Create();

            defConfig1.Key = "ViewConfirmScore";
            defConfig1.Value = "300";
            defConfig1.Comments = "看楼积分";

            var defConfig2 = context.CoreConfigs.Create();

            defConfig2.Key = "BuyConfirmScore";
            defConfig2.Value = "5000";
            defConfig2.Comments = "买楼积分";

            context.CoreConfigs.Add(defConfig1);
            context.CoreConfigs.Add(defConfig2);

            context.SaveChanges();

            base.Seed(context);
        }

        public void InitializeDatabase(WXPstudioDbContext context)
        {
            var exists = context.Database.Exists();
            if (exists && context.Database.CompatibleWithModel(true))
            {
                return;
            }

            // If the database exists and doesn't match the model
            // then prompt for input
            if (exists)
            {
                //Console.WriteLine
                //  ("Existing database doesn't match the model!");
                //Console.Write
                //  ("Do you want to drop and create the database? (Y/N): ");
                //var res = Console.ReadKey();
                //Console.WriteLine();
                //if (!String.Equals(
                //  "Y",
                //  res.KeyChar.ToString(),
                //  StringComparison.OrdinalIgnoreCase))
                //{
                //    return;
                //}
                context.Database.Delete();
            }
            // Database either didn't exist or it didn't match
            // the model and the user chose to delete it
            context.Database.Create();

            var defConfig1 = context.CoreConfigs.Create();

            defConfig1.Key = "ScoreRate";
            defConfig1.Value = "300";
            defConfig1.Comments = "积分比例";

            context.CoreConfigs.Add(defConfig1);

            context.SaveChanges();
        }
    }
}
