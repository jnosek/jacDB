using System;

namespace jacDB.Core.Statements
{
    class SelectStatement : Statement
    {
        internal const string Name = "select";

        public override void Execute()
        {
            Console.WriteLine("This is a select statement");
        }
    }
}
