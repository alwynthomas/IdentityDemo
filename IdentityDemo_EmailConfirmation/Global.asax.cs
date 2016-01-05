using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace IdentityDemo
{
    //nu-gets ->
    //Install-Package Microsoft.AspNet.Identity.Core
    //Install-Package Microsoft.AspNet.Identity.EntityFramework
    //Install-Package Microsoft.Owin.Host.SystemWeb
    //Install-Package Microsoft.Owin.Security.Cookies

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            RegisterGlobalFilters(GlobalFilters.Filters);
            ConfigBundles(BundleTable.Bundles);

            initDb();
        }

        private void RegisterGlobalFilters(GlobalFilterCollection  filters)
        {
            filters.Add(new AuthorizeAttribute());
        }

        private void ConfigBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/style.css"));
        }

        private void initDb()
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["dbCon"].ToString();

            Database.SetInitializer<Identity.AppIdentityDbContext>(new Initializer());
            var ctx = new Identity.AppIdentityDbContext(con);
            ctx.Database.Initialize(true);
        }

    }

    public class Initializer : DropCreateDatabaseAlways<Identity.AppIdentityDbContext>
    {
        protected override void Seed(Identity.AppIdentityDbContext ctx)
        {
            //TODO: define default account here
            base.Seed(ctx);
        }
    }

}
