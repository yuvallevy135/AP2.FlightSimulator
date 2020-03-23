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
using System.Windows.Media.Animation;

namespace FlightSimulatorApp.Views
{
	/// <summary>
	/// Interaction logic for Joystick.xaml
	/// </summary>
	/// comment
	public partial class Joystick : UserControl
	{
		private Point firstPoint;
		private volatile bool isMousePressed;
		double to_x, to_y;

		public Joystick()
		{
			InitializeComponent();
			firstPoint = new Point(this.Base.Width / 2, this.Base.Height / 2);
			isMousePressed = false;
			Storyboard storyboard = this.Knob.Resources["MoveKnob"] as Storyboard;
			DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
			DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
			x.From = this.firstPoint.X;
			y.From = this.firstPoint.Y;
		}

		private void Movement()
		{
			Storyboard storyboard = this.Knob.Resources["MoveKnob"] as Storyboard;
			DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
			DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
			x.To = this.to_x - this.firstPoint.X;
			y.To = this.to_y - this.firstPoint.Y;
			storyboard.Begin();
			x.From = x.To;
			y.From = y.To;
			
		}

		private void Knob_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
		{
			this.isMousePressed = true;
		}

		private void Base_MouseMove_1(object sender, MouseEventArgs e)
		{
			if (isMousePressed)
			{
				to_x = e.GetPosition(this.Base).X;
				to_y = e.GetPosition(this.Base).Y;
				Movement();
			}
		}

		private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
		{
			if (this.isMousePressed)
			{
				this.MoveToCenter();
			}
		}

		private void Base_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.MoveToCenter();
		}

		private void MoveToCenter()
		{
			this.isMousePressed = false;
			this.to_x = this.firstPoint.X;
			this.to_y = this.firstPoint.Y;
			this.Movement();
		}


	}
}
