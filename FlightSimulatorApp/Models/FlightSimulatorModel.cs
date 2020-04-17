using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

using FlightSimulatorApp.ViewModels;

namespace FlightSimulatorApp.Models
{
	public class FlightSimulatorModel : BaseNotify
	{
		private ITelnetClient telnetClient;

        private volatile bool stop = false;
        private int port;
        private string headingAddress, verticalSpeedAddress, groundSpeedAddress, airSpeedAddress, altitudeAddress, rollAddress, pitchAddress,
            altimeterAddress, latitudeAddress, longitudeAddress;
        private string heading, verticalSpeed, groundSpeed, airSpeed, altitude, roll, pitch, altimeter, latitude, longitude;
        private string location, status, err;
        private double maxLatitude = 85, minLatitude = -85, maxLongitude = 180, minLongitude = -180, defaultLatitudePositive = 84, defaultLongitudePositive = 179,
            defaultLatitudeNegative = -84, defaultLongitudeNegative = -179, rudder, elevator, throttle, aileron;

		public FlightSimulatorModel(ITelnetClient telnetC)
        {
            InitializeModel();
            telnetClient = telnetC;
            status = "Disconnected";
            InitializeDashboard();
        }

        // Connects to the server.
		public async void Connect(string ip, string portString)
        {
            // Check if the inserted port is valid.
            if (!int.TryParse(portString, out port))
            {
                Err = "The inserted port is not an integer";
                return;
            }
            // Using the telnet to connect to the server using an async task.
			await Task.Run(() => telnetClient.Connect(ip, port));
            stop = false;
            Status = "Connected";
            // Start sending "get" commands to the sever.
			StartReading();
		}

        // Disconnects from the server.
		public void Disconnect()
		{
			Status = "Disconnected";
			stop = true;
			telnetClient.Disconnect();
            InitializeDashboard();
        }

        // Sends "get" commands to the server using a thread.
		public void StartReading()
		{
			new Thread(delegate ()
			{
				while (!stop)
                {
					// Reading Dashboard elements from the simulator.
                    try
                    {
                        AirSpeed = telnetClient.Read(airSpeedAddress);
                        Altitude = telnetClient.Read(altitudeAddress);
                        Roll = telnetClient.Read(rollAddress);
                        Pitch = telnetClient.Read(pitchAddress);
                        Altimeter = telnetClient.Read(altimeterAddress);
						Heading = telnetClient.Read(headingAddress);
                        GroundSpeed = telnetClient.Read(groundSpeedAddress);
                        VerticalSpeed = telnetClient.Read(verticalSpeedAddress);
                        // Reading map values from the simulator.
                        Latitude = telnetClient.Read(latitudeAddress);
                        Longitude = telnetClient.Read(longitudeAddress);
                        Location = latitude + "," + longitude;
                        Thread.Sleep(250);
                    }
                    catch (ArgumentNullException)
                    {
                        // Recognizes when the server has been shut down.
                        Disconnect();
                        if (!telnetClient.GetTelnetErrorFlag())
                        {
                            Err = "Server ended communication";
                        }
                    }
                }
            }).Start();
		}

        // Checking if the value we got from the server is valid.
        public bool IsFormatValid(string valueRead)
        {
            try
            {
                Double.Parse(valueRead);
                return true;
            }
            catch (OverflowException)
            {
                Err = "Overflow: the value sent by the server is too large";
                //telnetClient.ReadTrash();
                return false;
            }
            catch (FormatException)
            {
                // Recognizes that a timeout has occured (more than 10 seconds passed without an answer from the server).
                if (valueRead.Equals("timeout"))
                {
                    Err = "Server is not responding...";
                }
                else
                {
                    Err = "Format exception detected from server - notice value isn't updating";
                }
                return false;
            }
        }

        // Initializes the dashboard when we start the program or disconnect from the server.
        public void InitializeDashboard()
        {
            // Initialize all the properties to 0.s.
            AirSpeed = "0";
            Altitude = "0";
            Roll = "0";
            Pitch = "0";
            Altimeter = "0";
            Heading = "0";
            GroundSpeed = "0";
            VerticalSpeed = "0";
            // Initialize the location to Ben-Gurion Airport.
            Latitude = "32.005232";
            Longitude = "34.886709";
            Location = latitude + "," + longitude;
		}

        // Sends "set" commands to the server.
		public async Task StartWriting(string command)
        {
            await Task.Run(() => telnetClient.Write(command)); 
        }

		public string Heading
		{
			get { return heading; }
			set
			{
                if (IsFormatValid(value))
                {
                    heading = value;
                    NotifyPropertyChanged("Heading");
				}
            }
		}

		public string VerticalSpeed
		{
			get { return verticalSpeed; }
			set
			{
                if (IsFormatValid(value))
                {
                    verticalSpeed = value;
                    NotifyPropertyChanged("VerticalSpeed");
				}
            }
		}

