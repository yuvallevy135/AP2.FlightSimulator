using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using FlightSimulatorApp.ViewModels;

namespace FlightSimulatorApp.Models
{
	class FlightSimulatorModel : BaseNotify
	{
		private ITelnetClient telnetClient;
		private bool stop = false;
		private double heading;
		private double verticalSpeed;
		private double groundSpeed;
		private double airSpeed;
		private double altitude;
		private double roll;
		private double pitch;
		private double altimeter;
		private double rudder;
		private double elevator;
		private double throttle;
		private double aileron;

		public FlightSimulatorModel()
		{
		}
		public void Connect(string ip, int port)
		{
			telnetClient.Connect(ip, port);
		}
		public void Disconnect()
		{
			this.stop = true;
			telnetClient.Disconnect();
		}
		public void Start()
		{
			new Thread(delegate ()
			{
				while (!stop)
				{
					telnetClient.Write("get Heading ");
					telnetClient.Write("get Heading ");
					telnetClient.Write("get Heading ");
					telnetClient.Write("get Heading ");

					Heading = Double.Parse(telnetClient.Read());




					Thread.Sleep(2000);
				}

			}).Start();
		}

		public double Heading
		{
			get { return heading; }
			set
			{
				heading = value;
				NotifyPropertyChanged("heading");
			}
		}
		public double VerticalSpeed
		{
			get { return verticalSpeed; }
			set
			{
				heading = value;
				NotifyPropertyChanged("VerticalSpeed");
			}
		}
		public double GroundSpeed
		{
			get { return groundSpeed; }
			set
			{
				heading = value;
				NotifyPropertyChanged("GroundSpeed");
			}
		}
		public double AirSpeed
		{
			get { return airSpeed; }
			set
			{
				heading = value;
				NotifyPropertyChanged("AirSpeed");
			}
		}
		public double Altitude
		{
			get { return altitude; }
			set
			{
				heading = value;
				NotifyPropertyChanged("Altitude");
			}
		}
		public double Roll
		{
			get { return roll; }
			set
			{
				heading = value;
				NotifyPropertyChanged("Roll");
			}
		}
		public double Pitch
		{
			get { return pitch; }
			set
			{
				heading = value;
				NotifyPropertyChanged("Pitch");
			}
		}
		public double Altimeter
		{
			get { return altimeter; }
			set
			{
				heading = value;
				NotifyPropertyChanged("Altimeter");
			}
		}
		public double Rudder
		{
			get { return rudder; }
			set
			{
				rudder = value;
				NotifyPropertyChanged("Rudder");
			}
		}
		public double Elevator
		{
			get { return elevator; }
			set
			{
				heading = value;
				NotifyPropertyChanged("Elevator");
			}
		}
		public double Throttle
		{
			get { return throttle; }
			set
			{
				heading = value;
				NotifyPropertyChanged("Throttle");
			}
		}
		public double Aileron
		{
			get { return aileron; }
			set
			{
				heading = value;
				NotifyPropertyChanged("Aileron");
			}
		}
	}
}
