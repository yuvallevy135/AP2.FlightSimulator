using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulatorApp.Models;

namespace FlightSimulatorApp.ViewModels
{
    public class FlightSimulatorViewModel : BaseNotify
    {
        private FlightSimulatorModel flightSimulatorModel;

        public FlightSimulatorViewModel(FlightSimulatorModel model)
        {
            flightSimulatorModel = model;
            flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        //public void Notify()
        //{
        //    flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
        //        NotifyPropertyChanged("VM_" + e.PropertyName);
        //    };
        //}

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

        //Manual Controls start
        public double VM_Throttle
        {
            get
            {
                return flightSimulatorModel.Throttle;
            }
            set
            {
                string stringThrottle = "set /controls/engines/current-engine/throttle";
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                stringThrottle += value.ToString();
                //flightSimulatorModel.
            }
        }
        public double VM_Rudder
        {
            get
            {
                return flightSimulatorModel.Rudder;
            }
            set
            {
                string stringRudder = "set /controls/flight/rudder";
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < -1)
                {
                    value = -1;
                }
                stringRudder += value.ToString();
            }
        }
        public double VM_Aileron
        {
            get
            {
                return flightSimulatorModel.Aileron;
            }
            set
            {

            }
        }
        public double VM_Elevator
        {
            get
            {
                return flightSimulatorModel.Elevator;
            }
            set
            {

            }
        }
        //Manual Controls end
    }
}
