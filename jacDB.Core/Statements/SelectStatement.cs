using jacDB.Core.Storage;
using System.Text;

namespace jacDB.Core.Statements
{
    class SelectStatement : StatementBase
    {
        internal const string Name = "SELECT";

        public override string Execute(Context context)
        {
            var table = context.Table;

            var output = new StringBuilder();

            var cursor = table.Start;

            while(!cursor.IsAtEnd)
            {
                var row = new Row();
                row.Deserialize(table.GetSlot(cursor));

                output.AppendLine($"({row.Id}, {row.Username}, {row.Email})");

                cursor.Advance();
            }

            return output.ToString();
        }
    }
}
