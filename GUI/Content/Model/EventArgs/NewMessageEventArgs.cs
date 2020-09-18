namespace ZiegelCAN
{
    using System;

    public class NewMessageEventArgs : EventArgs
    {
        private Message message;

        public Message Message { get => message; set => message = value; }

        public NewMessageEventArgs(Message message)
        {
            this.message = message;
        }
    }
}
