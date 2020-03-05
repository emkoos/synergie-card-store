namespace SynergieCardStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                        CategoryDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        ProductTitle = c.String(nullable: false, maxLength: 100),
                        ProductAuthor = c.String(nullable: false, maxLength: 100),
                        ProductDescription = c.String(),
                        ProductShortDescription = c.String(),
                        AddedDate = c.DateTime(nullable: false),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ISBN = c.String(),
                        PublishingHouse = c.String(),
                        CardDimensions = c.String(),
                        Bestseller = c.Boolean(nullable: false),
                        Preview = c.Boolean(nullable: false),
                        Old = c.Boolean(nullable: false),
                        Hidden = c.Boolean(nullable: false),
                        ImageFileName = c.String(),
                        ImagesName = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrderPositions",
                c => new
                    {
                        OrderPositionId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderPositionId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        PostalCode = c.String(nullable: false, maxLength: 6),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        Comment = c.String(),
                        AddedDate = c.DateTime(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        OrderValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderPositions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderPositions", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.OrderPositions", new[] { "ProductId" });
            DropIndex("dbo.OrderPositions", new[] { "OrderId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderPositions");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
