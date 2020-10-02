namespace Glamboyant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointment", "AppointmentTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Appointment", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointment", "Address");
            DropColumn("dbo.Appointment", "AppointmentTime");
        }
    }
}
