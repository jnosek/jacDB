namespace jacDB.Core
{
    public interface IStatement
    {
        /// <summary>
        /// Executes the statement
        /// </summary>
        /// <returns>result output</returns>
        string Execute();
    }
}
