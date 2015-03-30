using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;

namespace YuShang.ERP.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var contracts = db.ordercontracts.Include(o => o.productitems).AsParallel();//.AsNoTracking().AsParallel();

            //var sql = db.ordercontracts.SqlQuery("",)

            var temp = contracts.ToList();

            //return Json(new { total = contracts.Count(), rows = temp }, JsonRequestBehavior.AllowGet);
            return Json(new { total = contracts.Count(), rows = temp }, JsonRequestBehavior.AllowGet);
            //return View();
            /*
             var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

             var result = new { total = contracts.Count(), rows = temp };

             //string data = JsonConvert.SerializeObject(result, Formatting.Indented, serializerSettings);
             //temp, Formatting.Indented, serializerSettings);
             CustomJsonResult jr = new CustomJsonResult();
             jr.Data = result;//data;
             jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
             jr.ContentType = "application/json";
             jr.ContentEncoding = System.Text.Encoding.UTF8;

             return jr;*/
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return this.Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);

            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

            string jsonText = JsonConvert.SerializeObject(data, Formatting.Indented, serializerSettings);
            CustomJsonResult jr = new CustomJsonResult();
            jr.Data = jsonText;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.ContentType = contentType;// "application/json";
            jr.ContentEncoding = contentEncoding;// System.Text.Encoding.UTF8;

            return jr;
            //return base.Json(data, contentType, contentEncoding);
        }

        protected override JsonResult Json(object data, string contentType,
            System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

            //string jsonText = JsonConvert.SerializeObject(data, Formatting.Indented, serializerSettings);
            CustomJsonResult jr = new CustomJsonResult();
            jr.Data = data;
            jr.JsonRequestBehavior = behavior;// JsonRequestBehavior.AllowGet;
            jr.ContentType = contentType;// "application/json"; //
            jr.ContentEncoding = contentEncoding;// System.Text.Encoding.UTF8;

            return jr;
            //return base.Json(data, contentType, contentEncoding, behavior);
        }
    }

    public class CustomJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {

            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Compare(context.HttpContext.Request.HttpMethod, "Get", true) == 0)
            {
                throw new InvalidOperationException();
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (null != this.Data)
            {
                response.Write(JsonConvert.SerializeObject(this.Data));
            }
        }
    }
}