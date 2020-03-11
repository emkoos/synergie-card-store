namespace SynergieCardStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductVideoUrlField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "VideoYTUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "VideoYTUrl");
        }
    }
}
