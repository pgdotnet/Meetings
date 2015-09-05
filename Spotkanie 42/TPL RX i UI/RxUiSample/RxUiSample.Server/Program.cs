using System;
using System.Configuration;
using System.Reactive.Linq;
using Faker;
using NLog;

namespace RxUiSample.Server
{
	class Program
	{
		private static readonly Logger _logger = LogManager.GetLogger("Server");

		static void Main(string[] args)
		{
			var serverAddress = ConfigurationManager.AppSettings.Get("ServerAddress");
			var serverPort = int.Parse(ConfigurationManager.AppSettings.Get("ServerPort"));
			var serverChannel = new UdpCommunicationChannel<Message>(new UdpClientServer(), new ServerConfig { Address = serverAddress, Port = serverPort });

			var clientAddress = ConfigurationManager.AppSettings.Get("ClientAddress");
			var clientPort = int.Parse(ConfigurationManager.AppSettings.Get("ClientPort"));
			var clientChannel = new UdpCommunicationChannel<Message>(new UdpClientServer(), new ClientConfig { Address = clientAddress, Port = clientPort });

			_logger.Info("Server is running. Press ENTER to exit.");

			clientChannel.MessageStream
				.Subscribe(data => _logger.Warn("Received message: {0}", data),
						   ex => _logger.Fatal("Exception occured: {0}", ex),
						   () => _logger.Warn("Subscription finished!"));

			Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
				.Subscribe(v =>
				{
					serverChannel.SendMessage(new Message(v, Lorem.Sentence(7000)))
						.Subscribe(_ => _logger.Info("Sent message {0}", v),
								   ex => _logger.Fatal("Exception occured: {0}", ex),
								   () => _logger.Debug("Sending finished."));
				},
				() => _logger.Warn("Timer finished!"));

			var sub = Observable.Zip(
				Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Select(l => String.Format("A-{0}", l)).Do(Console.WriteLine),
				Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(2)).Select(l => String.Format("B-{0}", l)).Do(Console.WriteLine),
				(a, b) => String.Format("{0} : {1}", a, b)
				).Subscribe(Console.WriteLine);

			Console.ReadLine();

			sub.Dispose();

			Console.ReadLine();
		}
	}
}