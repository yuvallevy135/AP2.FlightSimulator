using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.ViewModels;

namespace FlightSimulatorApp.Models
{
	public class DashBoardModel : BaseNotify
	{
		
		public DashBoardModel()
		{
			
		}

		public double Heading { 
			get
			{
				return 0;
			}

			set{} 
		}
		public double VerticalSpeed
		{
			get
			{
				return 0;
			}

			set { }
		}
		public double GroundSpeed
		{
			get
			{
				return 0;
			}

			set { }
		}
		public double AirSpeed
		{
			get
			{
				return 0;
			}

			set { }
		}
		public double Altitude
		{
			get
			{
				return 0;
			}

			set { }
		}
		public double Roll
		{
			get
			{
				return 0;
			}

			set { }
		}
		public double Pitch
		{
			get
			{
				return 0;
			}

			set { }
		}
		public double Altimeter
		{
			get
			{
				return 0;
			}

			set { }
		}
	}
}
