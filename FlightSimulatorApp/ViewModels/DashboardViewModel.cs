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
            get { return Double.Parse(flightSimulatorModel.Heading); }
        }

        public double VM_VerticalSpeed
        {
            get { return Double.Parse(flightSimulatorModel.VerticalSpeed); }
        }

        public double VM_GroundSpeed
        {
            get { return Double.Parse(flightSimulatorModel.GroundSpeed); }
        }

        public double VM_AirSpeed
        {
            get { return Double.Parse(flightSimulatorModel.AirSpeed); }
        }

        public double VM_Altitude
        {
            get { return Double.Parse(flightSimulatorModel.Altitude); }
        }

        public double VM_Roll
        {
            get { return Double.Parse(flightSimulatorModel.Roll); }
        }

        public double VM_Pitch
        {
            get { return Double.Parse(flightSimulatorModel.Pitch); }
        }

        public double VM_Altimeter
        {
            get
            {
                return Double.Parse(flightSimulatorModel.Altimeter);

            }
        }

        //Dashboard end

    }
}
