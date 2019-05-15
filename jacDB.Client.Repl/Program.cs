using System;
using System.Reflection;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("jacDB.Client.Repl.Tests")]
#endif

namespace jacDB.Client.Repl
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("jacDB - v" + GetAssemblyVersion());

            if(args.Length < 1 || string.IsNullOrEmpty(args[0]))
            {
                Console.Error.WriteLine("Filename required");
                return;
            }

            var service = new ReplService(args[0], Console.OpenStandardInput(), Console.OpenStandardOutput());
            service.RunLoop();
        }

        static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
