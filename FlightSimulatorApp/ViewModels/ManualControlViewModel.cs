using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Models;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModels
{
	public class ManualControlViewModel : BaseNotify
	{
		private FlightSimulatorModel flightSimulatorModel;
		public ManualControlViewModel()
		{
            flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {

				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		public double VM_Throttle { 
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
				else if(value < -1)
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
	}
}
