﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace FlightSimulatorApp.Models
{
	public class MyTelnetClient : ITelnetClient
	{
        private TcpClient client;
        //private NetworkStream stream;
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
            // Sets the receive time out using the ReceiveTimeout public property.
            stillConnect = true;
            try
            {
                client.Connect(ip, port);
                telnetErrorFlag = false;
            }
            catch(Exception)
            {
                telnetErrorFlag = true;
                (Application.Current as App).model.Err = "Couldn't connect to server";
                Console.WriteLine("Couldn't connect to server");
            }
        }

		public void Disconnect()
		{
            // Disconecting from the server.
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
                    mutex.WaitOne();
                    client.ReceiveTimeout = timeout;
                    //// Gets the receive time out using the ReceiveTimeout public property.
                    //if (client.ReceiveTimeout == 2000)
                    //    Console.WriteLine("The receive time out limit was successfully set " + client.ReceiveTimeout.ToString());
                    byte[] read = Encoding.ASCII.GetBytes(command);
                    client.GetStream().Write(read, 0, read.Length);
                    byte[] buffer = new byte[1024];
                    StringBuilder data = new StringBuilder();
                    do
                    {
                        client.GetStream().Read(buffer, 0, buffer.Length);
                        //data.AppendFormat("{0", Encoding.ASCII.GetString(buffer, 0, buffer.Length));
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

                    //string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                    mutex.ReleaseMutex();
                    return data.ToString();
                }
                catch (InvalidOperationException)
                {
                    mutex.ReleaseMutex();
                    Disconnect();
                    //(Application.Current as App).model.Error = "Server ended communication";
                    return null;
                }
                catch (IOException)
                {
                    mutex.ReleaseMutex();
                    return "timeout";
                }
            }
            return null;
        }

        public void Write(string command)
        {
            // Sending the command we want and send it to the server.
            WriteCommand(command + "\n");
        }

		public void WriteCommand(string command)
        {
            if (stillConnect)
            {
                try
                {
                    mutex.WaitOne();
                    byte[] read = Encoding.ASCII.GetBytes(command);
                    client.GetStream().Write(read, 0, read.Length);
                    byte[] buffer = new byte[1024];
                    client.GetStream().Read(buffer, 0, 1024);
                    string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);

                    mutex.ReleaseMutex();
                    Console.WriteLine(data);
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
            }
        }

        public bool GetTelnetErrorFlag()
        {
            return telnetErrorFlag;
        }
    }
}
