using System;
using System.Collections.Generic;
using System.Linq;

namespace Rollback.Command
{
    public class CommandCenter
    {
        private Stack<ICommand> _commandsHistory = new Stack<ICommand>();

        private static Type[] _noPushHistoryCommands = new[]
        {
            typeof(UndoCommand),
            typeof(HelpCommand)
        };

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            
            if (_noPushHistoryCommands.Contains(command.GetType()) == false)
                _commandsHistory.Push(command);
        }
        
        public void UndoLastCommand()
        {
            if (_commandsHistory.TryPop(out var command))
                command.Undo();
        }
    }
}