		public string GroundSpeed
		{
			get { return groundSpeed; }
			set
			{
                if (IsFormatValid(value))
                {
                    groundSpeed = value;
                    NotifyPropertyChanged("GroundSpeed");
				}
            }
		}

		public string AirSpeed
		{
			get { return airSpeed; }
			set
			{
                if (IsFormatValid(value))
                {
                    airSpeed = value;
                    NotifyPropertyChanged("AirSpeed");
				}
            }
		}

		public string Altitude
		{
            get { return altitude; }
			set
			{
                if (IsFormatValid(value))
                {
                    altitude = value;
                    NotifyPropertyChanged("Altitude");
				}
            }
		}

		public string Roll
		{
			get { return roll; }
			set
			{
                if (IsFormatValid(value))
                {
                    roll = value;
                    NotifyPropertyChanged("Roll");
				}
            }
		}

        public string Pitch
        {
            get { return pitch; }
            set
            {
                if (IsFormatValid(value))
                {
                    pitch = value;
                    NotifyPropertyChanged("Pitch");
                }
            }
        }

        public string Altimeter
		{
			get { return altimeter; }
			set
			{
                if (IsFormatValid(value))
                {
                    altimeter = value;
                    NotifyPropertyChanged("Altimeter");
                }
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

		public string Latitude
        {
            get { return latitude; }
            set
            {
                if (IsFormatValid(value))
                {
                    latitude = value;
                    if (Double.Parse(value) > maxLatitude)
                    {
                        Err = "Latitude is too high! Invalid location";
                        latitude = defaultLatitudePositive.ToString("F");
                    }
                    else if (Double.Parse(value) < minLatitude)
                    {
                        Err = "Latitude is too low Invalid location";
                        latitude = defaultLatitudeNegative.ToString("F");
                    }
                    NotifyPropertyChanged("Latitude");
                }
            }
        }

		public string Longitude
        {
            get { return longitude; }
            set
            {
                if (IsFormatValid(value))
                {
                    longitude = value;
                    if (Double.Parse(value) > maxLongitude)
                    {
                        Err = "Longitude is too high! Invalid location";
                        longitude = defaultLongitudePositive.ToString("F");
                    }
                    else if (Double.Parse(value) < minLongitude)
                    {
                        Err = "Longitude is too low! Invalid location";
                        longitude = defaultLongitudeNegative.ToString("F");
                    }
                    NotifyPropertyChanged("Longitude");
                }
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
            }
		}

		public double Elevator
		{
			get { return elevator; }
			set
			{
                elevator = value;
            }
		}

		public double Throttle
		{
			get { return throttle; }
			set
			{
                throttle = value;
            }
		}

		public double Aileron
		{
			get { return aileron; }
			set
			{
                aileron = value;
            }
		}

        public string Err
        {
            get { return err; }
            set
            {
                // If err is null it means its the first error.
                if(err == null)
                {
                    err = value;
                    NotifyPropertyChanged("Err");
                    NotifyPropertyChanged("isErrorWindowEmpty");
                }
                // Checks if the error is already showing (so we will not repeat it).
                else if(!CheckAppearance(value))
                {
                    // If it isn't the first error and we want to print "\n" between the old error and the new one.
                    err += "\n" + value;
                    NotifyPropertyChanged("Err");
                    NotifyPropertyChanged("isErrorWindowEmpty");
                }
            }
        }

        // Cleans the error window from errors.
        public void ClearError()
        {
            err = null;
            NotifyPropertyChanged("isErrorWindowEmpty");
        }

        // Checks if the error appears in our error log.
        public bool CheckAppearance(string val)
        {
            List<string> list = TrimStringToLines(Err);
            foreach (string line in list)
            {
                if (line.Equals(val))
                {
                    return true;
                }
            }
            return false;
        }

        // Separates a string to a list of the string's lines and returns the list.
        public static List<string> TrimStringToLines(string str)
        {
            List<string> list = new List<string>(
                str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
            return list;
        }

        private void InitializeModel()
        {
            airSpeedAddress = "/instrumentation/airspeed-indicator/indicated-speed-kt";
            altitudeAddress = "/instrumentation/gps/indicated-altitude-ft";
            rollAddress = "/instrumentation/attitude-indicator/internal-roll-deg";
            pitchAddress = "/instrumentation/attitude-indicator/internal-pitch-deg";
            altimeterAddress = "/instrumentation/altimeter/indicated-altitude-ft";
            headingAddress = "/instrumentation/heading-indicator/indicated-heading-deg";
            groundSpeedAddress = "/instrumentation/gps/indicated-ground-speed-kt";
            verticalSpeedAddress = "/instrumentation/gps/indicated-vertical-speed";
            // Reading map values from the simulator.
            latitudeAddress = "/position/latitude-deg";
            longitudeAddress = "/position/longitude-deg";
		}
    }
}
