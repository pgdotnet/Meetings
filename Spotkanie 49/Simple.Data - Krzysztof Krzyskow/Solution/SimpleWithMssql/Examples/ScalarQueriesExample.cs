using Simple.Data;

namespace SimpleWithMssql.Examples
{
	public class ScalarQueriesExample
	{
		public static void GetCountByColumn(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var count = db.Movies.GetCount(db.Movies.GenreId == 3);

			Display.Result(count);
		}

		public static void GetCountByColumnFluent(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var count = db.Movies.GetCountByGenreId(3);

			Display.Result(count);
		}

		public static void CheckExistsByColumn(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var exists = db.Movies.Exists(db.Movies.GenreId == 3);

			Display.Result(exists);
		}

		public static void CheckExistsByColumnFluent(string connectionString)
		{
			Display.Title();
			var db = Database.OpenConnection(connectionString);

			var exists = db.Movies.ExistsByGenreId(3);

			Display.Result(exists);
		}
	}
}