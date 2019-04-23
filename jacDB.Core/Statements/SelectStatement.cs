using jacDB.Core.Storage;
using System.Text;

namespace jacDB.Core.Statements
{
    class SelectStatement : StatementBase
    {
        internal const string Name = "SELECT";

        public override string Execute()
        {
            var table = GetTable();

            var output = new StringBuilder();

            for(int i = 0; i < table.RowCount; i++)
            {
                var row = new Row();
                row.Deserialize(table.GetSlot(i));

                output.AppendLine($"({row.Id}, {row.Username}, {row.Email})");
            }

            return output.ToString();
        }
    }
}
