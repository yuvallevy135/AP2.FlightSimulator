using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using FlightSimulatorApp.ViewModels;

namespace FlightSimulatorApp.Models
{
	public class FlightSimulatorModel : BaseNotify
	{
		private ITelnetClient telnetClient;
        private volatile bool stop = false;
		private double heading;
		private double verticalSpeed;
		private double groundSpeed;
		private double airSpeed;
		private double altitude;
		private double roll;
		private double pitch;
		private double altimeter;
        private double latitude;
        private double longitude;
        private double rudder;
		private double elevator;
		private double throttle;
		private double aileron;
		private string location;
		private string status;

		public FlightSimulatorModel(ITelnetClient telnetC)
        {
            telnetClient = telnetC;
            status = "Disconnected";
            //telnetClient.Connect("127.0.0.1", 5402); //change later

        }
		public void Connect(string ip, int port)
		{
			telnetClient.Connect(ip, port);
			Status = "Connected";
			stop = false;
			StartReading();
		}
		public void Disconnect()
		{
			Status = "Disconnected";
			stop = true;
			telnetClient.Disconnect();
		}
		public void StartReading()
		{
			new Thread(delegate ()
			{
				while (!stop)
                {
					//reading Dashboard elements from the simulator
                    AirSpeed = Double.Parse(telnetClient.Read("/instrumentation/airspeed-indicator/indicated-speed-kt"));
                    Altitude = Double.Parse(telnetClient.Read("/instrumentation/gps/indicated-altitude-ft"));
                    Roll = Double.Parse(telnetClient.Read("/instrumentation/attitude-indicator/internal-roll-deg"));
                    Pitch = Double.Parse(telnetClient.Read("/instrumentation/attitude-indicator/internal-pitch-deg"));
                    Altimeter = Double.Parse(telnetClient.Read("/instrumentation/altimeter/indicated-altitude-ft"));
					Heading = Double.Parse(telnetClient.Read("/instrumentation/heading-indicator/indicated-heading-deg"));
                    GroundSpeed = Double.Parse(telnetClient.Read("/instrumentation/gps/indicated-ground-speed-kt"));
					VerticalSpeed = Double.Parse(telnetClient.Read("/instrumentation/gps/indicated-vertical-speed"));

                    //reading map values from the simulator
					Latitude = Double.Parse(telnetClient.Read("/position/latitude-deg"));
					Longitude = Double.Parse(telnetClient.Read("/position/longitude-deg"));
					Location = Convert.ToString(latitude + "," + longitude);


					////debug prints for controls
					//               Console.WriteLine("throttle: " + telnetClient.Read("/controls/engines/current-engine/throttle") + "\n");
					//Console.WriteLine("aileron: " + telnetClient.Read("/controls/flight/aileron") + "\n");
					//               Console.WriteLine("elevator: " + telnetClient.Read("/controls/flight/elevator") + "\n");
					//               Console.WriteLine("rudder: " + telnetClient.Read("/controls/flight/rudder") + "\n");

					Thread.Sleep(250);
				}

			}).Start();
		}

        public void StartWriting(string command)
        {
            telnetClient.Write(command); 
        }



		public double Heading
		{
			get { return heading; }
			set
			{
				heading = value;
				NotifyPropertyChanged("Heading");
			}
		}
		public double VerticalSpeed
		{
			get { return verticalSpeed; }
			set
			{
				verticalSpeed = value;
				NotifyPropertyChanged("VerticalSpeed");
			}
		}
		public double GroundSpeed
		{
			get { return groundSpeed; }
			set
			{
                groundSpeed = value;
				NotifyPropertyChanged("GroundSpeed");
			}
		}
		public double AirSpeed
		{
			get { return airSpeed; }
			set
			{
                airSpeed = value;
				NotifyPropertyChanged("AirSpeed");
			}
		}
		public double Altitude
		{
			get { return altitude; }
			set
			{
                altitude = value;
				NotifyPropertyChanged("Altitude");
			}
		}
		public double Roll
		{
			get { return roll; }
			set
			{
                roll = value;
				NotifyPropertyChanged("Roll");
			}
		}
		public double Pitch
		{
			get { return pitch; }
			set
			{
                pitch = value;
				NotifyPropertyChanged("Pitch");
			}
		}
		public double Altimeter
		{
			get { return altimeter; }
			set
			{
                altimeter = value;
				NotifyPropertyChanged("Altimeter");
			}
		}

		public string Location { 
			get 
			{
				return location;
			}
			set 
			{
				location = value;
				NotifyPropertyChanged("Location");
			}
		}
		public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }

		public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }

		public string Status
		{
			get { return status; }

			set
			{
				status = value;
				NotifyPropertyChanged("Status");
			}
		}


        public double Rudder
		{
			get { return rudder; }
			set
			{
                rudder = value;
				//NotifyPropertyChanged("Rudder");
			}
		}
		public double Elevator
		{
			get { return elevator; }
			set
			{
                elevator = value;
				//NotifyPropertyChanged("Elevator");
			}
		}
		public double Throttle
		{
			get { return throttle; }
			set
			{
                throttle = value;
                //NotifyPropertyChanged("Throttle");
			}
		}
		public double Aileron
		{
			get { return aileron; }
			set
			{
                aileron = value;
				//NotifyPropertyChanged("Aileron");
			}
		}


    }
}
