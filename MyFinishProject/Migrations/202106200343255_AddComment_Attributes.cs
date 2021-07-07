namespace MyFinishProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComment_Attributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Description", c => c.String());
        }
    }
}
