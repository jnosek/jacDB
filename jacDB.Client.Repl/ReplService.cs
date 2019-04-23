using jacDB.Core;
using jacDB.Core.Exceptions;
using System.IO;

namespace jacDB.Client.Repl
{
    internal class ReplService
    {
        const string Prompt = "jacDB> ";
        const string Executed = "Executed.";

        private readonly StreamReader input;
        private readonly StreamWriter output;

        public ReplService(Stream input, Stream output)
        {
            this.input = new StreamReader(input);
            
            this.output = new StreamWriter(output);
            this.output.AutoFlush = true;
        }

        public void RunLoop()
        {
            Context.Intialize(new Context());

            string command = string.Empty;

            while (command != MetaCommand.Exit)
            {
                output.Write(Prompt);

                command = input.ReadLine();

                // handle meta commands
                if (MetaCommand.IsMetaCommand(command))
                {
                    if (!MetaCommand.IsValid(command))
                    {
                        output.WriteLine(UIResources.UnrecognizedCommand, command);
                    }
                }
                // handle statements
                else
                {
                    try
                    {
                        var statement = Statement.Prepare(command);
                        var result = statement.Execute();

                        output.Write(result);
                        output.WriteLine(Executed);
                    }
                    catch(TableException e)
                    {
                        output.Write("Error: ");

                        string message = string.Empty;

                        switch(e.ErrorCode)
                        {
                            case TableException.Error.TableFull:
                                message = "Table Full.";
                                break;
                            default:
                                message = "Unknow Table Error.";
                                break;
                        }

                        output.WriteLine(message);
                    }
                    catch (UnrecognizedStatementException e)
                    {
                        output.WriteLine(UIResources.UnrecognizedKeywordAtStart, command);
                    }
                }
            }
        }
    }
}
