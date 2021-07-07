namespace MyFinishProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Products", "ProductType", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Images", c => c.Binary(nullable: false));
            AddColumn("dbo.AspNetUsers", "Favourites_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Favourites_Id");
            AddForeignKey("dbo.AspNetUsers", "Favourites_Id", "dbo.Favourites", "Id");
            DropColumn("dbo.Products", "ProductTypes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductTypes", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "Favourites_Id", "dbo.Favourites");
            DropForeignKey("dbo.Favourites", "ProductId", "dbo.Products");
            DropIndex("dbo.AspNetUsers", new[] { "Favourites_Id" });
            DropIndex("dbo.Favourites", new[] { "ProductId" });
            DropColumn("dbo.AspNetUsers", "Favourites_Id");
            DropColumn("dbo.Products", "Images");
            DropColumn("dbo.Products", "ProductType");
            DropTable("dbo.Favourites");
        }
    }
}
