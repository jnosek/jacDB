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
        private readonly string filename;

        public ReplService(string filename, Stream input, Stream output)
        {
            this.filename = filename;

            this.input = new StreamReader(input);

            this.output = new StreamWriter(output)
            {
                AutoFlush = true
            };
        }

        public void RunLoop()
        {
            using (var context = new Context(filename))
            {
                context.Open();

                string command = string.Empty;

                while (command != MetaCommand.Exit)
                {
                    output.Write(Prompt);

                    command = input.ReadLine();

                    // handle meta commands
                    if (MetaCommand.IsMetaCommand(command))
                    {
                        switch (command)
                        {
                            case MetaCommand.Exit:
                                output.WriteLine();
                                return;
                            default:
                                output.WriteLine(UIResources.UnrecognizedCommand, command);
                                break;
                        }
                    }
                    // handle statements
                    else
                    {
                        try
                        {
                            var statement = Statement.Prepare(command);
                            var result = statement.Execute(context);

                            output.Write(result);
                            output.WriteLine(Executed);
                        }
                        catch (StorageException e)
                        {
                            output.Write("Error: ");

                            string message = string.Empty;

                            switch (e.Code)
                            {
                                case StorageError.TableFull:
                                    message = "Table Full.";
                                    break;
                                default:
                                    message = "Unknow Table Error.";
                                    break;
                            }

                            output.WriteLine(message);
                        }
                        catch (IllegalStatementException e)
                        {
                            string message = string.Empty;

                            switch (e.Code)
                            {
                                case SyntaxError.UnknownStatement:
                                    message = string.Format(UIResources.UnrecognizedKeywordAtStart, command);
                                    break;
                                case SyntaxError.StringLength:
                                    message = UIResources.StringLength;
                                    break;
                                case SyntaxError.NegativeId:
                                    message = UIResources.NegativeId;
                                    break;
                                default:
                                    message = UIResources.IllegalSyntax;
                                    break;
                            }

                            output.WriteLine(message);
                        }
                    }
                }
            }
        }
    }
}
