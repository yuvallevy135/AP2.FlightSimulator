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
            FlightSimulatorModel model = new FlightSimulatorModel();
			model.Connect("121", 5644);
			model.Start();
        }

        private void Throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{

		}

		private void Joystick_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
