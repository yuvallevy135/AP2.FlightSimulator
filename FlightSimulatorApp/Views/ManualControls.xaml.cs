using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace FlightSimulatorApp.Views
{
	/// <summary>
	/// Interaction logic for ManualControls.xaml
	/// </summary>
	public partial class ManualControls : UserControl
	{
		public ManualControls()
		{
			InitializeComponent();
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
	}

}
