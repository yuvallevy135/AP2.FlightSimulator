using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Maps.MapControl.WPF;

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
                //myMap.Center = new Location(newLatitude,newLongitude);
                newDeg = Math.Atan(latitudeChange / longitudeChange) * (180/Math.PI);
                var doubleAnimation = new DoubleAnimation(currDeg, newDeg, new Duration(TimeSpan.FromSeconds(1)));
                var rotateTransform = new RotateTransform();
                PlaneIcon.RenderTransform = rotateTransform;
                PlaneIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                //doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, null);
                //planeIcon.RenderTransform = new RotateTransform(newDeg);
                //currDeg = newDeg;
            }
        }

        private void LongitudeVal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            newLongitude = Double.Parse(LongitudeVal.Text);
            longitudeChange = newLongitude - currentLongitude;
            MyMap.Center = new Location(currentLatitude, currentLongitude);
            currentLongitude = newLongitude;
            PlaneRotate();
        }

        private void LatitudeVal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            newLatitude = Double.Parse(LatitudeVal.Text);
            latitudeChange = newLatitude - currentLatitude;
            MyMap.Center = new Location(currentLatitude, currentLongitude);
            currentLatitude = newLatitude;
            PlaneRotate();
        }
    }
}
