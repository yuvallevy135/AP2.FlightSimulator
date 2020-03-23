using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModels
{
	public class MapControlViewModel : BaseNotify
	{
		private MapControlViewModel mapControlViewModel;
		public MapControlViewModel()
		{
			mapControlViewModel = new MapControlViewModel();
			mapControlViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged(e.PropertyName);
			};
		}
	}
}
