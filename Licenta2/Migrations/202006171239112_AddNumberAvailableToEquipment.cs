namespace Licenta2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberAvailableToEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "NumberAvailable", c => c.Byte(nullable: false));

            Sql("UPDATE Equipments SET NumberAvailable = NumberInStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "NumberAvailable");
        }
    }
}
