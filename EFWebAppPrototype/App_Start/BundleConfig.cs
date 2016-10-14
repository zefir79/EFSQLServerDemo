using System.Web;
using System.Web.Optimization;

namespace EFWebAppPrototype
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/app/bower_components/angular/angular.js",
                        "~/Scripts/app/bower_components/angular-resource/angular-resource.js",
                        "~/Scripts/app/bower_components/angular-route/angular-route.js",
                        "~/Scripts/app/bower_components/angular-bootstrap/ui-bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/app/bower_components/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/app/bower_components/jquery-validation/dist/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Scripts/app/bower_components/modernizr/src/Modernizr.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/app/bower_components/angular-bootstrap/ui-bootstrap.js"
                      ));

            //"~/Scripts/app/bower_components/respond/dest/respond.min.js"
            bundles.Add(new StyleBundle("~/style/css").Include(
                     "~/Scripts/app/bower_components/bootstrap-css-only/css/bootstrap.css",
                      "~/Scripts/app/app.css",
                      "~/Scripts/app/styles/application.css",
                      "~/Content/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/app.js",
                "~/Scripts/app/home/indexCtrl.js",
                "~/Scripts/app/services/apiService.js"
               
              //"~/Scripts/modules/common.core.js",
              //"~/Scripts/modules/common.ui.js",
              //"~/Scripts/app.js",
              //"~/Scripts/services/apiService.js",
              //"~/Scripts/services/notificationService.js"

              ));
        }
    }
}
