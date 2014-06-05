namespace Phoro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAsunto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MensajePrivadoes", "asunto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MensajePrivadoes", "asunto");
        }
    }
}
