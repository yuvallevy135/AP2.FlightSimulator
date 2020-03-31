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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        private double newLatitude;
        private double newLongitude;
        private double currentLatitude;
        private double currentLongitude;
        private double latitudeChange;
        private double longitudeChange;
        private double currDeg, newDeg;

        public MapControl()
        {
            InitializeComponent();
            currentLatitude = 0;
            currentLongitude = 0;
            currDeg = 0;
            newDeg = 0;
            latitudeChange = 0;
            longitudeChange = 0;
        }
        
        private void PlaneRotate()
        {
            if (longitudeChange != 0)
            {
                newDeg = Math.Atan(latitudeChange / longitudeChange) * (180/Math.PI);
                var doubleAnimation = new DoubleAnimation(currDeg, newDeg, new Duration(TimeSpan.FromSeconds(1)));
                var rotateTransform = new RotateTransform();
                planeIcon.RenderTransform = rotateTransform;
                planeIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                //doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
                //planeIcon.RenderTransform = new RotateTransform(newDeg);
                //currDeg = newDeg;
            }
        }

        private void longitudeVal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            newLongitude = Double.Parse(longitudeVal.Text);
            longitudeChange = newLongitude - currentLongitude;
            currentLongitude = newLongitude;
            PlaneRotate();
        }

        private void latitudeVal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            newLatitude = Double.Parse(latitudeVal.Text);
            latitudeChange = newLatitude - currentLatitude;
            currentLatitude = newLatitude;
            PlaneRotate();
        }
    }
}
