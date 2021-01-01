namespace Rollback.Command
{
    public class UndoCommand : ICommand
    {
        private CommandCenter _commandCenter = null;

        public UndoCommand(CommandCenter commandCenter) => _commandCenter = commandCenter;

        public void Execute() => _commandCenter.UndoLastCommand();

        public void Undo() { }
    }
}