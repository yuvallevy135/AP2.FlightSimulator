using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Models;

namespace FlightSimulatorApp.ViewModels
{
    public class DashBoardViewModel : BaseNotify
    {
        private FlightSimulatorModel flightSimulatorModel;
        public DashBoardViewModel(FlightSimulatorModel model)
        {
            flightSimulatorModel = model;
            flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {

                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public double VM_Heading
        {
            get { return flightSimulatorModel.Heading; }
        }

        public double VerticalSpeed
        {
            get { return flightSimulatorModel.VerticalSpeed; }
        }

        public double GroundSpeed
        {
            get { return flightSimulatorModel.GroundSpeed; }
        }

        public double AirSpeed
        {
            get { return flightSimulatorModel.AirSpeed; }
        }

        public double Altitude
        {
            get { return flightSimulatorModel.Altitude; }
        }

        public double Roll
        {
            get { return flightSimulatorModel.Roll; }
        }

        public double Pitch
        {
            get { return flightSimulatorModel.Pitch; }
        }

        public double Altimeter
        {
            get
            {
                return flightSimulatorModel.Altimeter; 

            }
        }
    }
}
