namespace ZiegelCAN
{
    using System.IO.Ports;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System;

    public class ZiegelCANDriver : Driver
    {
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
            this.OnErrorOccured(new NotImplementedException());
        }

        public override void Disconnect()
        {
            
        }
    }
}
