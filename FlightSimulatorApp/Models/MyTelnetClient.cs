using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulatorApp.Models
{
	class MyTelnetClient : ITelnetClient
	{
		private TcpClient client;
		private NetworkStream stream;
		private bool stillConnect = false;
       
		public void Connect(string ip, int port)
		{
            client = new TcpClient();
            client.Connect("127.0.0.1", 5402);

			//IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
			//this.client = new TcpClient();
			//while (!client.Connected)
			//{
			//	try { client.Connect(endPoint); }
			//	catch (Exception) { }
			//}
			//stillConnect = true;
			//this.stream = client.GetStream();

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
            return readData("get " + command + "\n");
        }


        public string readData(string command)
        {
            byte[] read = Encoding.ASCII.GetBytes(command);
            client.GetStream().Write(read, 0, read.Length);
            byte[] buffer = new byte[64];
            client.GetStream().Read(buffer, 0, 64);
            string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            Console.WriteLine(data);
            return data;
        }

		public void Write(string str)
		{
			
		}
	}
}
