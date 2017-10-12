namespace MySeries.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EpisodeCommentsMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EpisodeComments", "Text", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EpisodeComments", "Text", c => c.String());
        }
    }
}
