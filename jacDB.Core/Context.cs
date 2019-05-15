using jacDB.Core.Storage;
using System;

namespace jacDB.Core
{
    public class Context : IDisposable
    {
        public string Filename { get; private set; }

        public Context(string filename)
        {
            Filename = filename;
        }

        public void Open()
        {
            Table = new Table();
            Table.Open(Filename);
        }

        public void Close()
        {
            Table.Close();
            Table = null;
        }

        public void Dispose()
        {
            Close();
        }

        internal Table Table { get; private set; }
    }
}
