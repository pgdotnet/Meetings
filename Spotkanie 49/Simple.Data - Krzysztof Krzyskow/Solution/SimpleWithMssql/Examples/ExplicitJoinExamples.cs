using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class ExplicitJoinExamples
    {
        public static void SimpleJoin(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Movies.All()
                .Join(db.Genre, GenreId: db.Movies.GenreId)
                .Select(db.Movies.Title, db.Genre.Type);

            foreach (var result in results)
            {
                Display.Result(result.Title, result.Type);
            }
        }

        public static void SimpleJoinWithOn(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Movies.All()
                .Join(db.Genre).On(db.Genre.GenreId == db.Movies.GenreId)
                .Select(db.Movies.Title, db.Genre.Type);

            foreach (var result in results)
            {
                Display.Result(result.Title, result.Type);
            }
        }

        public static void SimpleJoinWithAlias(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            dynamic genreAlias;
            var results = db.Movies.All()
                .Join(db.Genre.As("g"), out genreAlias).On(genreAlias.GenreId == db.Movies.GenreId)
                .Select(db.Movies.Title, genreAlias.Type);

            foreach (var result in results)
            {
                Display.Result(result.Title, result.Type);
            }
        }

        public static void MoreComplexJoin(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Movies.All()
                .Join(db.Cast).On(db.Cast.MovieId == db.Movies.MovieId)
                .Join(db.Actor).On(db.Cast.ActorId == db.Actor.ActorId)
                .Select(db.Movies.Title, db.Actor.FirstName, db.Actor.LastName);

            foreach (var result in results)
            {
                Display.Result(result.Title, result.FirstName, result.LastName);
            }
        }

        public static void MoreComplexJoinWithFilter(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Movies.All()
                .Join(db.Cast).On(db.Cast.MovieId == db.Movies.MovieId)
                .Join(db.Actor).On(db.Cast.ActorId == db.Actor.ActorId)
                .Select(db.Movies.Title, db.Actor.FirstName, db.Actor.LastName)
                .Where(db.Actor.FirstName.Like("%Mat%"));

            foreach (var result in results)
            {
                Display.Result(result.Title, result.FirstName, result.LastName);
            }
        }
    }
}