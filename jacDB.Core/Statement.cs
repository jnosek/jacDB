using jacDB.Core.Exceptions;
using jacDB.Core.Statements;
using System.Linq;

namespace jacDB.Core
{
    public static class Statement
    {
        public static IStatement Prepare(string input)
        {
            // ensure input has values
            if(string.IsNullOrWhiteSpace(input))
            {
                throw new IllegalStatementException
                {
                    Statement = input,
                    SyntaxError = SyntaxError.NoValue
                };
            }

            var arguments = input.Split(' ');
            var statementName = arguments[0].ToUpperInvariant();

            StatementBase statement = null;

            switch(statementName)
            {
                case InsertStatement.Name:
                    statement = new InsertStatement();
                    break;
                case SelectStatement.Name:
                    statement = new SelectStatement();
                    break;
                default:
                    throw new IllegalStatementException
                    {
                        Statement = arguments[0],
                        SyntaxError = SyntaxError.UnknownStatement
                    };
            }

            // initialize with arguments but skip the statement name
            statement.Initialize(arguments.Skip(1).ToList());

            return statement;
        }
    }
}
