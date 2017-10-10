using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using PlcSecurityApp.ViewModels;

namespace PlcSecurityApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var window = new MainWindow();
            var viewModel = new MainWindowViewModel();

            window.DataContext = viewModel;
            window.Show();
        }
    }
}
