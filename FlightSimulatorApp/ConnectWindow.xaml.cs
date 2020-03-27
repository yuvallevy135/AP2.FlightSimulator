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
			portBlock.Text = ConfigurationManager.AppSettings.Get("Port");
			ipBlock.Text = ConfigurationManager.AppSettings.Get("IP");
		}

		private void connect_server_Click(object sender, RoutedEventArgs e)
		{
			vm.VM_Ip = ipBlock.Text;
			vm.VM_Port = portBlock.Text;
			vm.VM_Connect();
			this.Hide();
		}

		private void cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
		}
	}
}
