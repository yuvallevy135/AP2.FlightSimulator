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
        //private double latitudeChange;
        //private double longitudeChange;
        //private double currDeg, newDeg;
        //private DoubleAnimation doubleAnimation;
        //private RotateTransform rotateTransform;
        //private double TOLERANCE = 0.00000001;
        public MapControl()
        {
            InitializeComponent();
            currentLatitude = 32.005232;
            currentLongitude = 34.886709;
            //currDeg = 0;
            //newDeg = 0;
            //latitudeChange = 0;
            //longitudeChange = 0;
            //doubleAnimation = new DoubleAnimation(currDeg, newDeg, new Duration(TimeSpan.FromSeconds(1)));
            //rotateTransform = new RotateTransform();
        }
        
        //private void PlaneRotate()
        //{
        //    if (((Math.Abs(longitudeChange) > TOLERANCE) && (Math.Abs(latitudeChange) > TOLERANCE)) && (latFlag) && (lonFlag))
        //    {
        //        //myMap.Center = new Location(newLatitude,newLongitude);
        //        newDeg = Math.Atan(latitudeChange / longitudeChange) * (180/Math.PI);
        //        //var doubleAnimation = new DoubleAnimation(currDeg, newDeg, new Duration(TimeSpan.FromSeconds(1)));
        //        //var rotateTransform = new RotateTransform();
        //        doubleAnimation.From = currDeg;
        //        doubleAnimation.To = newDeg;
        //        PlaneIcon.RenderTransform = rotateTransform;
        //        PlaneIcon.RenderTransformOrigin = new Point(0.5, 0.5);
        //        //doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
        //        //doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
        //        rotateTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
        //        //planeIcon.RenderTransform = new RotateTransform(newDeg);
        //        currDeg = newDeg;
        //    }
        //}

        private void LongitudeVal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            newLongitude = Double.Parse(LongitudeVal.Text);
           // longitudeChange = newLongitude - currentLongitude;
            MyMap.Center = new Location(currentLatitude, currentLongitude);
            currentLongitude = newLongitude;
            //PlaneRotate();
        }

        private void LatitudeVal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            newLatitude = Double.Parse(LatitudeVal.Text);
            //latitudeChange = newLatitude - currentLatitude;
            MyMap.Center = new Location(currentLatitude, currentLongitude);
            currentLatitude = newLatitude;
            //PlaneRotate();
        }
    }
}
