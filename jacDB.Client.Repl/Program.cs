using System;
using System.Reflection;

namespace jacDB.Client.Repl
{
    static class Program
    {
        static string Prompt = "jacDB> ";

        static void Main(string[] args)
        {
            Console.WriteLine("jacDB - v" + GetAssemblyVersion());

            string command = string.Empty;

            while(command != Commands.Exit)
            {
                Console.Write(Prompt);

                command = Console.ReadLine();

                if(!Commands.IsValid(command))
                {
                    Console.WriteLine(UIResources.UnrecognizedCommand, command);
                }
            }
        }

        static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
