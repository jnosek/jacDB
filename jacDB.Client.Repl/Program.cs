using jacDB.Core;
using jacDB.Core.Exceptions;
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

            while (command != MetaCommand.Exit)
            {
                Console.Write(Prompt);

                command = Console.ReadLine();

                // handle meta commands
                if (MetaCommand.IsMetaCommand(command))
                {
                    if (!MetaCommand.IsValid(command))
                    {
                        Console.WriteLine(UIResources.UnrecognizedCommand, command);
                    }
                }
                // handle statements
                else
                {
                    try
                    {
                        var statement = Statement.Prepare(command);
                        statement.Execute();
                    }
                    catch(UnrecognizedStatementException e)
                    {
                        Console.WriteLine(UIResources.UnrecognizedKeywordAtStart, command);
                    }
                }
            }
        }

        static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
