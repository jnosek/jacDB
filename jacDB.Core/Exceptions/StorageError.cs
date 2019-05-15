namespace jacDB.Core.Exceptions
{
    public enum StorageError : int
    {
        TableFull = 0010001,
        PageNumberOutOfBounds = 0020001,
        PageIOError = 0020002,
        FlushNullPage = 0020003
    }
}
