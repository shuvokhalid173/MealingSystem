namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataToGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (MovieType) VALUES ('Action')");
            Sql("INSERT INTO Genres (MovieType) VALUES ('Comedy')");
            Sql("INSERT INTO Genres (MovieType) VALUES ('Family')");
            Sql("INSERT INTO Genres (MovieType) VALUES ('Horror')");
            Sql("INSERT INTO Genres (MovieType) VALUES ('Romantic')");
        }

        public override void Down()
        {
        }
    }
}
