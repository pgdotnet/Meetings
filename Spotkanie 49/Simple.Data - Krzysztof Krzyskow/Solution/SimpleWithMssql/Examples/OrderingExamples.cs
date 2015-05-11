using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class OrderingExamples
    {
        public static void OrderBy(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All().OrderBy(db.Movies.Title);
            foreach (var result in movies)
            {
                Display.Result(result.MovieId, result.Title);
            }
        }

        public static void OrderByFluent(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All().OrderByTitle();
            foreach (var result in movies)
            {
                Display.Result(result.MovieId, result.Title);
            }
        }

        public static void OrderByDescending(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All().OrderByDescending(db.Movies.Title);
            foreach (var result in movies)
            {
                Display.Result(result.MovieId, result.Title);
            }
        }

        public static void OrderByDescendingFluent(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All().OrderByTitleDescending();
            foreach (var result in movies)
            {
                Display.Result(result.MovieId, result.Title);
            }
        }

        public static void OrderByInJoinedTablesByPrimaryTable(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results1 = db.Movies.All()
                .Select(db.Movies.Star(), db.Genre.Type)
                .LeftJoin(db.Genre).On(db.Genre.GenreId == db.Movies.GenreId)
                .OrderBy(db.Movies.Title); // .OrderByTitle(); also works

            foreach (var result in results1)
            {
                Display.Result(result.MovieId, result.Title, result.Type);
            }
        }

        public static void OrderByInJoinedTablesByJoinedTable(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results1 = db.Movies.All()
                .Select(db.Movies.Star(), db.Genre.Type)
                .LeftJoin(db.Genre).On(db.Genre.GenreId == db.Movies.GenreId)
                .OrderBy(db.Genre.Type); // .OrderByType(); doesn't work for joined tables

            foreach (var result in results1)
            {
                Display.Result(result.MovieId, result.Title, result.Type);
            }

            var results2 = db.Movies.All()
                .Select(db.Movies.Star(), db.Genre.Type.As("GenreName"))
                .LeftJoin(db.Genre).On(db.Genre.GenreId == db.Movies.GenreId)
                .OrderByGenreName();

            foreach (var result in results2)
            {
                Display.Result(result.MovieId, result.Title, result.GenreName);
            }

            dynamic genreAlias;
            var results3 = db.Movies.All()
                .LeftJoin(db.Genre.As("alias"), out genreAlias).On(genreAlias.GenreId == db.Movies.GenreId)
                .Select(db.Movies.Star(), genreAlias.Type)
                .OrderBy(genreAlias.Type);

            foreach (var result in results3)
            {
                Display.Result(result.MovieId, result.Title, result.Type);
            }
        }

        //instead of ThenBy we can use once again GroupBy()
        public static void ThenBy(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results1 = db.Movies.All().OrderByGenreId().ThenBy(db.Movies.Title);
            foreach (var result in results1)
            {
                Display.Result(result.GenreId, result.Title, result.Year);
            }

            var results2 = db.Movies.All().OrderByGenreId().ThenByTitle();
            foreach (var result in results2)
            {
                Display.Result(result.GenreId, result.Title, result.Year);
            }
        }

        public static void ThenByDescending(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results1 = db.Movies.All().OrderByGenreId().ThenByDescending(db.Movies.Title);
            foreach (var result in results1)
            {
                Display.Result(result.GenreId, result.Title, result.Year);
            }

            var results2 = db.Movies.All().OrderByGenreId().ThenByTitleDescending();
            foreach (var result in results2)
            {
                Display.Result(result.GenreId, result.Title, result.Year);
            }
        }
    }
}