using System;

namespace Rollback.Command
{
    public class ClearCommand : ICommand
    {
        public void Execute() => Console.Clear();

        public void Undo() { }
    }
}