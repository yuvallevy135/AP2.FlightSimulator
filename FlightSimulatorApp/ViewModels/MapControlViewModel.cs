using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Models;

namespace FlightSimulatorApp.ViewModels
{
	public class MapControlViewModel : BaseNotify
	{
		private FlightSimulatorModel flightSimulatorModel;
		public MapControlViewModel(FlightSimulatorModel model)
		{
            flightSimulatorModel = model;
            flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged(e.PropertyName);
			};
		}
	}
}
