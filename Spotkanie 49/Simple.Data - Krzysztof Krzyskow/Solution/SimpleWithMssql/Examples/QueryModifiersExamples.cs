using System;
using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class QueryModifiersExamples
    {
        public static void SelectSomething(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All().Select(db.Movies.Title, db.Movies.Profit);

            foreach (var movie in movies)
            {
                Display.FormatedResult("Title: {0}, Profit: {1} MLN", movie.Title, movie.Profit);
            }
        }

        public static void SelectSomethingWithAlias(string connectionString)
        {
            //Warning string concatenation is tricky:
            //that will work: (db.Actors.FirstName + " " + db.Actors.LastName).As("ExtendedName")
            //but that:  ("AnyStringAtTheBegining" + db.Actors.FirstName + " " + db.Actors.LastName).As("ExtendedName") will handle expression in parenthesis and will throw exception
            //
            //var results = db.Actors.All()
            //    .Select(("AnyStringAtTheBegining" + db.Actors.FirstName + " " + db.Actors.LastName).As("ExtendedName"), db.Actors.Star());

            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All()
                .Select(db.Movies.Title, db.Movies.Profit, (db.Movies.Profit - db.Movies.Budget).As("Balance"));

            foreach (var movie in movies)
            {
                Display.FormatedResult("Title: {0}, Profit: {1} MLN, Balance: {2} MLN", movie.Title, movie.Profit,
                    movie.Balance);
            }
        }

        public static void SelectSomethingWithDistinct(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All().Select(db.Movies.GenreId.Distinct(), db.Movies.Title, db.Movies.Profit);

            foreach (var movie in movies)
            {
                Display.FormatedResult("Title: {0}, Profit: {1} MLN, Genre: {2}", movie.Title, movie.Profit,
                    movie.GenreId);
            }
        }

        public static void SelectSomethingWithWhere(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All()
                .Select(db.Movies.Title, db.Movies.Profit)
                .Where(db.Movies.Profit > 500);

            foreach (var movie in movies)
            {
                Display.FormatedResult("Title: {0}, Profit: {1} MLN", movie.Title, movie.Profit);
            }
        }

        public static void SelectSomethingWithCombinedWhere(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All()
                .Select(db.Movies.Title, db.Movies.Profit)
                .Where(db.Movies.Profit > 500)
                .Where(db.Movies.Year > new DateTime(2014, 1, 1));

            foreach (var movie in movies)
            {
                Display.FormatedResult("Title: {0}, Profit: {1} MLN", movie.Title, movie.Profit);
            }
        }

        public static void WhereWithIn(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All()
                .Select(db.Movies.Title, db.Movies.Profit)
                .Where(db.Movies.GenreId != new[] {1, 4, 5});

            foreach (var movie in movies)
            {
                Display.FormatedResult("Title: {0}, Profit: {1} MLN", movie.Title, movie.Profit);
            }
        }

        public static void WhereWithBetween(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All()
                .Select(db.Movies.Title, db.Movies.Profit)
                .Where(db.Movies.Profit == 300.to(500));

            foreach (var movie in movies)
            {
                Display.FormatedResult("Title: {0}, Profit: {1} MLN", movie.Title, movie.Profit);
            }
        }

        public static void WhereWithLike(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All()
                .Select(db.Movies.Title, db.Movies.Profit)
                .Where(db.Movies.Title.Like("%Django%"));

            foreach (var movie in movies)
            {
                Display.FormatedResult("Title: {0}, Profit: {1} MLN", movie.Title, movie.Profit);
            }
        }
    }
}