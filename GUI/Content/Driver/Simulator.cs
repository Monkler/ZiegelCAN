namespace ZiegelCAN
{
    using System;
    using System.Timers;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Simulator : Driver
    {
        private TimeSpan timestamp;

        private Timer messageTimer;

        public Simulator()
            : base("Simulator")
        {
            this.messageTimer = new Timer(20);
            this.messageTimer.Elapsed += OnMessageTimerElapsed;

            this.Channels.Add(new Channel("Channel 1"));
            this.SelectedChannel = this.Channels[0];
        }

        public override void Connect()
        {
            try
            {
                this.timestamp = new TimeSpan(0);
                this.messageTimer.Start();
            }
            catch (Exception ex)
            {
                this.OnErrorOccured(ex);
            }
        }

        public override void Disconnect()
        {
            try
            {
                this.messageTimer.Stop();
            }
            catch (Exception ex)
            {
                this.OnErrorOccured(ex);
            }
        }

        byte counter = 0;
        private void OnMessageTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                timestamp = timestamp.Add(new TimeSpan(100000));

                counter++;
                if (counter > 255)
                {
                    counter = 0;
                }

                for (int i = 0; i < 10; i ++)
                {
                    this.OnNewMessage(new Message(i, new byte[] { (byte)i, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, counter }, 8, timestamp));
                }
            }
            catch (Exception ex)
            {
                this.messageTimer.Stop();

                this.OnErrorOccured(ex);                
            }
        }
    }
}
