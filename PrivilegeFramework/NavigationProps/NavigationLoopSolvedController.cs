using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UtilityFramework;

namespace PrivilegeFramework.NavigationProps
{
    /// <summary>
    /// 解决掉Navigation Property循环引用使得Json反序列化失败的问题。
    /// </summary>
    public class NavigationLoopSolvedController : Controller
    {
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return this.Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }

        protected override JsonResult Json(object data, string contentType,
            System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

                NavigationLoopSolvedJsonResult jr = new NavigationLoopSolvedJsonResult();
                jr.Data = data;
                jr.JsonRequestBehavior = behavior;
                jr.ContentType = contentType;
                jr.ContentEncoding = contentEncoding;

                return jr;
            }
            catch (Exception ex)
            {
                LogHelper.Debug(ex.Message + "\t\t" + ex.StackTrace);
            }

            return base.Json(data, contentType, contentEncoding, behavior);
        }
    }
}
