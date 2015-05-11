using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class NaturalJoinExamples
    {
        public static void SimpleJoin(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var results = db.Movies.All()
                .Select(db.Movies.Title, db.Movies.Genre.Type);

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
                .Select(db.Movies.Title, db.Movies.Cast.Actor.FirstName, db.Movies.Cast.Actor.LastName);

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
                .Select(db.Movies.Title, db.Movies.Cast.Actor.FirstName, db.Movies.Cast.Actor.LastName)
                .Where(db.Actor.FirstName.Like("%Mat%"));

            foreach (var result in results)
            {
                Display.Result(result.Title, result.FirstName, result.LastName);
            }
        }
    }
}