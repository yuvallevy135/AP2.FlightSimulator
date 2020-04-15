using System;

using System.Windows;



namespace FlightSimulatorApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        private ConnectWindow cw;
        private ErrorsWindow ew;

        public MainWindow()
		{
            InitializeComponent();
            // Data context initialization.
            DataContext = (Application.Current as App).flightSimulatorViewModel;
            myDashboard.DataContext = (Application.Current as App).dashboardViewModel;
            myMapControl.DataContext = (Application.Current as App).mapControlViewModel;
            myManualControls.DataContext = (Application.Current as App).manualControlsViewModel;
            // Connect window and ErrorsWindow initialization.
            cw = new ConnectWindow((Application.Current as App).flightSimulatorViewModel);
            ew = new ErrorsWindow((Application.Current as App).flightSimulatorViewModel);
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (!cw.IsLoaded)
            {
                // Click on the "Connect" button to insert ip and port.
                cw = new ConnectWindow((Application.Current as App).flightSimulatorViewModel);
                cw.Show();
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
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
