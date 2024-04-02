using System.Web;
using System.Web.Optimization;

namespace LibraWebApp
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
					   "~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery-ui-{version}.js",
						"~/bundles/jqueryui"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate.js",
						"~/Scripts/jquery.validate.unobtrusive.js",
						"~/Scripts/jquery.unobtrusive-ajax.js"
						));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new Bundle("~/bundles/bootstrap").Include(
				"~/Scripts/bootstrap.bundle.min.js",
				"~/Scripts/bootstrap.min.js"
				//"~/Scripts/bootstrap-select.js"
				));

			bundles.Add(new ScriptBundle("~/bundles/popper").Include(
				"~/Scripts/umd/popper.js"));

			bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
				//"~/Scripts/dataTables.dataTables.min.js",
						"~/Scripts/DataTables/jquery.dataTables.js",
						"~/Scripts/DataTables/dataTables.select.js",
						"~/Scripts/DataTables/dataTables.bootstrap4.js"
						));

			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/bootstrap.css",
				"~/Content/DataTables/css/dataTables.bootstrap.min.css",
				"~/Content/DataTables/css/dataTables.bootstrap4.min.css",
				"~/Content/DataTables/css/select.dataTables.css",
				"~/Content/DataTables/css/jquery.dataTables.min.css"
				//"~/Content/bootstrap-select.css",
				//"~/Content/bootstrap-toggle.css",
				//"~/Content/themes/base/*.css"
				));


			bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
							"~/Content/bootstrap.css"
							//"~/Content/bootstrap-select.css"
							));
			//bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
			//			"~/Scripts/jquery-{version}.js"));

			//bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
			//			"~/Scripts/jquery.validate*"));

			//// Use the development version of Modernizr to develop with and learn from. Then, when you're
			//// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			//bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
			//			"~/Scripts/modernizr-*"));

			//bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
			//		  "~/Scripts/bootstrap.js"));
			//bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
			//			"~/Scripts/bootstrap.js",
			//			"~/Scripts/bootstrap.min.js"
			//			));

			//bundles.Add(new ScriptBundle("~/bundles/dataTables.bootstrap.js").Include(
			//	"~/Lib/datatables/js/dataTables.bootstrap.js"));

			//bundles.Add(new ScriptBundle("~/bundles/dataTables.dataTables.js").Include(
			//	"~/Lib/datatables/js/dataTables.dataTables.js",
			//	"~/Scripts/dataTables.js",
			//	"~/Scripts/dataTables.min.js",
			//	"~/Scripts/dataTables.bootstrap4.js",
			//	"~/Scripts/dataTables.dataTables.min.js"
			//	));
			//bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
			//			"~/Scripts/DataTables/jquery.dataTables.js",
			//			"~/Scripts/DataTables/dataTables.select.js",
			//			"~/Scripts/DataTables/dataTables.bootstrap4.js"
			//			));

			//bundles.Add(new StyleBundle("~/Content/css").Include(
			//	"~/Content/bootstrap.css",
			//	"~/Content/DataTables/css/dataTables.bootstrap4.css",
			//	"~/Content/DataTables/css/select.dataTables.css",
			//	"~/Content/bootstrap-select.css",
			//	"~/Content/bootstrap-toggle.css",
			//	"~/Content/themes/base/*.css"
			//	));

			//bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
			//	"~/Scripts/dataTables.js",
			//	"~/Scripts/dataTables.min.js",
			//	"~/Scripts/dataTables.bootstrap4.js",
			//	"~/Scripts/dataTables.dataTables.min.js"
			//	));

			//bundles.Add(new StyleBundle("~/bundles/dataTables.bootstrap.css").Include(
			//	"~/Lib/datatables/css/dataTables.bootstrap.css"));

			//bundles.Add(new StyleBundle("~/bundles/dataTables.dataTables.css").Include(
			//	"~/Lib/datatables/css/dataTables.dataTables.css"));

			////bundles.Add(new StyleBundle("~/Content/css").Include(
			////		  "~/Content/bootstrap.css",
			////		  "~/Content/Site.css"));
			//bundles.Add(new Bundle("~/bundles/bootstrap").Include(
			//		  "~/Scripts/bootstrap.js",
			//		  "~/Scripts/respond.js"));

			// Set EnableOptimizations to false for debugging. Allow to use bundles in debug mode

			BundleTable.EnableOptimizations = true;
		}
	}
}
