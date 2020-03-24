using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
	
	interface ITelnetClient
	{
		void Connect(string ip, int port);
		void Disconnect();
		void Write(string str);
		string Read(string command);
	}
}
