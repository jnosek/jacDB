using System;

namespace jacDB.Core.Storage
{
    internal class Table
    {
        public const int PageSize = 4096;
        public const int MaxPages = 100;
        public const int RowsPerPage = PageSize / Row.RowSize;
        public const int MaxRows = RowsPerPage * MaxPages;

        public byte[][] Pages { get; private set; } = new byte[100][];
        public int RowCount { get; private set; } = 0;

        public Span<byte> GetNewSlot()
        {
            var slot = GetSlot(RowCount);

            RowCount++;

            return slot;
        }

        public Span<byte> GetSlot(int rowNumber)
        {
            int pageNumber = rowNumber / RowsPerPage;

            var page = Pages[pageNumber];

            // if page is empty, allocate it
            if (page == null)
            {
                page = Pages[pageNumber] = new byte[PageSize];
            }

            int rowOffset = rowNumber % RowsPerPage;
            int byteOffset = rowOffset * Row.RowSize;

            return page.AsSpan(byteOffset, Row.RowSize);
        }
    }
}
