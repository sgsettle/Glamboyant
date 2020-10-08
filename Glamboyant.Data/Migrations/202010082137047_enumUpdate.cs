namespace Glamboyant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enumUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HairService", "ServiceType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HairService", "ServiceType");
        }
    }
}
