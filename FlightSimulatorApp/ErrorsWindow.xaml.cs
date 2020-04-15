using FlightSimulatorApp.ViewModels;
using System;
using System.Windows;


namespace FlightSimulatorApp
{
	/// <summary>
	/// Interaction logic for ErrorsWindow.xaml
	/// </summary>
	public partial class ErrorsWindow : Window
	{
		FlightSimulatorViewModel vm;
		public ErrorsWindow(FlightSimulatorViewModel flightSimulatorViewModel)
		{
			InitializeComponent();
			vm = flightSimulatorViewModel;
            DataContext = vm;
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
    }
}
