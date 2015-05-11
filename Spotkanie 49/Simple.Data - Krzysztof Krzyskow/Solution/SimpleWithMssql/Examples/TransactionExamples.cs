using Simple.Data;
using SimpleWithMssql.Dto;

namespace SimpleWithMssql.Examples
{
    public class TransactionExamples
    {
        public static void RunTransactionWithRolback(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            using (var transaction = db.BeginTransaction())
            {
                var arnold = new Actor {FirstName = "Arnold", LastName = "Schwarzeneger"};
                var insertedActor = transaction.Actors.Insert(arnold);
                Display.Result(insertedActor.ActorId, insertedActor.FirstName, insertedActor.LastName);

                transaction.Rollback();
            }


            var actor = db.Actors.FindAll(db.Actors.FirstName.Like("Arnold")).FirstOrDefault();
            if (actor == null) Display.Result("Actor doesn't exist.");
            else Display.Result(actor.ActorId, actor.FirstName, actor.LastName);
        }

        public static void RunTransactionWithCommit(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            using (var transaction = db.BeginTransaction())
            {
                var arnold = new Actor {FirstName = "Arnold", LastName = "Schwarzeneger"};
                var insertedActor = transaction.Actors.Insert(arnold);
                Display.Result(insertedActor.ActorId, insertedActor.FirstName, insertedActor.LastName);

                transaction.Commit();
            }

            var actor = db.Actors.FindAll(db.Actors.FirstName.Like("Arnold")).FirstOrDefault();
            if (actor == null) Display.Result("Actor doesn't exist.");
            else Display.Result(actor.ActorId, actor.FirstName, actor.LastName);

            db.Actors.DeleteAll(db.Actors.FirstName.Like("Arnold"));
        }
    }
}