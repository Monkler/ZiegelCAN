namespace ZiegelCAN
{
    public class Channel
    {
        private string name;

        public string Name { get => name; set => name = value; }

        public Channel(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
