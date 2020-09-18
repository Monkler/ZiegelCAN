namespace ZiegelCAN
{
    using System.Collections.Generic;

    public class MessageInformation
    {
        private int id;

        private string name;
        private string description;

        private List<SignalInformation> signals;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public List<SignalInformation> Signals { get => signals; }

        public MessageInformation(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.signals = new List<SignalInformation>();
        }
    }
}
