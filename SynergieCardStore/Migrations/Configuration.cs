namespace SynergieCardStore.Migrations
{
    using SynergieCardStore.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SynergieCardStore.EF.SynergieEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SynergieCardStore.EF.SynergieEntities";
        }

        protected override void Seed(SynergieCardStore.EF.SynergieEntities context)
        {
            //  This method will be called after migrating to the latest version.
            DataInitializer.SeedSynergieData(context);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
