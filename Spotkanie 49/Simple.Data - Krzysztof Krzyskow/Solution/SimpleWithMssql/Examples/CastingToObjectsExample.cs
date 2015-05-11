using System.Collections.Generic;
using Simple.Data;
using SimpleWithMssql.Dto;

namespace SimpleWithMssql.Examples
{
    public class CastingToObjectsExample
    {
        public static void SimpleRecordImplicitCast(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            Actor actor = db.Actor.Get(1);

            Display.Result(actor.ActorId, actor.FirstName, actor.LastName);
        }

        public static void SimpleRecordImplicitCastWhenNoRecords(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            Actor actor = db.Actor.Get(977212);

            if (actor == null) Display.Result("Actor doesn't exist.");
            else Display.Result(actor.ActorId, actor.FirstName, actor.LastName);
        }

        public static void SimpleRecordImplicitCastWhenCastingToWrongType(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            NotAnActor actor = db.Actor.Get(1);

            if (actor == null) Display.Result("Cannot cast to NotAnActor.");
            else Display.Result(actor.NotActorId, actor.NotFirstName, actor.NotLastName);
        }

        public static void SimpleRecordImplicitCastWhenCastingToAlmostWrongType(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            MaybeActor actor = db.Actor.Get(1);

            if (actor == null) Display.Result("Cannot cast to NotAnActor.");
            else Display.Result(actor.ActorId, actor.FirstName, actor.NotLastName);
        }

        public static void SimpleQueryImplicitCast(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            List<Actor> actors = db.Actor.All(); // IEnumerable<>,List<>,Array<> allowed in implicit cast

            foreach (var actor in actors)
            {
                Display.Result(actor.FirstName, actor.LastName);
            }
        }

        public static void SimpleQueryImplicitCastWhenNoRecords(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            List<Actor> actors = db.Actor.FindAllByActorId(777);

            Display.FormatedResult("Actors count: {0}", actors.Count);
            foreach (var actor in actors)
            {
                Display.Result(actor.FirstName, actor.LastName);
            }
        }

        public static void SimpleQueryImplicitCastWhenCastingToWrongType(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            List<NotAnActor> actors = db.Actor.All();

            Display.FormatedResult("Actors count: {0}", actors.Count);
            foreach (var actor in actors)
            {
                Display.Result(actor.NotActorId, actor.NotFirstName, actor.NotLastName);
            }
        }

        public static void SimpleQueryImplicitCastWhenCastingToAlmostWrongType(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            List<MaybeActor> actors = db.Actor.All();

            Display.FormatedResult("Actors count: {0}", actors.Count);
            foreach (var actor in actors)
            {
                Display.Result(actor.ActorId, actor.FirstName, actor.NotLastName);
            }
        }

        public static void ThrowOnSimpleQueryAndRecordImplicitCastMismatch(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            //This will result with empty collection
            List<Actor> actors = db.Actor.Get(1); // assigning record to a list
            foreach (var record in actors)
            {
                Display.Result(record.FirstName, record.LastName);
            }

            //This will throw: RuntimeBinderException
            Actor actor = db.Actor.All(); // assigning list to single record
            Display.Result(actor.FirstName, actor.LastName);
        }

        private class NotAnActor
        {
            public int NotActorId { get; set; }
            public string NotFirstName { get; set; }
            public string NotLastName { get; set; }
        }

        private class MaybeActor
        {
            public int ActorId { get; set; }
            public string FirstName { get; set; }
            public string NotLastName { get; set; }
        }
    }
}