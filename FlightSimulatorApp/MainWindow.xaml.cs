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

namespace FlightSimulatorApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			TcpClient t = new TcpClient();
			t.Connect("127.0.0.1", 5402);
			reading(t, "get /position/latitude-deg\n");
			reading(t, "get /instrumentation/heading-indicator/indicated-heading-deg\n");
		}

		private void reading(TcpClient t, string command)
		{
			byte[] read = Encoding.ASCII.GetBytes(command);
			t.GetStream().Write(read, 0, read.Length);
			byte[] buffer = new byte[64];
			t.GetStream().Read(buffer, 0, 64);
			string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
			Console.WriteLine(data);
		}

		private void Throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{

		}

		private void Joystick_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
