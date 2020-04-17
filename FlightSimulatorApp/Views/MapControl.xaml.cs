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
        public MapControl()
        {
            InitializeComponent();
            currentLatitude = 32.005232;
            currentLongitude = 34.886709;
        }
        
        private void LongitudeVal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            newLongitude = Double.Parse(LongitudeVal.Text);
            // Centers the plane in the middle of the map.
            MyMap.Center = new Location(currentLatitude, currentLongitude);
            currentLongitude = newLongitude;
        }

        private void LatitudeVal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            newLatitude = Double.Parse(LatitudeVal.Text);
            // Centers the plane in the middle of the map.
            MyMap.Center = new Location(currentLatitude, currentLongitude);
            currentLatitude = newLatitude;
        }
    }
}
