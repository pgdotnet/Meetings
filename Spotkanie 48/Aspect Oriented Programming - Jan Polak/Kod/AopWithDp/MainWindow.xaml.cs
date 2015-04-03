using AopWithDp.Interceptors;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AopWithDp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var model = new FormModel();
            model.Name = "Pan Pafnucy";
            model.Phone = "123-456-789";
            model.City = "Radom";

            var factory = new VmFactory();
            var proxy = factory.CreateViewModel(model, new FormValidator());

            DataContext = proxy;
        }
    }
}
