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

        protected internal Table GetTable()
        {
            return Context.Current.Table;
        }

        public abstract string Execute();
    }
}
