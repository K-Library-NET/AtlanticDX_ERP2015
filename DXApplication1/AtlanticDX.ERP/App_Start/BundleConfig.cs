using System.Web;
using System.Web.Optimization;

namespace AtlanticDX.ERP
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/ScriptBundle/libs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.easyui.min.js",
                        "~/Scripts/easyui-lang-zh_CN.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.messages_zh.js"));

            bundles.Add(new ScriptBundle("~/ScriptBundle/js").Include(
                        "~/Scripts/dxy.js",
                        "~/Scripts/common.js"));

            //bundles.Add(new ScriptBundle("~/ScriptBundle/reports").IncludeDirectory(
            //    "~/Scripts/reports", "*.js", true));

            bundles.Add(new StyleBundle("~/StyleBundle/libs").Include(
               "~/Content/easyui/themes/default/easyui.css",
               "~/Content/easyui/themes/icon.css"));

            bundles.Add(new StyleBundle("~/StyleBundle/css").Include(
                "~/Content/css/base.css",
                "~/Content/css/site.css",
                "~/Content/css/dxy.css")
                );

        }
    }
}