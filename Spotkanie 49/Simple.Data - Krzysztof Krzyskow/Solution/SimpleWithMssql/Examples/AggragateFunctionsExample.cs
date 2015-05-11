using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class AggragateFunctionsExample
    {
        public static void AggregateWithCount(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Movies.All()
                .Select(db.Movies.Cast.Actor.FirstName,
                    db.Movies.Cast.Actor.LastName,
                    db.Movies.MovieId.Count().As("NumberOfMovies"));

            foreach (var result in results)
            {
                Display.Result(result.NumberOfMovies, result.FirstName, result.LastName);
            }
        }

        public static void AggregateWithCount2(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Actor.All()
                .Select(db.Actor.FirstName,
                    db.Actor.LastName,
                    db.Actor.Cast.MovieId.Count().As("NumberOfMovies"));

            foreach (var result in results)
            {
                Display.Result(result.NumberOfMovies, result.FirstName, result.LastName);
            }
        }

        public static void SimpleHaving(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Actor.All().Having(db.Actor.Cast.Movie.Profit.Min() > 300);

            foreach (var result in results)
            {
                Display.Result(result.FirstName, result.LastName);
            }
        }

        public static void AggregateWithCountAndFilter(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Actor.All()
                .Select(db.Actor.FirstName,
                    db.Actor.LastName,
                    db.Actor.Cast.MovieId.Count().As("NumberOfMovies"))
                .Having(db.Actor.Cast.MovieId.Count() > 2);

            foreach (var result in results)
            {
                Display.Result(result.NumberOfMovies, result.FirstName, result.LastName);
            }
        }

        public static void AggregateWithCountAndOrFilter(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Actor.All()
                .Select(db.Actor.FirstName,
                    db.Actor.LastName,
                    db.Actor.Cast.MovieId.Count().As("NumberOfMovies"))
                .Having(db.Cast.MovieId.Count() > 3 || db.Cast.MovieId.Count() < 3);

            foreach (var result in results)
            {
                Display.Result(result.NumberOfMovies, result.FirstName, result.LastName);
            }
        }

        public static void AggregateWithCountAndSumFilter(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Actor.All()
                .Select(db.Actor.FirstName,
                    db.Actor.LastName,
                    db.Actor.Cast.MovieId.Count().As("NumberOfMovies"),
                    db.Actor.Cast.Movie.Profit.Sum().As("ProfitOfMovies"))
                .Having(db.Cast.MovieId.Count() < 3)
                .Having(db.Actor.Cast.Movie.Profit.Sum() > 1000);

            foreach (var result in results)
            {
                Display.Result(result.NumberOfMovies, result.ProfitOfMovies, result.FirstName, result.LastName);
            }
        }

        public static void AggregateWithCountAndMinFilter(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Actor.All()
                .Select(db.Actor.FirstName,
                    db.Actor.LastName,
                    db.Actor.Cast.MovieId.Count().As("NumberOfMovies"),
                    db.Actor.Cast.Movie.Profit.Min().As("MinProfitOfMovie"))
                .Having(db.Cast.MovieId.Count() > 2)
                .Having(db.Actor.Cast.Movie.Profit.Min() > 200);

            foreach (var result in results)
            {
                Display.Result(result.NumberOfMovies, result.MinProfitOfMovie, result.FirstName, result.LastName);
            }
        }
    }
}