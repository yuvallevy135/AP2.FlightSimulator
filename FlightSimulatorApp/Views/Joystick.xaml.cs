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
        private enum BoundState  {
            MOVE,STAY,CENTER
        };
		private Point centerPoint;
		private volatile bool isMousePressed;
        private double from_x ,from_y, to_x, to_y, ellipseRadius, relativeBorder = 0.5;
        private double normalX = 0, normalY = 0;

		public delegate void Event (double x, double y);

        public Event MyEvent;

        public Joystick()
		{
			InitializeComponent();

			centerPoint = new Point(Base.Width / 2, Base.Height / 2);
            // radius = (Base.Width - centerPoint.X) * 0.235;
            ellipseRadius = this.borderCircle.Width / 2;
            //Console.WriteLine(borderCircle.Width/2);
            //Console.WriteLine(borderCircle.Width);
            //Console.WriteLine(Base.Width);
			isMousePressed = false;
			Storyboard storyboard = Knob.Resources["MoveKnob"] as Storyboard;
			DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
			DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
			x.From = 0;
			y.From = 0;
            from_x = 0;
            from_y = 0;
            //Console.WriteLine(centerPoint.X);
            //Console.WriteLine(centerPoint.Y);
        }

        private BoundState CheckBound()
        {
            double bound = Math.Sqrt(Math.Pow(to_x - this.centerPoint.X, 2) + Math.Pow(to_y - this.centerPoint.Y, 2));
            if (this.ellipseRadius* relativeBorder > bound)
            {
                return BoundState.MOVE;
            } else if (this.ellipseRadius > bound)
            {
                return BoundState.STAY;
            }
            else
            {
                return BoundState.STAY;
            }
        }

		private void Movement()
		{
            switch (CheckBound())
            {
				case BoundState.MOVE:
                    Storyboard storyboard = Knob.Resources["MoveKnob"] as Storyboard;
                    DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
                    DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
                    x.To = to_x - centerPoint.X;
                    y.To = to_y - centerPoint.Y;
                    storyboard.Begin();
                    x.From = x.To;
                    y.From = y.To;
                    from_x = x.From.Value;
                    from_y = y.From.Value;
                    //Console.WriteLine(x.To);
                    //Console.WriteLine(y.To);

					break;
				case BoundState.STAY:
                    break;
				case BoundState.CENTER:
					MoveToCenter();
					break;
				default:
                    break;
            }
			Normal();
        }

        //public double GetNormalX()
        //{
        //    return normalX;
        //}

        //public double GetNormalY()
        //{
        //    return normalY;
        //}

		private void Normal()
        {
            //normalize
            normalX = ((to_x - centerPoint.X) / (ellipseRadius * relativeBorder ));
			normalY = ((to_y - centerPoint.Y) / (ellipseRadius * relativeBorder));

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
