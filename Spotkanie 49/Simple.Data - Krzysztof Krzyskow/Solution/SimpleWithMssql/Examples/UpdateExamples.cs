using Simple.Data;
using SimpleWithMssql.Dto;

namespace SimpleWithMssql.Examples
{
    public class UpdateExamples
    {
        public static void UpdateByObject(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var insertedActor = db.Actors.Insert(FirstName: "Boguslaw", LastName: "Linda");
            var actor = new Actor
            {
                ActorId = insertedActor.ActorId,
                FirstName = "Arnold",
                LastName = insertedActor.LastName
            };

            var numberOfUpdatedRows = db.Actors.Update(actor);

            Display.Result(numberOfUpdatedRows);
        }

        public static void UpdateByNamedParametersWhenNoPrimaryKey(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var insertedActor = db.Actors.Insert(FirstName: "Boguslaw", LastName: "Linda");

            var numberOfUpdatedRows = db.Actors.UpdateByActorId(ActorId: insertedActor.ActorId, FirstName: "Rados³aw");

            Display.Result(numberOfUpdatedRows);
        }

        public static void UpdateByWhenNoPrimaryKey(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var insertedActor = db.Actors.Insert(FirstName: "Boguslaw", LastName: "Linda");
            var actor = new Actor
            {
                ActorId = insertedActor.ActorId,
                FirstName = "Rados³aw",
                LastName = insertedActor.LastName
            };

            var numberOfUpdatedRows = db.Actors.UpdateByActorId(actor);

            Display.Result(numberOfUpdatedRows);
        }

        public static void UpdateAllWithCriteria(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var resultSet = db.Actors.UpdateAll(db.Actors.LastName.Like("Linda"), FirstName: "Boguœ");

            foreach (var updated in resultSet.Cast<Actor>())
            {
                Display.Result(updated.ActorId, updated.FirstName, updated.LastName);
            }
        }
    }
}