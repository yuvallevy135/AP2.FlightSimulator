using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Models;

namespace FlightSimulatorApp.ViewModels
{
    public class DashboardViewModel : BaseNotify
    {

        private FlightSimulatorModel flightSimulatorModel;

        public DashboardViewModel()
        {
        }
        public DashboardViewModel(FlightSimulatorModel model)
        {
            flightSimulatorModel = model;
            flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }


        //Dashboard start
        public double VM_Heading
        {
            get { return flightSimulatorModel.Heading; }
        }

        public double VM_VerticalSpeed
        {
            get { return flightSimulatorModel.VerticalSpeed; }
        }

        public double VM_GroundSpeed
        {
            get { return flightSimulatorModel.GroundSpeed; }
        }

        public double VM_AirSpeed
        {
            get { return flightSimulatorModel.AirSpeed; }
        }

        public double VM_Altitude
        {
            get { return flightSimulatorModel.Altitude; }
        }

        public double VM_Roll
        {
            get { return flightSimulatorModel.Roll; }
        }

        public double VM_Pitch
        {
            get { return flightSimulatorModel.Pitch; }
        }

        public double VM_Altimeter
        {
            get
            {
                return flightSimulatorModel.Altimeter;

            }
        }

        //Dashboard end

    }
}
