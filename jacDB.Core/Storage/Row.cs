using System;
using System.Text;

namespace jacDB.Core.Storage
{
    internal class Row
    {
        const int Id_Size = sizeof(int);
        const int Id_Offset = 0;

        const int Username_Size = 32;
        const int Username_Offset = Id_Offset + Id_Size;

        const int Email_Size = 255;
        const int Email_Offset = Username_Offset + Username_Size;

        public const int RowSize = Id_Size + Username_Size + Email_Size;

        public uint Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public void Serialize(Span<byte> destination)
        {
            BitConverter.GetBytes(Id).AsSpan()
                .CopyTo(destination.Slice(Id_Offset, Id_Size));

            Encoding.ASCII.GetBytes(Username).AsSpan()
                .CopyTo(destination.Slice(Username_Offset, Username_Size));

            Encoding.ASCII.GetBytes(Email).AsSpan()
                .CopyTo(destination.Slice(Email_Offset, Email_Size));
        }

        public void Deserialize(Span<byte> source)
        {
            var sourceArray = source.ToArray();

            Id = BitConverter.ToUInt32(sourceArray, Id_Offset);
            
            Username = Encoding.ASCII.GetString(sourceArray, Username_Offset, 
                FindStringLength(sourceArray, Username_Offset, Username_Size));

            Email = Encoding.ASCII.GetString(sourceArray, Email_Offset,
                FindStringLength(sourceArray, Email_Offset, Email_Size));
        }

        /// <summary>
        /// finds the length of an ASCII string in a byte array by find the first null character terminator
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private int FindStringLength(byte[] array, int offset, int size)
        {
            var endIndex = Array.IndexOf<byte>(array, 0x0, offset, size);

            if (endIndex <= -1)
                return size;
            else
                return endIndex - offset;
        }
    }
}
