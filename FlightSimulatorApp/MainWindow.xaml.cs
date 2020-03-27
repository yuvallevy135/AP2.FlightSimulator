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
        private FlightSimulatorViewModel vm;
		private ConnectWindow cw;
		public MainWindow()
		{
		
			InitializeComponent();
			vm = new FlightSimulatorViewModel(new FlightSimulatorModel(new MyTelnetClient()));
			DataContext = vm;
			cw = new ConnectWindow(vm);


			//model.Connect("127.0.0.1", 5402);
			//model.StartReading();

		}

		private void Throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{

		}

		private void Joystick_Loaded(object sender, RoutedEventArgs e)
		{

		}

        private void AileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AileronValue.Text = AileronSlider.Value.ToString();
        }

        private void ThrottleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ThrottleValue.Text = ThrottleSlider.Value.ToString();
        }


		private void connect_Click(object sender, RoutedEventArgs e)
		{
			//connectWindow.Show();
			//this.vm.VM_ConnectWindow();
			cw.Show();
		}

		private void disconnect_Click(object sender, RoutedEventArgs e)
		{
			vm.VM_Disconnect();
		}
	}
}
