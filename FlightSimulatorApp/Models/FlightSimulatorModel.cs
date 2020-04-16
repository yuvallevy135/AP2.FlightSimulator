using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Media;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Animation;
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
		public async void Connect(string ip, string portString)
        {
            try
            {
                port = int.Parse(portString);
            }
            catch (FormatException)
            {
                Err = "The inserted port is not an integer";
                return;
            }
            // Using the telnet to connect to the server.
			await Task.Run(() => telnetClient.Connect(ip, port));
            stop = false;
            Status = "Connected";
			StartReading();
		}

		public void Disconnect()
		{
			Status = "Disconnected";
			stop = true;
			telnetClient.Disconnect();
            InitializeDashboard();
        }
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
                        //reading map values from the simulator
                        Latitude = telnetClient.Read(latitudeAddress);
                        Longitude = telnetClient.Read(longitudeAddress);
                        Location = latitude + "," + longitude;
                        Thread.Sleep(250);
                    }
                    catch (ArgumentNullException)
                    {
                        // Catching errors from the server that sent unvalid values.
                        Disconnect();
                        if (!telnetClient.GetTelnetErrorFlag())
                        {
                            Err = "Server ended communication";
                        }
                        
                    }
                }
            }).Start();
		}

        public bool IsFormatValid(string valueRead)
        {
            // Checking if the value we got from the serverg is valid.
            try
            {
                Double.Parse(valueRead);
                return true;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Overflow: value is too large");
                Err = "Overflow: the value sent by the server is too large";
                //telnetClient.ReadTrash();
                return false;
            }
            catch (FormatException)
            {
                if (valueRead.Equals("timeout"))
                {
                    Err = "Server is not responding...";
                }
                else
                {
                    Console.WriteLine("format exception detected");
                    Err = "Format exception detected from server - notice value isn't updating";
                }
                return false;
            }
           

        }

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
            // Reading map values from the simulator.
            Latitude = "32.005232";
            Longitude = "34.886709";
            Location = latitude + "," + longitude;
		}

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
                    // Console.WriteLine("latitude value: " + value);
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
                    // Console.WriteLine("latitude: " + latitude);
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
                    //Console.WriteLine("longitude value: " + value);

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
                    //Console.WriteLine("longitude: " + longitude);
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
        public string Err
        {
            get { return err; }
            set
            {
                // if err is null it means its the first error.
                if(err == null)
                {
                    err = value;
                    NotifyPropertyChanged("Err");
                    NotifyPropertyChanged("isErrorWindowEmpty");
                }
                // Else it isn't the first error and we want to print "\n" between the old error and the new one.
                else if(!CheckAppearance(value))
                {
                    err += "\n" + value;
                    NotifyPropertyChanged("Err");
                    NotifyPropertyChanged("isErrorWindowEmpty");
                }
            }
        }


        public void ClearError()
        {
            err = null;
            NotifyPropertyChanged("isErrorWindowEmpty");
        }

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
