namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsStarredProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "IsStarred", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Note", "IsStarred");
        }
    }
}
