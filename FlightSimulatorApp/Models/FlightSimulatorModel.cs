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
        private string heading, verticalSpeed, groundSpeed, airSpeed, altitude, roll, pitch, altimeter, latitude, longitude;
        private string location, status, err;
        private double maxLatitude = 85, minLatitude = -85, maxLongitude = 180, minLongitude = -180, defaultLatitudePositive = 84, defaultLongitudePositive = 179,
            defaultLatitudeNegative = -84, defaultLongitudeNegative = -179, rudder, elevator, throttle, aileron;

        private string headingRead, verticalSpeedRead, groundSpeedRead,
            airSpeedRead, altitudeRead, rollRead, pitchRead, altimeterRead, latitudeRead, longitudeRead;
        private bool errorFlag;
		public FlightSimulatorModel(ITelnetClient telnetC)
        {
            initializeModel();
            telnetClient = telnetC;
            status = "Disconnected";
            initializeDashboard();
        }
		public async void Connect(string ip, int port)
        { 
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
            initializeDashboard();
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
                    catch (ArgumentNullException nullException)
                    {
                        // Catching errors from the server that sent unvalid values.
                        Disconnect();
                        if (!telnetClient.getTelnetErrorFlag())
                        {
                            Err = "Server ended communication";
                        }
                        
                    }
                }
            }).Start();
		}

        public bool isFormatValid(string valueRead)
        {
            // Checking if the value we got from the serverg is valid.
            try
            {
                Double.Parse(valueRead);
                return true;
            }
            catch (OverflowException overflowException)
            {
                Console.WriteLine("Overflow: value is too large");
                Err = "Overflow: the value sended by the server is too large";
                //telnetClient.ReadTrash();
                return false;
            }
            catch (FormatException formatException)
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

        public void initializeDashboard()
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
            Latitude = "0";
            Longitude = "0";
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                if (isFormatValid(value))
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
                    SystemSounds.Hand.Play();
                }
                else
                {
                    // Else it isnt the first error and we want to print "\n" between the old error and the new one.
                    err += "\n" + value;
                    SystemSounds.Hand.Play();
                }
                NotifyPropertyChanged("Err");
                NotifyPropertyChanged("isErrorWindowEmpty");
            }
        }


        public void clearError()
        {
            err = null;
            NotifyPropertyChanged("isErrorWindowEmpty");
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
            // Reading map values from the simulator.
            latitudeAddress = "/position/latitude-deg";
            longitudeAddress = "/position/longitude-deg";
		}
    }
}
