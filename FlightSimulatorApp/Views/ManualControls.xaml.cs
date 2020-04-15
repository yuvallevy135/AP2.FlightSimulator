using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            Joystick.MyEvent += SetXy;
            RudderValue.Text = "0";
            ElevatorValue.Text = "0";
        }

        private void SetXy(double x, double y)
        {
            if (x > 1)
            {
                x = 1;
            }
            else if (x < -1)
            {
                x = -1;
            }

            if (y > 1)
            {
                y = 1;
            }
            else if (y < -1)
            {
                y = -1;
            }       
            RudderValue.Text = x.ToString();
            ElevatorValue.Text = y.ToString();
        }

        private void AileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AileronValue.Text = AileronSlider.Value.ToString("F");
        }

        private void ThrottleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ThrottleValue.Text = ThrottleSlider.Value.ToString("F");
        }

        private void AileronSlider_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AileronSlider.Value = 0;
        }
    }
}
