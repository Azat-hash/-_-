namespace MyFinishProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRequiredImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Images", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Images", c => c.Binary(nullable: false));
        }
    }
}
