using System.Web;
using System.Web.Optimization;

namespace LYSApp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //juqery-ui
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.min.js"));

            //jqueryuitouchpunch
            bundles.Add(new ScriptBundle("~/bundles/jqueryuitouchpunch").Include(
                       "~/Scripts/jquery-ui-touch-punch.js"));

            //jqueryplaceholder
            bundles.Add(new ScriptBundle("~/bundles/jqueryplaceholder").Include(
                       "~/Scripts/jquery.placeholder.js"));

            //jquerytouchSwipe
            bundles.Add(new ScriptBundle("~/bundles/jquerytouchSwipe").Include(
                       "~/Scripts/jquery.touchSwipe.min.js"));

            //infobox
            bundles.Add(new ScriptBundle("~/bundles/infobox").Include(
                       "~/Scripts/infobox.js"));

            //jqueryvisible
            bundles.Add(new ScriptBundle("~/bundles/jqueryvisible").Include(
                      "~/Scripts/jquery.visible.js"));

            // validate 
            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // LockYourStay script
            bundles.Add(new ScriptBundle("~/bundles/lockyourstay").Include(
                      "~/Scripts/lockyourstay.js"));

            // Layout script
            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                      "~/Scripts/Shared/layout.js"));

            // GA script
            bundles.Add(new ScriptBundle("~/bundles/lockyourstayGA").Include(
                      "~/Scripts/GoogleAnalytics/lockyourstay-google-analytics.min.js"));

            //css added in _Layout.cshtml
            bundles.Add(new StyleBundle("~/Content/layout").Include(
                     "~/Content/font-awesome.css",
                     "~/Content/simple-line-icons.css",
                     "~/Content/fullscreen-slider.css",
                     "~/Content/bootstrap.css",
                     "~/Content/app.css",
                     "~/Content/lockyourstay.css",
                     "~/Content/Custom/Shared/_Header.css"));

            //css added in Index.cshtml --> Home page
            bundles.Add(new StyleBundle("~/Content/index").Include(
                     "~/Content/jquery-ui.css",
                     "~/Content/Custom/Account/Index.css"));

            //css added in About.cshtml --> About page
            bundles.Add(new StyleBundle("~/Content/about").Include(
                     "~/Content/Custom/Home/About.css"));

            // css contact
            bundles.Add(new StyleBundle("~/Content/contact").Include(
                "~/Content/Custom/Home/Contact.css"));

            //jobs
            bundles.Add(new StyleBundle("~/Content/jobs").Include(
                "~/Content/Custom/Home/Jobs.css"));

            //TermsAndConditions
            bundles.Add(new StyleBundle("~/Content/termsAndConditions").Include(
              "~/Content/Custom/Home/TermsAndConditions.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
