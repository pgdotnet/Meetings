using Simple.Data;
using SimpleWithMssql.Dto;

namespace SimpleWithMssql.Examples
{
    public class InsertExamples
    {
        public static void InsertByNamedParameters(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var insertedActor = db.Actors.Insert(FirstName: "Boguslaw", LastName: "Linda");

            Display.Result(insertedActor.ActorId, insertedActor.FirstName, insertedActor.LastName);
        }

        public static void InsertByObject(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var actorLinda = new Actor {FirstName = "Boguslaw", LastName = "Linda"};
            var insertedActor = db.Actors.Insert(actorLinda);

            Display.Result(insertedActor.ActorId, insertedActor.FirstName, insertedActor.LastName);
        }

        public static void MultiInsert(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var actor1 = new Actor {FirstName = "Boguslaw", LastName = "Linda"};
            var actor2 = new Actor {FirstName = "Boguslaw", LastName = "Linda"};

            var actors = new[] {actor1, actor2};
            var insertedActors = db.Actors.Insert(actors);

            foreach (var insertedActor in insertedActors)
            {
                Display.Result(insertedActor.ActorId, insertedActor.FirstName, insertedActor.LastName);
            }
        }
    }
}