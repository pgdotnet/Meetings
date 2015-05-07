using Microsoft.Practices.Unity;
using RxUiSample.UI.Controllers;
using RxUiSample.UI.Services;
using RxUiSample.UI.Utilities;
using RxUiSample.UI.ViewModels;
using RxUiSample.UI.Views;
using System.Configuration;
using System.Windows;

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

            var address = ConfigurationManager.AppSettings.Get("ServerAddress");
            var port = int.Parse(ConfigurationManager.AppSettings.Get("ServerPort"));
            container.RegisterInstance(new ServerConfig { Address = address, Port = port }, new ContainerControlledLifetimeManager());
            container.RegisterType<IUdpClientServer, UdpClientServer>(new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICommunicationChannel<>), typeof(UdpCommunicationChannel<>));

            container.RegisterType<IInteraction, Interaction>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommandProxy, CommandProxy>();
            container.RegisterType<IMainWindowController, MainWindowController>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMainWindowViewModel, MainWindowViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<MainWindow>(new ContainerControlledLifetimeManager());

            container.Resolve<MainWindow>().Show();
        }
    }
}
