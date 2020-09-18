namespace ZiegelCAN
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PeakDriver : Driver
    {
        public PeakDriver()
            : base("PEAK PCAN")
        {
            
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
