namespace CenECommerce
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using CenECommerce.Classes;
    using CenECommerce.Models;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.
                SetInitializer(
                new MigrateDatabaseToLatestVersion<
                    CenECommerceContext,
                    Migrations.Configuration>());

            CheckRolesAndSuperUser();

            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.
                Configure(WebApiConfig.Register);

            FilterConfig.
                RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.
                RegisterRoutes(RouteTable.Routes);

            BundleConfig.
                RegisterBundles(BundleTable.Bundles);
        }

        private void CheckRolesAndSuperUser()
        {
            UsersHelpers.CheckRole("Admin");

            UsersHelpers.CheckRole("User");

            UsersHelpers.CheckSuperUser();
        }
    }
}
