namespace MyFinishProject.Migrations
{
    using MyFinishProject.Models;
    using MyFinishProject.Models.HelpersModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyFinishProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MyFinishProject.Models.ApplicationDbContext";
        }

        protected override void Seed(MyFinishProject.Models.ApplicationDbContext context)
        {
            var admin = new ApplicationUser()
            {
                Favourites = new List<Favourites>()
            };
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
