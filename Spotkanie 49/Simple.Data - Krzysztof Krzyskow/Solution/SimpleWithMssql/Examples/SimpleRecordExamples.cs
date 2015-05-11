using System;
using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class SimpleRecordExamples
    {
        //always generates query that returns TOP 1
        //Get always use table primary key to fetch data
        //If there is compound primary key it will assign it in order that were defined 
        public static void GetByPrimaryKey(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movie = db.Movies.Get(3);

            Display.Result(movie.MovieId, movie.Title);
        }

        //Find shoot to DB without TOP 1 !!!! it selects all and reader takes first record.
        //Better call (Saul ;)) FindAll...FirstOrDefault().
        public static void FindByGenreId(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movie = db.Movies.Find(db.Movies.GenreId == 3);

            Display.Result(movie.MovieId, movie.GenreId, movie.Title);
        }

        public static void FindByGenreIdAndYear(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movie = db.Movies.Find(db.Movies.GenreId == 3 && db.Movies.Year > new DateTime(2014, 01, 01));

            Display.Result(movie.MovieId, movie.GenreId, movie.Title, movie.Year);
        }

        public static void FindByGenreIdOrYear(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movie = db.Movies.Find(db.Movies.GenreId == 5 || db.Movies.Year > new DateTime(2014, 01, 01));

            Display.Result(movie.MovieId, movie.GenreId, movie.Title, movie.Year);
        }
    }
}