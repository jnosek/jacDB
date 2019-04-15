using System;

namespace jacDB.Core.Statements
{
    class InsertStatement : Statement
    {
        internal const string Name = "insert";

        public override void Execute()
        {
            Console.WriteLine("This is an insert statement");
        }
    }
}
