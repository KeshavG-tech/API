namespace ZomatoAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        DishId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                        VegOrNonVeg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DishId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        Rating = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dishes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Dishes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Dishes", new[] { "RestaurantId" });
            DropIndex("dbo.Dishes", new[] { "CategoryId" });
            DropTable("dbo.Restaurants");
            DropTable("dbo.Dishes");
            DropTable("dbo.Categories");
        }
    }
}
