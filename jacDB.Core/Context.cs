using jacDB.Core.Storage;

namespace jacDB.Core
{
    public class Context
    {
        public static Context Current { get; private set; }

        public static void Intialize(Context c)
        {
            Current = c;
        }

        internal Table Table { get; } = new Table();
    }
}
