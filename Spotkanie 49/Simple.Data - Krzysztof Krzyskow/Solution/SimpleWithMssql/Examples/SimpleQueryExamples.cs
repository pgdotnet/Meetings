using System;
using Simple.Data;

namespace SimpleWithMssql.Examples
{
	public class SimpleQueryExamples
	{
		public static void GetAllOfThem(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var movies = db.Movies.All();

			foreach (var movie in movies)
			{
				Display.Result(movie.MovieId, movie.Title);
			}
		}

		public static void FindAllOfThem(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var movies = db.Movies.FindAll(db.Movies.GenreId == 3);
			
			foreach (var movie in movies)
			{
				Display.Result(movie.GenreId, movie.MovieId, movie.Title);
			}
		}

		public static void FindAllOfThemWithAnd(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var movies = db.Movies.FindAll(db.Movies.GenreId == 3 && db.Movies.Year < new DateTime(2014, 01, 01));

			foreach (var movie in movies)
			{
				Display.Result(movie.GenreId, movie.MovieId, movie.Title, movie.Year);
			}
		}

		public static void FindAllOfThemWithOr(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var movies = db.Movies.FindAll(db.Movies.GenreId == 3 || db.Movies.GenreId == 4);

			foreach (var movie in movies)
			{
				Display.Result(movie.GenreId, movie.MovieId, movie.Title);
			}
		}

		//NOTE: FindAllBy supports only FilterExpression so: doesn't support OR, <,>,<=,>= comparators

		public static void FindAllOfThemBy(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var movies = db.Movies.FindAllByGenreId(3);

			foreach (var movie in movies)
			{
				Display.Result(movie.GenreId, movie.MovieId, movie.Title);
			}
		}

		public static void FindAllOfThemByWithAnd(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var movies = db.Movies.FindAllByGenreIdAndTitle(3, "Django Unchained");

			foreach (var movie in movies)
			{
				Display.Result(movie.GenreId, movie.MovieId, movie.Title, movie.Year);
			}
		}

		public static void FindAllOfThemByNamedParameterVersion(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var movies = db.Movies.FindAllBy(GenreId: 3);

			foreach (var movie in movies)
			{
				Display.Result(movie.GenreId, movie.MovieId, movie.Title);
			}
		}

		public static void FindAllOfThemByWithAndNamedParameterVersion(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var movies = db.Movies.FindAllBy(GenreId: 3, Title: "Django Unchained");

			foreach (var movie in movies)
			{
				Display.Result(movie.GenreId, movie.MovieId, movie.Title, movie.Year);
			}
		}
	}
}