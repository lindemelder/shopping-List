namespace SimpleShoppingList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Checked = c.Boolean(nullable: false),
                        ShoppingListID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingListID, cascadeDelete: true)
                .Index(t => t.ShoppingListID);
            
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ShoppingListID", "dbo.ShoppingLists");
            DropIndex("dbo.Items", new[] { "ShoppingListID" });
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.Items");
        }
    }
}
