
using System.Web.Optimization;

namespace GameStore.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/adminJs").Include(
               "~/Scripts/admin.js"));
            bundles.Add(new ScriptBundle("~/bundles/mainJs").Include(
              "~/Scripts/main.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/bundles/select2").Include(
             "~/Content/css/select2.min.css",
             "~/Content/select2-bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/select2Js").Include(
              "~/Scripts/select2.js"));

            bundles.Add(new ScriptBundle("~/bundles/delivery").Include(
                "~/Scripts/Delivery.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/main/css").Include(
                 "~/Content/font-awesome.css",
                      "~/Content/bootstrap.css",
                      "~/Content/main.css"));


        }
    }
}
