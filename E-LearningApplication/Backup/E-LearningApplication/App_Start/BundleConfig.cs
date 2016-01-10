using System.Web;
using System.Web.Optimization;

namespace E_LearningApplication {
    public class BundleConfig {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //scripts from the theme
            bundles.Add(new ScriptBundle("~/Content/themes/Theme/assets/js").Include(
                        "~/Content/themes/Theme/assets/js/common-scripts.js",
                        "~/Content/themes/Theme/assets/js/jquery.js",
                        "~/Content/themes/Theme/assets/js/jquery-1.8.3.min.js",
                        "~/Content/themes/Theme/assets/js/bootstrap.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            //css from the theme
            bundles.Add(new StyleBundle("~/Content/themes/Theme/assets/cssApp").Include(
                        "~/Content/themes/Theme/assets/css/bootstrap.css",
                        "~/Content/themes/Theme/assets/css/style.css",
                        "~/Content/themes/Theme/assets/css/style-responsive.css"
                ));
            bundles.Add(new StyleBundle("~/Content/themes/Theme/assets/font-awesome/cssApp").Include(
                        "~/Content/themes/Theme/assets/font-awesome/css/font-awesome.css"
                ));
            bundles.Add(new StyleBundle("~/Content/themes/Theme/assets/font-awesome/fontsApp").Include(
                        "~/Content/themes/Theme/assets/font-awesome/fonts/fontawesome-webfont.eot",
                        "~/Content/themes/Theme/assets/font-awesome/fonts/fontawesome-webfont.svg",
                        "~/Content/themes/Theme/assets/font-awesome/fonts/fontawesome-webfont.ttf",
                        "~/Content/themes/Theme/assets/font-awesome/fonts/fontawesome-webfont.woff",
                        "~/Content/themes/Theme/assets/font-awesome/fonts/FontAwesome.otf"
                ));
        }
    }
}
