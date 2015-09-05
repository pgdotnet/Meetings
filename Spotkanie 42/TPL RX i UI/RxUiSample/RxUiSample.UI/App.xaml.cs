using System.Configuration;
using System.Windows;
using Microsoft.Practices.Unity;
using RxUiSample.UI.Controllers;
using RxUiSample.UI.Services;
using RxUiSample.UI.Utilities;
using RxUiSample.UI.ViewModels;
using RxUiSample.UI.Views;

namespace RxUiSample.UI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var container = new UnityContainer();

			var serverAddress = ConfigurationManager.AppSettings.Get("ServerAddress");
			var serverPort = int.Parse(ConfigurationManager.AppSettings.Get("ServerPort"));
			var clientAddress = ConfigurationManager.AppSettings.Get("ClientAddress");
			var clientPort = int.Parse(ConfigurationManager.AppSettings.Get("ClientPort"));
			container.RegisterInstance(new ClientConfig { Address = clientAddress, Port = clientPort }, new ContainerControlledLifetimeManager());
			container.RegisterInstance(new ServerConfig { Address = serverAddress, Port = serverPort }, new ContainerControlledLifetimeManager());

			container.RegisterType<IUdpClientServer, UdpClientServer>(new ContainerControlledLifetimeManager());
			container.RegisterType<ICommunicationChannel<Message>>("server", new InjectionFactory(c => new UdpCommunicationChannel<Message>(c.Resolve<IUdpClientServer>(), c.Resolve<ServerConfig>())));
			container.RegisterType<ICommunicationChannel<Message>>("client", new InjectionFactory(c => new UdpCommunicationChannel<Message>(c.Resolve<IUdpClientServer>(), c.Resolve<ClientConfig>())));

			container.RegisterType<IInteraction, Interaction>(new ContainerControlledLifetimeManager());
			container.RegisterType<ICommandProxy, CommandProxy>();
			container.RegisterType<IMainWindowController, MainWindowController>(new ContainerControlledLifetimeManager());
			container.RegisterType<IMainWindowViewModel, MainWindowViewModel>(new ContainerControlledLifetimeManager());
			container.RegisterType<MainWindow>(new ContainerControlledLifetimeManager());

			container.Resolve<MainWindow>().Show();
		}
	}
}