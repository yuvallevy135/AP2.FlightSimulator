using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.ViewModels;

namespace FlightSimulatorApp.Models
{
	class ManualControlModel : BaseNotify
	{
		public ManualControlModel()
		{

		}
		public double Rudder
		{
			get
			{
				return 0;
			}
			set
			{

			}
		}
	}
}
