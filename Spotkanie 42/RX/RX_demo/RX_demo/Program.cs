using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;

namespace RX_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Greetings from thread {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("which demo?");
            int demo = int.Parse(Console.ReadLine());
            Console.Clear();

            bool demo1 = demo == 1;
            bool demo2 = demo == 2;
            bool demo3 = demo == 3;
            bool demo4 = demo == 4;
            bool demo5 = demo == 5;
            bool demo6 = demo == 6;

            if (demo1)
            {
                Console.WriteLine("demo 1: pull based iteration");
                PullBased();
                Console.WriteLine("demo 2: push based iteration");
                PushBased();
            }

            if (demo2)
            {
                Console.WriteLine("Subscription lifetime");
                SubscriptionLifetime();
            }

            if (demo3)
            {
                Console.WriteLine("IObservable details");
                ObservableDetails();
            }

            if (demo4)
            {
                Console.WriteLine("Observer creation");
                ObserverCreation();
            }

            if (demo5)
            {
                Console.WriteLine("Subjects");
                Subjects();
            }

            if (demo6)
            {
                Console.WriteLine("UDP receiver");
                UdpReceiver();
            }

            Console.WriteLine("All done!!!");
            Console.ReadLine();
        }


        #region Demo1

        static void PullBased()
        {
            ICollection<int> collection = new int[] {1, 2, 3, 4, 5, 6};

            foreach (var i in collection.Where(i => i % 2 == 0))
            {
                PrintNumber(i);
            }
        }

        static void PushBased()
        {
            IObservable<int> collection = new int[] {1, 2, 3, 4, 5, 6}.ToObservable().Where(i => i % 2 == 0);

            collection.Subscribe(PrintNumber);
        }


        static void PrintNumber(int number)
        {
            Console.WriteLine("Printing {0}", number);
        }

        #endregion

        #region Demo2
        static void SubscriptionLifetime()
        {
            int i = 0;
            IObservable<int> longRunning = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), ThreadPoolScheduler.Instance).Select(_ => ++i);

            IDisposable subscription = longRunning.Subscribe(item => Console.WriteLine("Received {0}", item));
            Console.WriteLine("Press any key to cancel...");
            Console.ReadKey();
            subscription.Dispose();
            Console.WriteLine("I'm still waiting....");
            Console.ReadKey();

        }
        #endregion

        #region Demo3
        static void ObservableDetails()
        {
            IObservable<int> sunnyWeatherScenario = new int[] {1, 2, 3, 4}.ToObservable().Select(i => i * i);
            sunnyWeatherScenario.Subscribe(item => Console.WriteLine("Result is {0}", item), () => Console.WriteLine("All done!"));

            IObservable<int> badWeatherScenario = new int[] {1, 2, 3, 4}.ToObservable().Select(i => i / (i - 3));
            badWeatherScenario.Subscribe(item => Console.WriteLine("Result is {0}", item), err => Console.WriteLine("Opps... {0}", err.Message));
        }
        #endregion

        #region Demo4
        static void ObserverCreation()
        {
            IObservable<int> singleValueInstant = Observable.Return(103);
            singleValueInstant.Subscribe(PrintNumber);

            Console.WriteLine("press any key...");
            Console.ReadKey();
            Console.Clear();

            IObservable<int> delegatedEagerCalculation = Observable.Start(() =>
                {
                    try
                    {
                        Console.WriteLine("I have so much to do... {0}", Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(5000);
                        return 104;
                    }
                    finally
                    {
                        Console.WriteLine("All done! {0}", Thread.CurrentThread.ManagedThreadId);
                    }
                });

            Console.WriteLine("Let me sleep for several seconds...{0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(10000);
            Console.WriteLine("Ok, lets continue...");
            delegatedEagerCalculation.Subscribe(PrintNumber);

            Console.WriteLine("press any key...");
            Console.ReadKey();
            Console.Clear();


            IObservable<int> delegatedLazyCalculatoin = Observable.Create<int>(o =>
                {
                    try
                    {
                        Console.WriteLine("Oh, not again... {0}", Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(5000);
                        o.OnNext(105);
                        o.OnCompleted();
                        return Disposable.Empty;
                    }
                    finally
                    {
                        Console.WriteLine("All done! {0}", Thread.CurrentThread.ManagedThreadId);
                    }
                });

            Console.WriteLine("Let me sleep for several seconds... {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Console.WriteLine("Ok, lets continue...");

            delegatedLazyCalculatoin.Subscribe(PrintNumber);

        }
        #endregion

        #region Demo5
        static void Subjects()
        {
            Subject<int> basicSubject = new Subject<int>();
            ReplaySubject<int> replySubject = new ReplaySubject<int>(2);
            BehaviorSubject<int> behaviorSubject = new BehaviorSubject<int>(-1);
            AsyncSubject<int> asyncSubject = new AsyncSubject<int>();

            Dictionary<string, ISubject<int>> toRun = new Dictionary<string, ISubject<int>>
                                                           {
                                                               {"Subject", basicSubject},
                                                               {"ReplaySubject", replySubject},
                                                               {"BehaviourSubject", behaviorSubject},
                                                               {"AsyncSubject", asyncSubject}
                                                           };
            foreach (var kvp in toRun)
            {

                Console.WriteLine("Testing {0}", kvp.Key);
                ProduceItems(kvp.Value, 10);
                Console.WriteLine("sleeping 5 seconds...");
                Thread.Sleep(5000);

                using (var mre = new ManualResetEvent(false))
                {
                    Console.WriteLine("let see what's there...");
                    kvp.Value.Subscribe(PrintNumber, () =>
                                                         {
                                                             Console.WriteLine("All done!");
                                                             mre.Set();
                                                         });
                    mre.WaitOne();
                    Console.WriteLine("all done...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void ProduceItems(IObserver<int> target, int expectedCount, int delay = 1050)
        {
            ThreadPool.QueueUserWorkItem(_ =>
                                             {
                                                 for (int i = 0; i < expectedCount; ++i)
                                                 {
                                                     Thread.Sleep(delay);
                                                     Console.WriteLine("Publishing item {0}", i);
                                                     target.OnNext(i);
                                                 }
                                                 target.OnCompleted();
                                             });
        }
        #endregion

        #region Demo6
        public class SimpleUdpClient : IDisposable
        {
            private readonly UdpClient _client = new UdpClient(new IPEndPoint(new IPAddress(new byte[] {127, 0, 0, 1}), 9999));
           
            public IAsyncResult BeginReceive(AsyncCallback cb, object state)
            {
                return _client.BeginReceive(cb, state);
            }

            public byte[] EndReceive(IAsyncResult ar)
            {
                try
                {
                    IPEndPoint endPoint = new IPEndPoint(0, 0);
                    byte[] result = _client.EndReceive(ar, ref endPoint);
                    return result;
                }
                catch (ObjectDisposedException e)
                {
                    return new byte[0];
                }
            }

            public void Dispose()
            {
                Console.WriteLine("disposing...");
                _client.Close();                
            }
        }

        public static void UdpReceiver()
        {
            IObservable<byte[]> source = Observable.Using(
                    () => new SimpleUdpClient(),
                    client => Observable
                              .Defer(Observable.FromAsyncPattern<byte[]>(client.BeginReceive, client.EndReceive))
                              .Repeat())
                .Publish()
                .RefCount();


            source
                .Skip(10)
                .Take(500)
                .Max(p => p[0])
                .Subscribe(max => Console.WriteLine("Maximum element: {0}", max));

            source
                .Take(1000)
                .Select(p => p[1])
                .Aggregate(new Dictionary<byte, int>(), (buffer, input) =>
                                                            {
                                                                if (buffer.ContainsKey(input))
                                                                {
                                                                    buffer[input]++;
                                                                }
                                                                else
                                                                {
                                                                    buffer[input] = 1;
                                                                }
                                                                return buffer;
                                                            })
                .Select(FormatStats)                                                            
                .Subscribe(p => Console.WriteLine("Collected stats: {0}", p));

            source
                .Select(p => p[1])
                .Scan(0, (buffered, input) =>
                             {
                                 if (input == buffered + 1)
                                 {
                                     return input;
                                 }
                                 return buffered;
                             })
                .DistinctUntilChanged()
                .Buffer(5)
                .Subscribe(p =>
                               { 
                                   foreach (var i in p) Console.WriteLine("New number greater by 1 from prev is {0}", i);
                               });

        }

        private static string FormatStats(IDictionary<byte, int> input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in input)
            {
                sb.AppendFormat("{0} => {1}\r\n", kvp.Key, kvp.Value);
            }
            return sb.ToString();
        }
        #endregion
    }
}
