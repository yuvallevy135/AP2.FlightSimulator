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
			IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
			this.client = new TcpClient();
			while (!client.Connected)
			{
				try { client.Connect(endPoint); }
				catch (Exception) { }
			}
			stillConnect = true;
			this.stream = client.GetStream();
			
		}

		public void Disconnect()
		{
			if (stillConnect)
			{
				stillConnect = false;
				client.Close();
			}
		}

		public string Read()
		{
			throw new NotImplementedException();
		}

		public void Write(string str)
		{
			throw new NotImplementedException();
		}
	}
}
