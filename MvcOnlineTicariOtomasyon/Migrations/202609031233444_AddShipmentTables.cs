namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShipmentTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        ShipmentId = c.Int(nullable: false, identity: true),
                        TrackingCode = c.String(maxLength: 10, unicode: false),
                        RecipientName = c.String(maxLength: 50, unicode: false),
                        HandledBy = c.String(maxLength: 50, unicode: false),
                        CarrierName = c.String(maxLength: 50, unicode: false),
                        Note = c.String(maxLength: 200, unicode: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ShipmentId);
            
            CreateTable(
                "dbo.ShipmentTrackings",
                c => new
                    {
                        ShipmentTrackingId = c.Int(nullable: false, identity: true),
                        TrackingCode = c.String(maxLength: 10, unicode: false),
                        StatusDetail = c.String(maxLength: 100, unicode: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ShipmentTrackingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShipmentTrackings");
            DropTable("dbo.Shipments");
        }
    }
}
