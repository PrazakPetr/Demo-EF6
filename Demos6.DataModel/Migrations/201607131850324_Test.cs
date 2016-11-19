namespace Demos6.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas");
            DropIndex("dbo.NinjaEquipments", new[] { "Ninja_Id" });
            RenameColumn(table: "dbo.NinjaEquipments", name: "Ninja_Id", newName: "NinjaId");
            AlterColumn("dbo.NinjaEquipments", "NinjaId", c => c.Int(nullable: false));
            CreateIndex("dbo.NinjaEquipments", "NinjaId");
            AddForeignKey("dbo.NinjaEquipments", "NinjaId", "dbo.Ninjas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NinjaEquipments", "NinjaId", "dbo.Ninjas");
            DropIndex("dbo.NinjaEquipments", new[] { "NinjaId" });
            AlterColumn("dbo.NinjaEquipments", "NinjaId", c => c.Int());
            RenameColumn(table: "dbo.NinjaEquipments", name: "NinjaId", newName: "Ninja_Id");
            CreateIndex("dbo.NinjaEquipments", "Ninja_Id");
            AddForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas", "Id");
        }
    }
}
