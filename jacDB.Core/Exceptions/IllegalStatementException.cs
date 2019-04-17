using System;

namespace jacDB.Core.Exceptions
{
    public class IllegalStatementException : Exception
    {
        public string Statement { get; internal set; }
        public SyntaxError SyntaxError { get; internal set; }
    }
}
