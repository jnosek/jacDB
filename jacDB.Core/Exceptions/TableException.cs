using System;
using System.Collections.Generic;
using System.Text;

namespace jacDB.Core.Exceptions
{
    public class TableException : Exception
    {
        public Error ErrorCode { get; internal set; }

        public enum Error
        {
            TableFull = 1
        }
    }
}
