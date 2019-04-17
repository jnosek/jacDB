using jacDB.Core.Exceptions;
using jacDB.Core.Storage;

namespace jacDB.Core.Statements
{
    class InsertStatement : StatementBase
    {
        internal const string Name = "INSERT";

        private Row row = new Row();

        protected override void OnInitialize()
        {
            if(Arguments.Count != 3)
            {
                throw new IllegalStatementException
                {
                    SyntaxError = SyntaxError.ArgumentLength
                };
            }

            row.Id = uint.Parse(Arguments[0]);
            row.Username = Arguments[1];
            row.Email = Arguments[2];
        }

        public override void Execute()
        {
            var table = GetTable();

            if(table.RowCount > Table.MaxRows)
            {
                throw new TableException
                {
                    ErrorCode = TableException.Error.TableFull
                };
            }

            row.Serialize(table.GetNewSlot());
        }
    }
}
