using jacDB.Core.Exceptions;
using jacDB.Core.Statements;
using System;

namespace jacDB.Core
{
    public abstract class Statement
    {
        public static Statement Prepare(string input)
        {
            if (input.StartsWith(InsertStatement.Name, StringComparison.OrdinalIgnoreCase))
                return new InsertStatement();

            if (input.StartsWith(SelectStatement.Name, StringComparison.OrdinalIgnoreCase))
                return new SelectStatement();

            throw new UnrecognizedStatementException
            {
                Statement = input
            };
        }

        public abstract void Execute();
    }
}
