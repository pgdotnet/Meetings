using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TplUiSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TaskScheduler _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        public MainWindow()
        {
            InitializeComponent();
            Log = new ObservableCollection<string>();
            DataContext = this;
        }

        public ObservableCollection<string> Log { get; set; }

        private void BackgroundTask(object sender, RoutedEventArgs e)
        {
            var taskName = "Background task";

            var task = new Task(() => LogOperation(true, taskName));

            task.ContinueWith(_ => { Thread.Sleep(TimeSpan.FromSeconds(5)); }, TaskScheduler.Default)
                .ContinueWith(_ => LogOperation(false, taskName), _uiScheduler);

            task.Start(_uiScheduler);
        }

        private void MultipleTasks(object sender, RoutedEventArgs e)
        {
            var taskName = "Background task";

            var task = new Task(() => LogOperation(true, taskName));

            task.ContinueWith(_ => { Thread.Sleep(TimeSpan.FromSeconds(5)); }, TaskScheduler.Default)
                .ContinueWith(_ => LogOperation(false, taskName), _uiScheduler);

            task.Start(_uiScheduler);
        }

        private void ExceptionHandling(object sender, RoutedEventArgs e)
        {
            var taskName = "Exceptions";

            var task = new Task(() => LogOperation(true, taskName));

            var operation = task.ContinueWith(_ => { Thread.Sleep(TimeSpan.FromSeconds(3)); throw new NotImplementedException(); }, TaskScheduler.Default);
            
            operation.ContinueWith(t => LogOperation(false, String.Format("{0} failed: {1}", taskName, t.Exception.InnerException.Message)),
                    CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion, _uiScheduler);

            operation.ContinueWith(_ => LogOperation(false, String.Format("{0} finished!", taskName)),
                    CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, _uiScheduler);

            task.Start(_uiScheduler);
        }

        private void LogOperation(bool isRunning, string message)
        {
            Progress.IsIndeterminate = isRunning;
            Log.Add(String.Format("{0} {1}: {2}", DateTime.Now, isRunning ? "START" : "END", message));
        }
    }
}
