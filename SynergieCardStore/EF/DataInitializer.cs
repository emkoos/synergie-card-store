using SynergieCardStore.Migrations;
using SynergieCardStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SynergieCardStore.EF
{
    public class DataInitializer : MigrateDatabaseToLatestVersion<SynergieEntities, Configuration>
    {

        public static void SeedSynergieData(SynergieEntities context)
        {
            // Some Initializing Data
        }
    }
}