using jacDB.Core.Exceptions;
using System;
using System.Diagnostics;
using System.IO;

namespace jacDB.Core.Storage
{
    internal class Pager : IDisposable
    {
        public const int MaxPages = 100;
        public const int PageSize = 4096;

        private FileStream fileStream;

        /// <summary>
        /// Cache of retrieve pages from file
        /// </summary>
        private byte[][] pages;

        public long FileLength => fileStream.Length;
        public int PageCount { get; private set; }

        public Pager()
        {
        }

        public void Initialize(string fileName)
        {
            Debug.Assert(!string.IsNullOrEmpty(fileName));

            fileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            PageCount = (int)(fileStream.Length / PageSize);
            
            // We might save a partial page at the end of the file
            // add one if we detect this
            if (fileStream.Length % PageSize > 0)
            {
                PageCount += 1;
            }

            pages = new byte[MaxPages][];
        }

        public byte[] GetPage(int number)
        {
            Debug.Assert(number >= 0);

            ValidatePageNumber(number);

            // if the page is not in the cache
            if(pages[number] == null)
            {
                pages[number] = new byte[PageSize];

                // if asked for a page within the file, retrieve it
                if(number <= PageCount)
                {
                    try
                    {
                        fileStream.Position = number * PageSize;

                        fileStream.Read(pages[number], 0, PageSize);
                    }
                    catch(IOException e)
                    {
                        throw new StorageException(e)
                        {
                            Code = StorageError.PageIOError
                        };
                    }
                }
            }

            return pages[number];
        }

        /// <summary>
        /// Flushes a page to disk
        /// </summary>
        /// <param name="number">The number of a page to flush</param>
        /// <param name="pageSize">
        /// The size of the page to flush, could be a partial page
        /// </param>
        public void Flush(int number, int pageSizeToFlush = PageSize)
        {
            Debug.Assert(number >= 0);

            ValidatePageNumber(number);

            // make sure we are not flushig a null page,
            // if so we are done
            if(pages[number] == null)
            {
                return;
            }

            try
            {
                fileStream.Position = number * PageSize;

                fileStream.Write(pages[number], 0, pageSizeToFlush);
                fileStream.Flush();
            }
            catch(IOException e)
            {
                throw new StorageException(e)
                {
                    Code = StorageError.PageIOError
                };
            }
        }

        private void ValidatePageNumber(int number)
        {
            if(number >= MaxPages)
            {
                throw new StorageException
                {
                    Code = StorageError.PageNumberOutOfBounds
                };
            }
        }

        public void Dispose()
        {
            fileStream.Close();
            fileStream.Dispose();

            pages = null;
        }
    }
}
