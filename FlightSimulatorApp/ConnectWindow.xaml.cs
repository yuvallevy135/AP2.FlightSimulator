using System;
using System.Windows;
using System.Configuration;

namespace FlightSimulatorApp
{
	/// <summary>
	/// Interaction logic for ConnectWindow.xaml
	/// </summary>
	public partial class ConnectWindow : Window
	{
		private ViewModels.FlightSimulatorViewModel vm;

		public ConnectWindow(ViewModels.FlightSimulatorViewModel viewModel)
		{
			InitializeComponent();
			vm = viewModel;
			DataContext = vm;
			// Getting the default Ip and Port for the app.
			portBlock.Text = ConfigurationManager.AppSettings.Get("Port");
			ipBlock.Text = ConfigurationManager.AppSettings.Get("IP");
		}

		private void Connect_server_Click(object sender, RoutedEventArgs e)
		{
			// Getting the Ip and Port that the user chose.
			vm.VM_Ip = ipBlock.Text;
			vm.VM_Port = portBlock.Text;
			// Running the connect VM to open a connection with the simulator.
			vm.VM_Connect();
			Close();
        }

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			// Clicking cancel to close the window. The use regretted choosing ip and port. 
			Close();
		}
	}
}
