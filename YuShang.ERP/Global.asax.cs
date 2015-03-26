using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace YuShang.ERP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configure((HttpConfiguration) =>
            //{
            //    HttpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //        new IgnoreListContractResolver();
            //});

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver
                = new IgnoreListContractResolver();

            //GlobalConfiguration.DefaultServer.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver
            //         = new IgnoreListContractResolver();
        }
    }
    public class IgnoreListContractResolver : DefaultContractResolver
    {
        //private readonly Dictionary<string, List<string>> IgnoreList;

        public IgnoreListContractResolver()//Dictionary<string, List<string>> i)
        {
            //IgnoreList = i;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            List<JsonProperty> properties = base.CreateProperties(type, memberSerialization).ToList();

            if (type.Name.Contains("OrderContract"))  //IgnoreList.ContainsKey(type.Name))
            {
                properties.RemoveAll(json => json.PropertyName == "OrderProducts");
            }

            return properties;
        }
    }
}
