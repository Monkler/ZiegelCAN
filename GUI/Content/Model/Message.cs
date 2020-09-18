using System;

namespace ZiegelCAN
{
    public class Message
    {
        private int id;
        private byte[] data;
        private int length;
        private TimeSpan timestamp;

        public int Id { get => id; set => id = value; }
        public byte[] Data { get => data; set => data = value; }
        public int Length { get => length; set => length = value; }

        public TimeSpan Timestamp { get => timestamp; set => timestamp = value; }

        public Message(int id, byte[] data, int length, TimeSpan timestamp)
        {
            this.id = id;
            this.data = data;
            this.length = length;
            this.timestamp = timestamp;
        }

        public override string ToString()
        {
            string res = $"0x{id:X2} | ";

            for (int i = 0; i < length; i++)
            {
                res += $"{data[i]:X2} ";
            }

            res += "| " + timestamp;

            return res;
        }
    }
}
