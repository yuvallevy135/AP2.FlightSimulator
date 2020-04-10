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
using System.Net.Sockets;
using System.Windows.Interop;
using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Views;


namespace FlightSimulatorApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        //private FlightSimulatorViewModel flightSimulatorViewModel;
        //private ManualControlsViewModel manualControlsViewModel;
        //private DashboardViewModel dashboardViewModel;
        //private MapControlViewModel mapControlViewModel;
        //private FlightSimulatorModel model;
        private ConnectWindow cw;
        private ErrorsWindow ew;
        public MainWindow()
		{
            InitializeComponent();
            DataContext = (Application.Current as App).flightSimulatorViewModel;
            //model = new FlightSimulatorModel(new MyTelnetClient()); 
            ////flightSimulatorViewModel = new FlightSimulatorViewModel(new FlightSimulatorModel(new MyTelnetClient()));
            //flightSimulatorViewModel = new FlightSimulatorViewModel(model);
            //manualControlsViewModel = new ManualControlsViewModel(model);
            //dashboardViewModel = new DashboardViewModel(model);
            //mapControlViewModel = new MapControlViewModel(model);
            //DataContext = flightSimulatorViewModel;
            myDashboard.DataContext = (Application.Current as App).dashboardViewModel;
            myMapControl.DataContext = (Application.Current as App).mapControlViewModel;
            myManualControls.DataContext = (Application.Current as App).manualControlsViewModel;
            //dashboard.DataContext = flightSimulatorViewModel;
            //Joystick.MyEvent += SetXY;
            cw = new ConnectWindow((Application.Current as App).flightSimulatorViewModel);
            ew = new ErrorsWindow((Application.Current as App).flightSimulatorViewModel);
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            if (!cw.IsLoaded)
            {
                // Click on the "Connect" button to insert ip and port.
                cw = new ConnectWindow((Application.Current as App).flightSimulatorViewModel);
                cw.Show();
            }
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            // Disconnecting from the server.
            (Application.Current as App).flightSimulatorViewModel.VM_Disconnect();
        }

        private void Error_Click(object sender, RoutedEventArgs e)
        {
            // Clicking on the error icon to see all the errors the user got from the app.
            if (!ew.IsLoaded)
            {
                ew = new ErrorsWindow((Application.Current as App).flightSimulatorViewModel);
                ew.Show();       
            }         
        }
    }
}
