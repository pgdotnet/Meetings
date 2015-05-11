using System.Data.SqlClient;
using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class ConnectionExample
    {
        public static void UseSharedConnection(string connectionString)
        {
            Display.Title();

            var db = Database.OpenConnection(connectionString);
            var conn = new SqlConnection(connectionString);
            conn.Open();
            db.UseSharedConnection(conn);

            //Do stuff here
            var movie = db.Movies.FindByTitle("Interstellar");
            Display.Result(movie.Title);

            db.StopUsingSharedConnection();
            conn.Close();
        }
    }
}