namespace Glamboyant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Review", "Image");
        }
    }
}
