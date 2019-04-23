using jacDB.Core.Storage;
using System.Collections.Generic;

namespace jacDB.Core
{
    internal abstract class StatementBase : IStatement
    {
        public IList<string> Arguments { get; private set; }

        public void Initialize(IList<string> arguments)
        {
            Arguments = arguments;

            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        private static Table table = new Table();
        protected internal Table GetTable()
        {
            return table;
        }

        public abstract string Execute();
    }
}
