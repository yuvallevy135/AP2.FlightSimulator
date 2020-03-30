using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FlightSimulatorApp.ViewModels;
using Timer = System.Threading.Timer;

namespace FlightSimulatorApp.Models
{
	public class FlightSimulatorModel : BaseNotify
	{
		private ITelnetClient telnetClient;

        private volatile bool stop = false;

        private string headingAddress, verticalSpeedAddress, groundSpeedAddress, airSpeedAddress, altitudeAddress, rollAddress, pitchAddress,
            altimeterAddress, latitudeAddress, longitudeAddress, rudderAddress, elevatorAddress, throttleAddress, aileronAddress, locationAddress;
        private double heading, verticalSpeed, groundSpeed, airSpeed, altitude, roll, pitch, altimeter, latitude, longitude, rudder, elevator, throttle, aileron;
        private string location, status;
        private double maxLatitude = 85, minLatitude = -85, maxLongitude = 180, minLongitude = -180;

        private string headingRead, verticalSpeedRead, groundSpeedRead,
            airSpeedRead, altitudeRead, rollRead, pitchRead, altimeterRead, latitudeRead, longitudeRead;
        
		public FlightSimulatorModel(ITelnetClient telnetC)
        {
            initializeModel();
            telnetClient = telnetC;
            status = "Disconnected";
            //telnetClient.Connect("127.0.0.1", 5402); //change later

        }
		public void Connect(string ip, int port)
        { 
			Task.Run(() => telnetClient.Connect(ip, port));
            Status = "Connected";
			stop = false;
			StartReading();
		}

		public void Disconnect()
		{
			Status = "Disconnected";
			stop = true;
			telnetClient.Disconnect();
            initializeDashboard();
        }
		public void StartReading()
		{
			new Thread(delegate ()
			{
				while (!stop)
                {
					//reading Dashboard elements from the simulator
                    try
                    {
                        airSpeedRead = telnetClient.Read(airSpeedAddress);
                        altitudeRead = telnetClient.Read(altitudeAddress);
                        rollRead = telnetClient.Read(rollAddress);
                        pitchRead = telnetClient.Read(pitchAddress);
                        altimeterRead = telnetClient.Read(altimeterAddress);
						headingRead = telnetClient.Read(headingAddress);
                        groundSpeedRead = telnetClient.Read(groundSpeedAddress);
                        verticalSpeedRead = telnetClient.Read(verticalSpeedAddress);
                        //reading map values from the simulator
                        latitudeRead = telnetClient.Read(latitudeAddress);
                        longitudeRead = telnetClient.Read(longitudeAddress);
                        Location = Convert.ToString(latitude + "," + longitude);

						if (isFormatValid(airSpeedRead))
						{
							AirSpeed = Double.Parse(airSpeedRead);
						}

						if (isFormatValid(altitudeRead))
						{
							Altitude = Double.Parse(altitudeRead);
						}

						if (isFormatValid(rollRead))
						{
							Roll = Double.Parse(rollRead);
						}

						if (isFormatValid(pitchRead))
						{
							Pitch = Double.Parse(pitchRead);
						}

						if (isFormatValid(altimeterRead))
						{
							Altimeter = Double.Parse(altimeterRead);
						}

						if (isFormatValid(headingRead))
						{
							Heading = Double.Parse(headingRead);
						}

						if (isFormatValid(groundSpeedRead))
						{
							GroundSpeed = Double.Parse(groundSpeedRead);
						}

						if (isFormatValid(verticalSpeedRead))
						{
							VerticalSpeed = Double.Parse(verticalSpeedRead);
						}

						if (isFormatValid(latitudeRead))
						{
							Latitude = Double.Parse(latitudeRead);
						}

						if (isFormatValid(longitudeRead))
						{
							Longitude = Double.Parse(longitudeRead);
						}

						Thread.Sleep(250);
                    }
                    catch (ArgumentNullException nullException)
                    {
                        Disconnect();
					}
                }

			}).Start();
		}

        public bool isFormatValid(string valueRead)
        {
            try
            { 
                Double.Parse(valueRead);
                return true;
            }
			catch (FormatException formatException)
            {
                Console.WriteLine("format exception detected");
                //Disconnect();
                return false;
            }
		}

        public void initializeDashboard()
        {
            AirSpeed = 0;
            Altitude = 0;
            Roll = 0;
            Pitch = 0;
            Altimeter = 0;
            Heading = 0;
            GroundSpeed = 0;
            VerticalSpeed = 0;
            //reading map values from the simulator
            Latitude = 0;
            Longitude = 0;
            Location = Convert.ToString(latitude + "," + longitude);
		}

		public async Task StartWriting(string command)
        {
            await Task.Run(() => telnetClient.Write(command)); 
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
               // Console.WriteLine("latitude value: " + value);
				if (value > maxLatitude)
                {
                    latitude= maxLatitude - 1;
                }
				else if (value < minLatitude)
                {
                    latitude = minLatitude + 1;
                }
               // Console.WriteLine("latitude: " + latitude);
				NotifyPropertyChanged("Latitude");
            }
        }

		public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
				//Console.WriteLine("longitude value: " + value);

                if (value > maxLongitude)
                {
                    longitude = maxLongitude - 1;
                }
                else if (value < minLongitude)
                {
                    longitude = minLongitude + 1;
                }
                //Console.WriteLine("longitude: " + longitude);
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

        private void initializeModel()
        {
            airSpeedAddress = "/instrumentation/airspeed-indicator/indicated-speed-kt";
            altitudeAddress = "/instrumentation/gps/indicated-altitude-ft";
            rollAddress = "/instrumentation/attitude-indicator/internal-roll-deg";
            pitchAddress = "/instrumentation/attitude-indicator/internal-pitch-deg";
            altimeterAddress = "/instrumentation/altimeter/indicated-altitude-ft";
            headingAddress = "/instrumentation/heading-indicator/indicated-heading-deg";
            groundSpeedAddress = "/instrumentation/gps/indicated-ground-speed-kt";
            verticalSpeedAddress = "/instrumentation/gps/indicated-vertical-speed";
            //reading map values from the simulator
            latitudeAddress = "/position/latitude-deg";
            longitudeAddress = "/position/longitude-deg";
		}
    }
}
