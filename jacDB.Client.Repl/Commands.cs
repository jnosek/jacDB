using System;
using System.Collections.Generic;
using System.Text;

namespace jacDB.Client.Repl
{
    static class Commands
    {
        internal const string Exit = ".exit";

        internal static bool IsValid(string command)
        {
            return string.Equals(command, Exit, StringComparison.Ordinal);
        }
    }
}
