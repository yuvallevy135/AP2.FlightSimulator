using FlightSimulatorApp.ViewModels;
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

namespace FlightSimulatorApp
{
	/// <summary>
	/// Interaction logic for ErrorsWindow.xaml
	/// </summary>
	public partial class ErrorsWindow : Window
	{
		FlightSimulatorViewModel vm;
		double currentStartingX = 0, currentStartingY = 0;
		public ErrorsWindow(FlightSimulatorViewModel flightSimulatorViewModel)
		{
			InitializeComponent();
			vm = flightSimulatorViewModel;
            DataContext = vm;
        }

		private void TextBlock_TargetUpdated(object sender, DataTransferEventArgs e)
		{
			//TextBlock textBlock = new TextBlock() { Text = vm.VM_Err, Width = Double.NaN, Height = 40, FontSize = 20 };
			//textBlock.RenderTransform = new TranslateTransform
			//{
			//	X = currentStartingX,
			//	Y = currentStartingY,
			//};
			//currentStartingY += textBlock.Height;
		}

		private void clear_Click(object sender, RoutedEventArgs e)
		{
			// Click clear to clear all the error screen from all the old errors the user got.
			errorText.Text = "";
		}

		private void close_Click(object sender, RoutedEventArgs e)
		{
			// Closing the window.
			this.Close();
		}

		private void TextBlock_SourceUpdated(object sender, DataTransferEventArgs e)
		{
			//TextBlock textBlock = new TextBlock() { Text = vm.VM_Err, Width = Double.NaN, Height = 40, FontSize = 20 };
			//textBlock.RenderTransform = new TranslateTransform
			//{
			//	X = currentStartingX,
			//	Y = currentStartingY,
			//};
			//currentStartingY += textBlock.Height;
		}

		//public void temp()
		//{
			
		//}
	}
}
