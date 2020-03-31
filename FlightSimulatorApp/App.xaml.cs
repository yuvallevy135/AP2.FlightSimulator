using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;

namespace FlightSimulatorApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
      public FlightSimulatorViewModel flightSimulatorViewModel;
      public ManualControlsViewModel manualControlsViewModel;
      public DashboardViewModel dashboardViewModel;
      public MapControlViewModel mapControlViewModel;
      public FlightSimulatorModel model;

      private void Application_Startup(Object sender, StartupEventArgs e)
      {
            model = new FlightSimulatorModel(new MyTelnetClient());

            flightSimulatorViewModel = new FlightSimulatorViewModel(model);
            manualControlsViewModel= new ManualControlsViewModel(model);
            dashboardViewModel = new DashboardViewModel(model);
            mapControlViewModel = new MapControlViewModel(model);

            Window mainWindow = new MainWindow();
            mainWindow.Show();
        }
	}
}
