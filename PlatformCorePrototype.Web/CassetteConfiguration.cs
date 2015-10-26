using System.IO;
using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace PlatformCorePrototype.Web
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            var files = new[]
            {
                "~/Vendor/modernizr/modernizr.js",
                "~/Vendor/jquery/dist/jquery.js",
                "~/Vendor/angular/angular.js",
                "~/Vendor/angular-route/angular-route.js",
                "~/Vendor/angular-cookies/angular-cookies.js",
                "~/Vendor/angular-animate/angular-animate.js",
                "~/Vendor/angular-ui-router/release/angular-ui-router.js",
                "~/Vendor/d3_3.5.6/d3.js",
                "~/Vendor/crossfilter/crossfilter.js",
                "~/Vendor/ngstorage/ngStorage.js",
                "~/Vendor/angular-ui-utils/ui-utils.js",
                "~/Vendor/angular-sanitize/angular-sanitize.js",
                "~/Vendor/angular-resource/angular-resource.js",
                "~/Vendor/angular-translate/angular-translate.js",
                "~/Vendor/angular-translate-loader-url/angular-translate-loader-url.js",
                "~/Vendor/angular-translate-loader-static-files/angular-translate-loader-static-files.js",
                "~/Vendor/angular-translate-storage-local/angular-translate-storage-local.js",
                "~/Vendor/angular-translate-storage-cookie/angular-translate-storage-cookie.js",
                "~/Vendor/oclazyload/dist/ocLazyLoad.js",
                "~/Vendor/angular-bootstrap/ui-bootstrap-tpls.js",
                "~/Vendor/angular-loading-bar/build/loading-bar.js",
                "~/Vendor/jquery.browser/dist/jquery.browser.js",
                "~/Vendor/ladda/dist/spin.js",
                "~/Vendor/ladda/dist/ladda.js",
                "~/Vendor/lodash/lodash.js",
                //"~/Vendor/amcharts_3.14.5/amcharts/amcharts.js",
                //"~/Vendor/amcharts_3.14.5/amcharts/serial.js",
                //"~/Vendor/amcharts_3.14.5/amcharts/xy.js",
                //"~/Vendor/amcharts_3.14.5/amcharts/themes/light.js",
                "~/Vendor/numeraljs_1.5.3/numeral.js",
                "~/Vendor/angular-input-masks/masks.js",
                "~/Vendor/animo/animo.js",
                "~/Vendor/angular-bootstrap-nav-tree/dist/abn_tree_directive.js",
                "~/Vendor/jsTree3/jstree.js"
            };
            bundles.Add<ScriptBundle>("~/Vendor", files);
            bundles.Add<ScriptBundle>("~/Scripts",
                new FileSearch {Pattern = "*.js", SearchOption = SearchOption.AllDirectories});

            files = new[]
            {
                "~/Content/app/bootstrap.css"
            };
            bundles.Add<StylesheetBundle>("~/Bootstrap", files);
            files = new[] {"~/Content/app/app.css", "~/Content/app/custom/site.css"};
            bundles.Add<StylesheetBundle>("~/App", files);
            files = new[]
            {
                "~/Vendor/angular-multi-select-master/isteven-multi-select.css",
                "~/Vendor/animate.css/animate.min.css",
                "~/Vendor/angular-bootstrap-nav-tree/dist/abn_tree.css",
                "~/Vendor/jsTree3/themes/default/style.css"
            };
            bundles.Add<StylesheetBundle>("~/Vendor", files);
        }
    }
}