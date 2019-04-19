namespace CenECommerce.Migrations
{
    using CenECommerce.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : 
        DbMigrationsConfiguration<
            CenECommerceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CenECommerceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
