using System.Diagnostics;

namespace jacDB.Core.Storage
{
    internal class Cursor
    {
        private readonly Table table;

        public int RowNumber { get; private set; }
        public bool IsAtEnd { get; private set; }

        public Cursor(Table table, int RowNumber = 0)
        {
            Debug.Assert(table != null);
            Debug.Assert(RowNumber >= 0);

            this.table = table;
            this.RowNumber = RowNumber;

            UpdateIsAtEnd();
        }

        public void Advance()
        {
            RowNumber++;

            UpdateIsAtEnd();
        }

        private void UpdateIsAtEnd()
        {
            IsAtEnd = RowNumber >= table.RowCount;
        }
    }
}
