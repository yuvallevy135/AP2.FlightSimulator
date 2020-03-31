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
		double currentStartingX = 0, currentStartingY = 0;
		public ErrorsWindow()
		{
			InitializeComponent();
		}

		public void temp()
		{
			TextBlock textBlock = new TextBlock() {Text = "Text Block", Width = Double.NaN, Height = 40, FontSize = 20 };
			textBlock.RenderTransform = new TranslateTransform
			{
				X = currentStartingX,
				Y = currentStartingY,
			};
			currentStartingY += textBlock.Height;
		}
	}
}
