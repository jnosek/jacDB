using System;
using System.Diagnostics;

namespace jacDB.Core.Storage
{
    internal class Table
    {
        private readonly Pager pager;
        
        public const int RowsPerPage = Pager.PageSize / Row.RowSize;
        public const int MaxRows = RowsPerPage * Pager.MaxPages;
        
        public string Filename { get; private set; }
        public int RowCount { get; private set; }

        public Table()
        {
            RowCount = 0;
            pager = new Pager();
        }

        public void Open(string filename)
        {
            RowCount = 0;
            Filename = filename;

            pager.Initialize(filename);
            RowCount = (int)(pager.FileLength / Row.RowSize);
        }

        public void Close()
        {
            // calulcate how many full pages there are to flush
            int fullPageCount = RowCount / RowsPerPage;

            // flush full pages
            for(int i = 0; i < fullPageCount; i++)
            {
                pager.Flush(i);
            }

            // There may be a partial page to write to the end of the file
            int additionalRowCount = RowCount % RowsPerPage;
            if(additionalRowCount > 0)
            {
                pager.Flush(fullPageCount, additionalRowCount * Row.RowSize);
            }

            pager.Dispose();
        }

        public Cursor End => new Cursor(this, RowCount);

        public Cursor Start => new Cursor(this);

        public Span<byte> GetSlot(Cursor cursor)
        {
            Debug.Assert(cursor != null);

            int pageNumber = cursor.RowNumber / RowsPerPage;

            // ask pager for page
            var page = pager.GetPage(pageNumber);

            // find row location
            int rowOffset = cursor.RowNumber % RowsPerPage;
            int byteOffset = rowOffset * Row.RowSize;

            // if we are retrieving the slot for a new row, increase row count
            if(cursor.IsAtEnd)
            {
                RowCount++;
            }

            return page.AsSpan(byteOffset, Row.RowSize);
        }
    }
}
