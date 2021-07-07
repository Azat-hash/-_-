namespace MyFinishProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserProducts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserProducts", "Product_Id", "dbo.Products");
            DropIndex("dbo.ApplicationUserProducts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserProducts", new[] { "Product_Id" });
            AddColumn("dbo.Favourites", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Favourites", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Product_Id", c => c.Int());
            CreateIndex("dbo.Favourites", "UserId");
            CreateIndex("dbo.Favourites", "ProductId");
            CreateIndex("dbo.AspNetUsers", "Product_Id");
            AddForeignKey("dbo.Favourites", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.Favourites", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropTable("dbo.ApplicationUserProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserProducts",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Product_Id });
            
            DropForeignKey("dbo.Favourites", "ProductId", "dbo.Products");
            DropForeignKey("dbo.AspNetUsers", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Favourites", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Product_Id" });
            DropIndex("dbo.Favourites", new[] { "ProductId" });
            DropIndex("dbo.Favourites", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "Product_Id");
            DropColumn("dbo.Favourites", "ProductId");
            DropColumn("dbo.Favourites", "UserId");
            CreateIndex("dbo.ApplicationUserProducts", "Product_Id");
            CreateIndex("dbo.ApplicationUserProducts", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserProducts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserProducts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
