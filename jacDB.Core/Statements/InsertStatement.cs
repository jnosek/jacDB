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
                    Code = SyntaxError.ArgumentLength
                };
            }

            var id = long.Parse(Arguments[0]);

            if(id < 0)
            {
                throw new IllegalStatementException
                {
                    Code = SyntaxError.NegativeId
                };
            }

            row.Id = (uint)id;
            row.Username = Arguments[1];
            row.Email = Arguments[2];
        }

        public override string Execute(Context context)
        {
            var table = context.Table;

            if(table.RowCount >= Table.MaxRows)
            {
                throw new StorageException
                {
                    Code = StorageError.TableFull
                };
            }

            row.Serialize(table.GetSlot(table.End));

            return string.Empty;
        }
    }
}
