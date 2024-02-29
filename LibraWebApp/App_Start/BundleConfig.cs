﻿using System.Web;
using System.Web.Optimization;

namespace LibraWebApp
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

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/dataTables.bootstrap.js").Include(
				"~/Lib/datatables/js/dataTables.bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/dataTables.dataTables.js").Include(
				"~/Lib/datatables/js/dataTables.dataTables.js"));

			bundles.Add(new StyleBundle("~/bundles/dataTables.bootstrap.css").Include(
				"~/Lib/datatables/css/dataTables.bootstrap.css"));

			bundles.Add(new StyleBundle("~/bundles/dataTables.dataTables.css").Include(
				"~/Lib/datatables/css/dataTables.dataTables.css"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));
		}
	}
}
