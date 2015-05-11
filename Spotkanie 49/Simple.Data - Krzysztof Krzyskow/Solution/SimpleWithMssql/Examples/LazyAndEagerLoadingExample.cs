using System.Linq;
using Simple.Data;
using SimpleWithMssql.Dto;

namespace SimpleWithMssql.Examples
{
    public class LazyAndEagerLoadingExample
    {
        public static void SimpleLazyLoading(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var lazyMovie = db.Movie.Get(1);
            Display.Result(lazyMovie.MovieId, lazyMovie.Title);

            foreach (var actor in lazyMovie.Cast.Actor)
            {
                Display.Result(actor.FirstName, actor.LastName);
            }
        }

        public static void SimpleEagerLoadingWithoutCasting(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            var eagerMovie = db.Movie.With(db.Movie.Cast.Actor.As("Actors")).Get(1);
            Display.Result(eagerMovie.MovieId, eagerMovie.Title);

            foreach (var actor in eagerMovie.Actors)
            {
                Display.Result(actor.FirstName, actor.LastName);
            }
        }

        public static void NaiveEagerLoading(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            MovieBase eagerMovie = db.Movie.Get(1);
            Display.Result(eagerMovie.MovieId, eagerMovie.Title); // eagerMovie.Genre will be null in this case!
        }

        public static void SimpleEagerLoading(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            MovieBase eagerMovie = db.Movie.WithGenre().Get(1);
            Display.Result(eagerMovie.MovieId, eagerMovie.Title, eagerMovie.Genre.Type);
        }

        public static void ComplexEagerLoading(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            dynamic actorAlias;
            Movie eagerMovie = db.Movie.WithGenre()
                .Join(db.Cast).On(db.Cast.MovieId == db.Movie.MovieId)
                .Join(db.Actor.As("Actors"), out actorAlias).On(actorAlias.ActorId == db.Cast.ActorId)
                .With(actorAlias).Get(1);

            Display.Result(eagerMovie.MovieId, eagerMovie.Title, eagerMovie.Genre.Type);
            foreach (var actor in eagerMovie.Actors)
            {
                Display.Result(actor.FirstName, actor.LastName);
            }
        }

        public static void ComplexEagerLoadingShorterWith(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            Movie eagerMovie = db.Movie.WithGenre().With(db.Movie.Cast.Actor.As("Actors")).Get(1);

            Display.Result(eagerMovie.MovieId, eagerMovie.Title, eagerMovie.Genre.Type);
            foreach (var actor in eagerMovie.Actors)
            {
                Display.Result(actor.FirstName, actor.LastName);
            }
        }

        public static void ComplexEagerLoadingShorterWithAndFilter(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            Movie eagerMovie = db.Movie.All().WithGenre().With(db.Movie.Cast.Actor.As("Actors"))
                .Where(db.Movie.MovieId == 1 && db.Movie.Cast.Actor.FirstName.Like("%Mat%"))
                .FirstOrDefault();

            Display.Result(eagerMovie.MovieId, eagerMovie.Title, eagerMovie.Genre.Type);
            foreach (var actor in eagerMovie.Actors)
            {
                Display.Result(actor.FirstName, actor.LastName);
            }
        }

        public static void ComplexEagerLoadingShorterWithAndFilterAndSingleActor(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            ManyGenresSingleActorMovie eagerMovie = db.Movie.All()
                .WithMany(db.Movie.Genre.As("Genres")) // we forcing to wrap genre in collection
                .WithOne(db.Movie.Cast.Actor) //we forcing here to take only one actor
                .Where(db.Movie.MovieId == 1 && db.Movie.Cast.Actor.FirstName.Like("%Mat%"))
                .FirstOrDefault();

            Display.Result(eagerMovie.MovieId, eagerMovie.Title, eagerMovie.Genres.First().Type,
                eagerMovie.Actor.FirstName, eagerMovie.Actor.LastName);
        }

        public static void ComplexEagerLoadingNonForeignKeyRelationship(string connectionString)
        {
            Display.Title();
            var db = Database.OpenConnection(connectionString);

            // Warning: in documentation WithOne/WithMany are assigned to fetch also data for relationship WITHOUT foreign key. 
            // It's not implicit Cake is a lie. they don't.
            // you need to explicitly tell how to match director records with movie - so you need to use JOIN as below.
            // WithOne/WithMany - defines relation type: WithOne = relation Many To One; WithMany = relation One To Many

            dynamic directorAlias;
            Movie eagerMovie = db.Movie
                .WithGenre()
                .With(db.Movie.Cast.Actor.As("Actors"))
                .Join(db.Director, out directorAlias).On(directorAlias.DirectorID == db.Movie.DirectorID)
                .WithOne(directorAlias) //we are telling here that one Movie has one Director (one-to-one relation)
                .Get(1);

            Display.Result(eagerMovie.MovieId, eagerMovie.Title, eagerMovie.Genre.Type, eagerMovie.Director.FirstName,
                eagerMovie.Director.LastName);
        }
    }
}