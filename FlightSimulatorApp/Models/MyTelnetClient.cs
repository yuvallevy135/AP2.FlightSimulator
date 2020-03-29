using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulatorApp.Models
{
	public class MyTelnetClient : ITelnetClient
	{
		private TcpClient client;
        //private NetworkStream stream;
		private bool stillConnect = false;
        
        public MyTelnetClient()
        {
            
        }
        public void Connect(string ip, int port)
		{
            client = new TcpClient();
            // Sets the receive time out using the ReceiveTimeout public property.
            stillConnect = true;
            try
            {
                client.Connect(ip, port);
            }
            catch(Exception exception)
            {
                Console.WriteLine("Couldn't connect to server");
            }
        }

		public void Disconnect()
		{
			if (stillConnect)
			{
				stillConnect = false;
				client.Close();
			}
		}

		public string Read(string command)
        {
            
            return ReadData("get " + command + "\n");
        }

		public string ReadData(string command)
        {
            if (stillConnect)
            {
                try
                {
                    client.ReceiveTimeout = 20000;
                    //// Gets the receive time out using the ReceiveTimeout public property.
                    //if (client.ReceiveTimeout == 2000)
                    //    Console.WriteLine("The receive time out limit was successfully set " + client.ReceiveTimeout.ToString());
                    byte[] read = Encoding.ASCII.GetBytes(command);
                    client.GetStream().Write(read, 0, read.Length);
                    byte[] buffer = new byte[1024];
                    client.GetStream().Read(buffer, 0, 1024);
                    string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                    //Console.WriteLine(data);
                    return data;
                }
                catch (Exception exception)
                {
                    Disconnect();
                    return null;
                }
               
            }
            else
            {
                return null;
            }
        }

        public void Write(string command)
        {
            WriteCommand(command + "\n");
        }

		public void WriteCommand(string command)
        {
            if (stillConnect)
            {
                try
                {
                    byte[] read = Encoding.ASCII.GetBytes(command);
                    client.GetStream().Write(read, 0, read.Length);
                    byte[] buffer = new byte[1024];
                    client.GetStream().Read(buffer, 0, 1024);
                    string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                    Console.WriteLine(data);
                }
                catch (Exception exception)
                {
                    Disconnect();
                }

            }
        }

	}
}
