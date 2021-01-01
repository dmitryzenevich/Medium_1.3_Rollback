using System;
using Rollback.Accounts;
using Rollback.Command;

namespace Rollback
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandCenter = new CommandCenter();
            var accountsSystem = new AccountsSystem();

            commandCenter.ExecuteCommand(new HelpCommand());

            while (true)
            {
                Console.WriteLine("Input command:");
                var input = Console.ReadLine();
                
                if (Enum.TryParse(input, out CommandType commandType))
                {
                    switch (commandType)
                    {
                        case CommandType.create_account:
                            commandCenter.ExecuteCommand(new CreateAccountCommand(accountsSystem));
                            break;
                        case CommandType.close_account:
                            commandCenter.ExecuteCommand(new CloseAccountCommand(accountsSystem));
                            break;
                        case CommandType.move_balance:
                            commandCenter.ExecuteCommand(new MoveBalanceCommand(accountsSystem));
                            break;
                        case CommandType.undo:
                            new UndoCommand(commandCenter).Execute();
                            break;
                        case CommandType.help:
                            new HelpCommand().Execute();
                            break;
                        case CommandType.clear:
                            new ClearCommand().Execute();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                    Console.WriteLine($"\"{input}\" - there is no command");
            }
        }
    }
}