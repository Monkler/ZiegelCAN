namespace ZiegelCAN
{
    using System.IO.Ports;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System;

    public class ZiegelCANDriver : Driver
    {
        private SerialPort serial;

        public ZiegelCANDriver()
            : base("ZiegelCAN")
        {
            foreach (string port in SerialPort.GetPortNames())
            {
                this.Channels.Add(new Channel(port));
            }

            if (this.Channels.Count != 0)
            {
                this.SelectedChannel = this.Channels[0];
            }
        }

        public override void Connect()
        {
            try
            {
                serial = new SerialPort();
                serial.PortName = "COM5";//Set your board COM
                serial.BaudRate = 9600;
                serial.Open();
                while (true)
                {
                    string a = serial.ReadExisting();
                    Console.WriteLine(a);
                    //Thread.Sleep(200);
                }
            }
            catch(Exception ex)
            {
                this.OnErrorOccured(ex);
            }
        }

        private void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.OnErrorOccured(ex);
            }
        }

        private void OnSerialErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.OnErrorOccured(new Exception("Serial Error: " + e.EventType));
        }

        public override void Disconnect()
        {
            try
            {
                if (serial != null)
                    serial.Close();
            }
            catch (Exception ex)
            {
                this.OnErrorOccured(ex);
            }
        }
    }
}
