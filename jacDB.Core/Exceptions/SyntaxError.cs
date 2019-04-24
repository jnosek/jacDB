namespace jacDB.Core.Exceptions
{
    public enum SyntaxError : uint
    {
        NoValue = 0000001,
        UnknownStatement = 0010001,
        ArgumentLength = 0020001,
        StringLength = 0030001,
        NegativeId = 00300002
    }
}
