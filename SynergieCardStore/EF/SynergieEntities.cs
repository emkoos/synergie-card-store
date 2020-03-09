using Microsoft.AspNet.Identity.EntityFramework;
using SynergieCardStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SynergieCardStore.EF
{
    public class SynergieEntities : IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'CardsEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SynergieCardStore.EF.CardsEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CardsEntities' 
        // connection string in the application configuration file.
        public SynergieEntities()
            : base("name=SynergieStoreConnection")
        {
        }

        static SynergieEntities()
        {
            Database.SetInitializer<SynergieEntities>(new DataInitializer());
        }

        public static SynergieEntities Create()
        {
            return new SynergieEntities();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrderPosition> OrderPosition { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

    }
}