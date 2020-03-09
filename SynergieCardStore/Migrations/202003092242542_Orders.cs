namespace SynergieCardStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Orders", "UserId");
            RenameColumn(table: "dbo.Orders", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.Orders", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            AlterColumn("dbo.Orders", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "UserId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Orders", "UserId", c => c.String(nullable: false));
            CreateIndex("dbo.Orders", "ApplicationUser_Id");
            AddForeignKey("dbo.Orders", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
