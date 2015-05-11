using System.Collections.Generic;
using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class CastingToScalarExample
    {
        public static void SimpleRecordImplicitCastToScalar(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            //Warning scalar doesn't support casting. i.e: cast int to string will not work.

            //string name = db.Actor.Select(db.Actor.FirstName).Get(1); // this will throw exception
            string name = db.Actor.Select(db.Actor.FirstName).Get(1).ToScalar(); // this will throw exception
            int id = db.Actor.Select(db.Actor.ActorId).ToScalar();

            Display.Result(id, name);
        }

        public static void SimpleQueryImplicitCastToScalar(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            string name = db.Actor.All().Select(db.Actor.FirstName).FirstOrDefault().ToScalar();
            Display.Result(name);
        }

        public static void SimpleQueryImplicitCastToScalarList(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            //Warning: ToScalarList<> needs explicit type in other way it will return List<dynamic>
            List<string> names = db.Actor.All().Select(db.Actor.FirstName).ToScalarList<string>();

            foreach (var name in names)
            {
                Display.Result(name);
            }
        }
    }
}