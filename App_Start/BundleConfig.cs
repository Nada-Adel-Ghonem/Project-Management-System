using System.Web;
using System.Web.Optimization;

namespace Final_Project_Management_System
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

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/site.css"));
            

            bundles.Add(new StyleBundle("~/HomeStyle").Include(
                "~/css/style.css"));

            bundles.Add(new StyleBundle("~/assets/css").Include(
                "~/assets/css/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/HomeJs").Include(
                "~/js/java.js"));

            bundles.Add(new ScriptBundle("~/assets/js").Include(
                "~/assets/js/jquery-3.5.1.min.js",
                "~/assets/js/bootstrap.min.js"));
        }
    }
}
