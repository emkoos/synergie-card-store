namespace SynergieCardStore.Migrations
{
    using SynergieCardStore.EF;
    using SynergieCardStore.Models;
    using System;
    using System.Collections.Generic;
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
            DataInitializer.SeedSynergieData(context);
            DataInitializer.SeedUsers(context);
        }
    }
}
