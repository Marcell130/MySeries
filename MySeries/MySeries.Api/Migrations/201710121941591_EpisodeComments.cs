namespace MySeries.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EpisodeComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GenreTvShows", "Genre_TmdbId", "dbo.Genres");
            DropForeignKey("dbo.GenreTvShows", "TvShow_TmdbId", "dbo.TvShows");
            CreateTable(
                "dbo.EpisodeComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EpisodeId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Timestamp = c.DateTime(nullable: false, storeType: "date"),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Episodes", t => t.EpisodeId, cascadeDelete: true)
                .ForeignKey("Identity.Users", t => t.UserId)
                .Index(t => t.EpisodeId)
                .Index(t => t.UserId);
            
            AddForeignKey("dbo.GenreTvShows", "Genre_TmdbId", "dbo.Genres", "TmdbId");
            AddForeignKey("dbo.GenreTvShows", "TvShow_TmdbId", "dbo.TvShows", "TmdbId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenreTvShows", "TvShow_TmdbId", "dbo.TvShows");
            DropForeignKey("dbo.GenreTvShows", "Genre_TmdbId", "dbo.Genres");
            DropForeignKey("dbo.EpisodeComments", "UserId", "Identity.Users");
            DropForeignKey("dbo.EpisodeComments", "EpisodeId", "dbo.Episodes");
            DropIndex("dbo.EpisodeComments", new[] { "UserId" });
            DropIndex("dbo.EpisodeComments", new[] { "EpisodeId" });
            DropTable("dbo.EpisodeComments");
            AddForeignKey("dbo.GenreTvShows", "TvShow_TmdbId", "dbo.TvShows", "TmdbId", cascadeDelete: true);
            AddForeignKey("dbo.GenreTvShows", "Genre_TmdbId", "dbo.Genres", "TmdbId", cascadeDelete: true);
        }
    }
}
