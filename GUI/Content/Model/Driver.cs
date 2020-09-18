namespace ZiegelCAN
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;

    public abstract class Driver
    {
        public event ErrorEventHandler ErrorOccured;
        public event EventHandler<NewMessageEventArgs> NewMessage;

        private string name;
        
        private Channel selectedChannel;
        private ObservableCollection<Channel> channels;

        public string Name { get => name; set => name = value; }

        public ObservableCollection<Channel> Channels { get => channels; }
        public Channel SelectedChannel { get => selectedChannel; set => selectedChannel = value; }

        public Driver(string name)
        {
            this.name = name;
            this.channels = new ObservableCollection<Channel>();
        }

        public abstract void Connect();
        public abstract void Disconnect();

        protected void OnErrorOccured(Exception exception)
        {
            this.ErrorOccured?.Invoke(this, new ErrorEventArgs(exception));
        }

        protected void OnNewMessage(Message message)
        {
            this.NewMessage?.Invoke(this, new NewMessageEventArgs(message));
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
