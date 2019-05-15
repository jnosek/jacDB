using System;

namespace jacDB.Client.Repl
{
    static class MetaCommand
    {
        private const char PrefixSymbol = '.';

        internal const string Exit = ".exit";

        internal static bool IsMetaCommand(string command)
        {
            return command != null && command.Length > 0 && command[0] == PrefixSymbol;
        }
    }
}
