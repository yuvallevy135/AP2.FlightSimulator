using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
	class MyTelnetClient : ITelnetClient
	{
		public void Connect(string ip, int port)
		{
			throw new NotImplementedException();
		}

		public void Disconnect()
		{
			throw new NotImplementedException();
		}

		public void Read()
		{
			throw new NotImplementedException();
		}

		public void Write(string str)
		{
			throw new NotImplementedException();
		}
	}
}
