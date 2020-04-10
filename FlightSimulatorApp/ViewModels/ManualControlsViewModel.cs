using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Models;

namespace FlightSimulatorApp.ViewModels
{
    public class ManualControlsViewModel : BaseNotify
    {

        private FlightSimulatorModel flightSimulatorModel;

        public ManualControlsViewModel(FlightSimulatorModel model)
        {
            flightSimulatorModel = model;
            flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }


        // Manual Controls start.
        public double VM_Throttle
        {
            set
            {
                // Checking that the value is valid.
                if (Math.Abs(value - flightSimulatorModel.Throttle) > 0.1)
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
        }

        public double VM_Rudder
        {
            set
            {
                // Checking that the value is valid.
                if (Math.Abs(value - flightSimulatorModel.Rudder) > 0.1)
                {
                    string stringRudder = "set /controls/flight/rudder ";
                    stringRudder += value.ToString();
                    flightSimulatorModel.StartWriting(stringRudder);
                }
                    
            }
        }

        public double VM_Aileron
        {
            set
            {
                // Checking that the value is valid.
                if (Math.Abs(value - flightSimulatorModel.Aileron) > 0.1)
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
        }

        public double VM_Elevator
        {
            set
            {
                // Checking that the value is valid.
                if (Math.Abs(value - flightSimulatorModel.Elevator) > 0.1)
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
        }
        // Manual Controls end.

    }
}
