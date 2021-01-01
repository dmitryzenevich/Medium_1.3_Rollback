using System;
using System.Linq;

namespace Rollback.Command
{
    public class HelpCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Commands:");
            var commandNames = Enum.GetNames<CommandType>().ToList();
            commandNames.ForEach(Console.WriteLine);
        }

        public void Undo() { }
    }
}