namespace Campus_Social_Network.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Campus_Social_Network.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<Campus_Social_Network.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Campus_Social_Network.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(new IdentityRole(Constants.UserType.Admin));
            context.Roles.AddOrUpdate(new IdentityRole(Constants.UserType.Student));


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
}
}
