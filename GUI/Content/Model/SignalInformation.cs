using System;

namespace ZiegelCAN
{
    public class SignalInformation
    {
        private string name;

        private int startBit;
        private int length;

        private Type type;

        public string Name { get => name; set => name = value; }
        public int StartBit { get => startBit; set => startBit = value; }
        public int Length { get => length; set => length = value; }
        public Type Type { get => type; set => type = value; }

        public SignalInformation(string name, int startBit, int length, Type type)
        {
            this.name = name;
            this.startBit = startBit;
            this.length = length;
            this.type = type;
        }
    }
}