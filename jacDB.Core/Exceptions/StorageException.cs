using System;

namespace jacDB.Core.Exceptions
{
    public class StorageException : Exception
    {
        public StorageError Code { get; internal set; }

        public StorageException()
        {
        }

        public StorageException(Exception innerException)
            :base(null, innerException)
        {
        }
    }
}
