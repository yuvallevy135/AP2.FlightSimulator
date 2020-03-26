﻿using System;
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

        //map start
        public double VM_Latitude
        {
            get { return flightSimulatorModel.Latitude; }
        }

        public double VM_Longitude
        {
            get { return flightSimulatorModel.Longitude; }
        }

        //map end


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
            set
            {
                string stringThrottle = "set /controls/engines/current-engine/throttle ";
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                stringThrottle += value.ToString();
                flightSimulatorModel.StartWriting(stringThrottle);
            }
        }

        public double VM_Rudder
        {
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
                flightSimulatorModel.StartWriting(stringRudder);
            }
        }

        public double VM_Aileron
        {
            set
            {
                string stringAileron = "set /controls/flight/aileron ";
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < -1)
                {
                    value = -1;
                }
                stringAileron += value.ToString();
                flightSimulatorModel.StartWriting(stringAileron);
            }
        }

        public double VM_Elevator
        {
            set
            {
                string stringElevator = "set /controls/flight/elevator ";
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < -1)
                {
                    value = -1;
                }
                stringElevator += value.ToString();
                flightSimulatorModel.StartWriting(stringElevator);
            }
        }
        //Manual Controls end
    }
}