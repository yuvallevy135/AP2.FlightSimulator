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
        // Joystick states.
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
			// The knob center point.
            centerPoint = new Point(Base.Width / 2, Base.Height / 2);
			// The radius the knob is allowed to travel in.
            ellipseRadius = borderCircle.Width / 2;
            isMousePressed = false;
			Storyboard storyboard = Knob.Resources["CenterKnob"] as Storyboard;
			DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
			DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
			x.From = 0;
			y.From = 0;
        }

		// Check the state the joystick should be in.
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
				// Moves the knob using animation.
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
			// Normalize the values.
			Normal();
        }

		// Normalizing the joystick x and y values to be between 0 and 1.
        private void Normal()
        {
            normalX = ((to_x - centerPoint.X) / (ellipseRadius));
			normalY = ((to_y - centerPoint.Y) / (ellipseRadius));
			// Add to my event.
			if (MyEvent != null)
            {
                MyEvent(normalX, -normalY);
            }
        }

		// Sets the isMousePressed flag to true when the user clicks on the knob with the left mouse button.
		private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			isMousePressed = true;
		}

		// Sets the location that the knob should move to.
		private void Base_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMousePressed)
			{
                to_x = e.GetPosition(Base).X;
				to_y = e.GetPosition(Base).Y;
				Movement();
			}
		}

        // Returning the knob to the center when the user stops holding the left mouse button.
		private void UserControl_MouseLeave(object sender, MouseEventArgs e)
		{
			MoveToCenter();
		}

        // Returning the knob to the center when the user leaves the ellipse's border.
		private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
		{
			if (isMousePressed)
			{
				MoveToCenter();
			}
		}

        // Returning the knob to the center when the user stops holding the left mouse button.
		private void Base_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MoveToCenter();
		}

		// Moving the knob to the center.
		private void MoveToCenter()
		{
			isMousePressed = false;
			to_x = centerPoint.X;
			to_y = centerPoint.Y;
			Movement();
		}
    }
}
