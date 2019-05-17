using jacDB.Core.Exceptions;
using System;
using System.Runtime.InteropServices;
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

        const byte StringTerminator = 0x0;

        public const int RowSize = Id_Size + Username_Size + Email_Size;

        public uint Id { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value.Length > Username_Size)
                {
                    throw new IllegalStatementException
                    {
                        Code = SyntaxError.StringLength
                    };
                }

                _username = value;
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (value.Length > Email_Size)
                {
                    throw new IllegalStatementException
                    {
                        Code = SyntaxError.StringLength
                    };
                }

                _email = value;
            }
        }

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
            Id = MemoryMarshal.Read<uint>(source);

            Username = Encoding.ASCII.GetString(
                FindStringBytes(source, Username_Offset, Username_Size));

            Email = Encoding.ASCII.GetString(
                FindStringBytes(source, Email_Offset, Email_Size));
        }

        /// <summary>
        /// finds the length of an ASCII string in a byte array by find the first null character terminator
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private byte[] FindStringBytes(Span<byte> row, int offset, int size)
        {
            var rawString = row.Slice(offset, size);
            var endIndex = rawString.IndexOf(StringTerminator);

            if (endIndex <= -1)
                return rawString.ToArray();
            else
                return rawString.Slice(0, endIndex).ToArray();
        }
    }
}
