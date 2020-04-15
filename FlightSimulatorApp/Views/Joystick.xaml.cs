using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FlightSimulatorApp.Views
{
    /// <summary>
	/// Interaction logic for Joystick.xaml
	/// </summary>
	/// comment
	public partial class Joystick : UserControl
    {
        private enum BoundState  {
            Move,Stay,Center
        };
		private Point centerPoint;
		private volatile bool isMousePressed;
        private double to_x, to_y, ellipseRadius, norm = 1.02;
        private double normalX = 0, normalY = 0;

		public delegate void Event (double x, double y);

        public Event MyEvent;

        public Joystick()
		{
			InitializeComponent();

			centerPoint = new Point(Base.Width / 2, Base.Height / 2);
            ellipseRadius = this.borderCircle.Width / 2;
            isMousePressed = false;
			Storyboard storyboard = Knob.Resources["CenterKnob"] as Storyboard;
			DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
			DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
			x.From = 0;
			y.From = 0;
        }

        private BoundState CheckBound()
        {
            double bound = Math.Sqrt(Math.Pow(to_x - this.centerPoint.X, 2) + Math.Pow(to_y - this.centerPoint.Y, 2));
            if (this.ellipseRadius * norm > bound)
            {
                return BoundState.Move;
            } 
            return BoundState.Center;
        }

		private void Movement()
		{
            switch (CheckBound())
            {
				case BoundState.Move:
                    Storyboard storyboard = Knob.Resources["CenterKnob"] as Storyboard;
                    DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
                    DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
                    x.To = to_x - centerPoint.X;
                    y.To = to_y - centerPoint.Y;
                    storyboard.Begin();
                    x.From = x.To;
                    y.From = y.To;

                    break;
				case BoundState.Stay:
                    break;
				case BoundState.Center:
					MoveToCenter();
					break;
				default:
                    break;
            }
			Normal();
        }

        private void Normal()
        {
            //normalize
            normalX = ((to_x - centerPoint.X) / (ellipseRadius));
			normalY = ((to_y - centerPoint.Y) / (ellipseRadius));

			if (MyEvent != null)
            {
                MyEvent(normalX, -normalY);
            }
        }

		private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			isMousePressed = true;
		}

		private void Base_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMousePressed)
			{
                to_x = e.GetPosition(Base).X;
				to_y = e.GetPosition(Base).Y;
				Movement();
			}
		}

		private void UserControl_MouseLeave(object sender, MouseEventArgs e)
		{
			MoveToCenter();
		}

		private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
		{
			if (isMousePressed)
			{
				MoveToCenter();
			}
		}

		private void Base_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MoveToCenter();
		}

		private void MoveToCenter()
		{
			isMousePressed = false;
			to_x = centerPoint.X;
			to_y = centerPoint.Y;
			Movement();
		}
    }
}
