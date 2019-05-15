using jacDB.Core.Storage;
using System;
using System.Collections.Generic;

namespace jacDB.Core
{
    internal abstract class StatementBase : IStatement
    {
        public IList<string> Arguments { get; private set; }

        public void Initialize(IList<string> arguments)
        {
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));

            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        public abstract string Execute(Context context);
    }
}
