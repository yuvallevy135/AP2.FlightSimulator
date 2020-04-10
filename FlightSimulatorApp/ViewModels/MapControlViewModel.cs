using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Models;

namespace FlightSimulatorApp.ViewModels
{
    public class MapControlViewModel : BaseNotify
    {
        private FlightSimulatorModel flightSimulatorModel;

        public MapControlViewModel(FlightSimulatorModel model)
        {
            flightSimulatorModel = model;
            flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        // Map starts.
        public string VM_Location
        {
            get
            {
                return flightSimulatorModel.Location;
            }
        }
        public double VM_Latitude
        {
            get { return Double.Parse(flightSimulatorModel.Latitude); }
        }

        public double VM_Longitude
        {
            get { return Double.Parse(flightSimulatorModel.Longitude); }
        }

        // Map ends.

    }
}
