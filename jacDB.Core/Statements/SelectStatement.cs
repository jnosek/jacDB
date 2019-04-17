using jacDB.Core.Storage;
using System;

namespace jacDB.Core.Statements
{
    class SelectStatement : StatementBase
    {
        internal const string Name = "SELECT";

        public override void Execute()
        {
            var table = GetTable();

            for(int i = 0; i < table.RowCount; i++)
            {
                var row = new Row();
                row.Deserialize(table.GetSlot(i));

                Console.WriteLine($"({row.Id}, {row.Username}, {row.Email})");
            }
        }
    }
}
