using Microsoft.Practices.Prism.Events;
using PayDay.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PayDay
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.StartupEventArgs"/>the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Get the reference to the same name as the current one
            Process thisproc = Process.GetCurrentProcess();

            // Check how many processes have the same name as the current one
            if(Process.GetProcessesByName(thisproc.ProcessName).Length > 2)
            {
                MessageBox.Show("Application is already running!");
                Application.Current.Shutdown();
                return;
            }
            // Init event aggregator and services
            IEventAggregator eventAggregator = new EventAggregator();
            // Init view and viewmodel
            MainWindow window = new MainWindow();
            MainViewModel viewModel = new MainViewModel(eventAggregator);
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
