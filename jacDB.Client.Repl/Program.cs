using jacDB.Core;
using jacDB.Core.Exceptions;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly:InternalsVisibleTo("jacDB.Client.Repl.Tests")]
#endif

namespace jacDB.Client.Repl
{ 
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("jacDB - v" + GetAssemblyVersion());

            var service = new ReplService(Console.OpenStandardInput(), Console.OpenStandardOutput());
            service.RunLoop();
        }

        static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
