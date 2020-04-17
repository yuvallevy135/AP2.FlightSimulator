using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace FlightSimulatorApp.Models
{
	public class MyTelnetClient : ITelnetClient
	{
        private TcpClient client;
        private bool stillConnect = false;
        private bool telnetErrorFlag;
        private bool endOfStream = false;
        private int timeout = 10000;
        Mutex mutex;
        
        public MyTelnetClient()
        {
            mutex = new Mutex();         
        }

        public void Connect(string ip, int port)
		{
            client = new TcpClient();
            // Initialize stillConnect to true so the the methods will know we are connected.
            stillConnect = true;
            // Trying to connect to the server.
            try
            {
                client.Connect(ip, port);
                telnetErrorFlag = false;
            }
            catch(Exception)
            {
                telnetErrorFlag = true;
                (Application.Current as App).model.Err = "Couldn't connect to server";
            }
        }

        // Disconnecting from the server.
        public void Disconnect()
		{
            if (stillConnect)
			{
				stillConnect = false;
				client.Close();
			}
		}

        // Sends the "get" command to the ReadData method
		public string Read(string command)
        {
            return ReadData("get " + command + "\n");
        }

        // Sends the "get" command to the server and updates the value.
		public string ReadData(string command)
        {
            if (stillConnect)
            {
                try
                {
                    // Lock access to the server.
                    mutex.WaitOne();
                    // Set timeout.
                    client.ReceiveTimeout = timeout;
                    byte[] read = Encoding.ASCII.GetBytes(command);
                    client.GetStream().Write(read, 0, read.Length);
                    byte[] buffer = new byte[1024];
                    StringBuilder data = new StringBuilder();
                    // Read data from server until "\n" is reached.
                    do
                    {
                        client.GetStream().Read(buffer, 0, buffer.Length);
                        data.Append(Encoding.ASCII.GetString(buffer, 0, buffer.Length));
                        for (int i = 0; i < 1024; i++)
                        {
                            if (buffer[i] == 10)
                            {
                                endOfStream = true;
                                break;
                            }
                        }
                    } while (!endOfStream);

                    endOfStream = false;
                    // Release lock.
                    mutex.ReleaseMutex();
                    return data.ToString();
                }
                catch (InvalidOperationException)
                {
                    mutex.ReleaseMutex();
                    Disconnect();
                    return null;
                }
                catch (IOException)
                {
                    mutex.ReleaseMutex();
                    return "timeout";
                }
                catch (OutOfMemoryException)
                {
                    mutex.ReleaseMutex();
                    (Application.Current as App).model.Err = "The value is too big";
                }
            }
            return null;
        }

        // Sends the "set" command to the WriteCommand method.
        public void Write(string command)
        {
            WriteCommand(command + "\n");
        }

        // Sends the "set" command to the server.
        public void WriteCommand(string command)
        {
            if (stillConnect)
            {
                try
                {
                    // Lock access to the server.
                    mutex.WaitOne();
                    byte[] read = Encoding.ASCII.GetBytes(command);
                    client.GetStream().Write(read, 0, read.Length);
                    byte[] buffer = new byte[1024];
                    client.GetStream().Read(buffer, 0, 1024);
                    string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                    // Release lock.
                    mutex.ReleaseMutex();
                }
                catch (IOException)
                {
                    mutex.ReleaseMutex();
                    (Application.Current as App).model.Err = "Server is not responding...";
                }
                catch (InvalidOperationException)
                {
                    mutex.ReleaseMutex();               
                    Disconnect();
                    if(telnetErrorFlag == false)
                    {
                        (Application.Current as App).model.Err = "Server ended communication";
                    }
                    telnetErrorFlag = true;
                }
                catch (OutOfMemoryException)
                {
                    mutex.ReleaseMutex();
                    (Application.Current as App).model.Err = "The value is too big";
                }
            }
        }

        public bool GetTelnetErrorFlag()
        {
            return telnetErrorFlag;
        }
    }
}
