namespace ZomatoAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dishes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Restaurants", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Rating", c => c.String());
            DropColumn("dbo.Dishes", "Price");
        }
    }
}
