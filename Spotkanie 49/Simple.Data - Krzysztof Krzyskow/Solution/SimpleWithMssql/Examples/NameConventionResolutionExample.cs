using System.Text.RegularExpressions;
using Simple.Data;
using Simple.Data.Extensions;

namespace SimpleWithMssql.Examples
{
    public class NameConventionResolutionExample
    {
        public static void CheckDifferentNamesForSameTable(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var result1 = db.Movies.Get(1);
            Display.FormatedResult("1: {0}", result1.Title);

            var result2 = db.Movie.Get(1);
            Display.FormatedResult("2: {0}", result2.Title);

            var result3 = db.movies.Get(1);
            Display.FormatedResult("3: {0}", result3.Title);

            var result4 = db._M_oV_iEs.Get(1);
            Display.FormatedResult("4: {0}", result4.Title);

            var result5 = db.dbo.Movie.Get(1);
            Display.FormatedResult("5: {0}", result5.Title);

            var result6 = db.notExistingSchema.Movie.Get(1);
            Display.FormatedResult("6: {0}", result6.Title);

            var result7 = db.dbo["Movie"].Get(1);
            Display.FormatedResult("7: {0}", result7.Title);

            //But that will throw Simple.Data.UnresolvableObjectException:
            //
            //var result7 = connection.MaybeItWasCalledMovie.Get(1);
            //Console.WriteLine("7: {0}", result7.Title);
        }

        public static void ThrowOnPotentialTableNamesConflict(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            //without this regex we will get InternalOperationException: Message = "Sequence contains more than one element"
            //Homogenize must be set before first call to the database! all homogenized table names are cached, so if it once homogenized it will stay like this in cache.
            //(tables are preloaded by SchemaProvider).

            HomogenizeEx.SetRegularExpression(new Regex("[^a-z0-9_]")); // default "[^a-z0-9]"

            var result1 = db.DemoTable.Get(1);
            Display.FormatedResult("DemoTable: {0}", result1.TableName);

            var result2 = db.Demo_Table.Get(1);
            Display.FormatedResult("Demo_Table: {0}", result2.TableName);
        }
    }
}