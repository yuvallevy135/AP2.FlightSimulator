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
using System.Windows.Shapes;
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

		private void connect_server_Click(object sender, RoutedEventArgs e)
		{
			// Getting the Ip and Port that the user chose.
			vm.VM_Ip = ipBlock.Text;
			vm.VM_Port = portBlock.Text;
			// Running the connect VM to open a connection with the silmulator.
			vm.VM_Connect();
			this.Close();
			
		}

		private void cancel_Click(object sender, RoutedEventArgs e)
		{
			// Clicking canel to close the window. The use regretted chosing ip and port. 
			this.Close();
		}
	}
}
