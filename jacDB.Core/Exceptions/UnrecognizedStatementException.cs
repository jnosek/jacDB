﻿using System;

namespace jacDB.Core.Exceptions
{
    public class UnrecognizedStatementException : Exception
    {
        public string Statement { get; internal set; }
    }
}
