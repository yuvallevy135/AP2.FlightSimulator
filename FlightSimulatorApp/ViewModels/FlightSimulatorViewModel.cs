using System;
using System.ComponentModel;
using FlightSimulatorApp.Models;

namespace FlightSimulatorApp.ViewModels
{
    public class FlightSimulatorViewModel : BaseNotify
    {
        private FlightSimulatorModel flightSimulatorModel;
        private string ip, port;
        #pragma warning disable 414
        private bool errorWindowEmptyFlag;
        #pragma warning restore 414

        public FlightSimulatorViewModel(FlightSimulatorModel model)
        {
            errorWindowEmptyFlag = true;
            flightSimulatorModel = model;
            flightSimulatorModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public string VM_Ip { 
            set 
            {
                ip = value;
            }
        }
        public string VM_Port
        {
            set
            {
                port = value.ToString();
            }
        }

        public string VM_Status
        {
            // Get the connection status.
            get
            {          
                return flightSimulatorModel.Status;
            }
        }

        public string VM_Err
        {
            set
            {
                // Clearing the error window.
                flightSimulatorModel.ClearError();
                errorWindowEmptyFlag = true;
            }
            get
            {
                errorWindowEmptyFlag = false;
                return flightSimulatorModel.Err;              
            }
        }

        public bool VM_isErrorWindowEmpty
        {
            get
            {
                if(flightSimulatorModel.Err != null)
                {
                    return false;
                }
                return true;
            }
        }

        public void VM_Connect()
        {
            flightSimulatorModel.Connect(ip, port);
        }

        public void VM_Disconnect()
        {
            flightSimulatorModel.Disconnect();
        }
    }
}
