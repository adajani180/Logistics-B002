using System.Web;
using System.Web.Optimization;

namespace Logistics
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleLunaTheme(bundles);
            BundleFullCalendar(bundles);
            BundleSatisPlugins(bundles);
        }

        private static void BundleLunaTheme(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/luna/js").Include(
                        "~/Themes/Luna/vendor/pacejs/pace.min.js",
                        "~/Themes/Luna/vendor/jquery/dist/jquery.min.js",
                        "~/Themes/Luna/vendor/bootstrap/js/bootstrap.min.js",
                        "~/Themes/Luna/vendor/switchery/switchery.min.js",
                        "~/Themes/Luna/vendor/datatables/datatables.min.js",
                        "~/Themes/Luna/vendor/toastr/toastr.min.js",
                        "~/Themes/Luna/vendor/sparkline/index.js",
                        "~/Themes/Luna/vendor/flot/jquery.flot.min.js",
                        "~/Themes/Luna/vendor/flot/jquery.flot.resize.min.js",
                        "~/Themes/Luna/vendor/flot/jquery.flot.spline.js",
                        "~/Themes/Luna/vendor/moment/moment.js",
                        "~/Themes/Luna/scripts/luna.js",
                        "~/Scripts/satis.js"));

            bundles.Add(new StyleBundle("~/bundles/luna/css").Include(
                      "~/Themes/Luna/vendor/fontawesome/css/font-awesome.css",
                      "~/Themes/Luna/vendor/animate.css/animate.css",
                      "~/Themes/Luna/vendor/bootstrap/css/bootstrap.css",
                      "~/Themes/Luna/vendor/switchery/switchery.min.css",
                      "~/Themes/Luna/vendor/toastr/toastr.min.css",
                      "~/Themes/Luna/vendor/datatables/datatables.min.css",
                      "~/Themes/Luna/styles/pe-icons/pe-icon-7-stroke.css",
                      "~/Themes/Luna/styles/pe-icons/helper.css",
                      "~/Themes/Luna/styles/stroke-icons/style.css",
                      "~/Themes/Luna/styles/style.css"));
        }

        private static void BundleFullCalendar(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/calendar/css").Include(
                      "~/Plugins/calendar/css/fullcalendar.css"));
            bundles.Add(new ScriptBundle("~/bundles/calendar/js").Include(
                      "~/Plugins/calendar/js/fullcalendar.js"));
        }

        private static void BundleSatisPlugins(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                        "~/Plugins/validator/validator.min.js",
                        "~/Plugins/bootbox/bootbox.min.js"));

            bundles.Add(new StyleBundle("~/bundles/plugins/datepicker/css").Include(
                      "~/Plugins/datepicker/css/bootstrap-datepicker3.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/plugins/datepicker/js").Include(
                      "~/Plugins/datepicker/js/bootstrap-datepicker.min.js"));
        }
    }
}
