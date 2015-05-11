using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class PagingFunctionExample
    {
        //Warning paging operates on table primary key. if table has no keys it will throw exception that paging cannot be applied

        public static void SkipFirstTwoAndTakeTwoNext(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var movies = db.Movies.All().Skip(2).Take(2);

            foreach (var movie in movies)
            {
                Display.Result(movie.MovieId, movie.Title);
            }
        }

        public static void SinglePageWithTotalCount(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            for (var i = 0; i < 4; i++)
            {
                Promise<int> count;
                var movie = db.Movies.All().WithTotalCount(out count).Skip(i).Take(1).FirstOrDefault();

                Display.FormatedResult("Page: {0}/{1}; Movie: {2}", i, count.Value, movie.Title);
            }
        }

        public static void MultiPageWithTotalCount(string connectionString)
        {
            //WARNING:
            // Promise<>.Value waits to return value on SpinWait.SpinUntil it means that we cannot check it before we evaluate query 
            // in other way we will hang on SpinUntil.
            // Other downside: count always return count of records in table, it isn't updated in context of pageSize passed to Take()

            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var pageSize = 2;
            for (var i = 0; i < 4; i++)
            {
                Promise<int> count;
                var movies = db.Movies.All()
                    .WithTotalCount(out count)
                    .Skip(i*pageSize)
                    .Take(pageSize);

                foreach (var movie in movies)
                {
                    Display.Result(movie.MovieId, movie.Title);
                }
                Display.FormatedResult("Page: {0}/{1}", i, count.Value);
            }
        }
    }
}