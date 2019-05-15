using jacDB.Core.Storage;
using System;

namespace jacDB.Core
{
    public class Context
    {
        public static Context Current { get; private set; }

        public static void Intialize(Context context)
        {
            Current = context ?? throw new ArgumentNullException(nameof(context));
        }

        internal Table Table { get; } = new Table();
    }
}
