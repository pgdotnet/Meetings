using Simple.Data;

namespace SimpleWithMssql.Examples
{
    public class DeleteExamples
    {
        public static void DeleteByNamedParameter(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var insertedActor = db.Actors.Insert(FirstName: "Boguslaw", LastName: "Linda");

            var numberOfUpdatedRows = db.Actors.Delete(ActorId: insertedActor.ActorId);

            Display.Result(numberOfUpdatedRows);
        }

        public static void DeleteBy(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var insertedActor = db.Actors.Insert(FirstName: "Boguslaw", LastName: "Linda");

            var numberOfUpdatedRows = db.Actors.DeleteByActorId(insertedActor.ActorId);

            Display.Result(numberOfUpdatedRows);
        }

        public static void DeleteAllWithCriteria(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            db.Actors.Insert(FirstName: "Boguslaw", LastName: "Linda");

            var numberOfUpdatedRows = db.Actors.DeleteAll(db.Actors.LastName.Like("Linda"));

            Display.Result(numberOfUpdatedRows);
        }
    }
}