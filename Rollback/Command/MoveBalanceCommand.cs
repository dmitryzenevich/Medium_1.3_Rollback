using System;
using Rollback.Accounts;

namespace Rollback.Command
{
    public class MoveBalanceCommand : ICommand
    {
        private AccountsSystem _accountsSystem;
        private int _fromId;
        private int _toId;
        private float _amount;

        public MoveBalanceCommand(AccountsSystem accountsSystem) => _accountsSystem = accountsSystem;

        public void Execute()
        {
            while (TryInput("Input account Id from:", out _fromId) == false) { }

            while (TryInput("Input account Id to:", out _toId) == false) { }

            while (TryInput("Input amount:", out _amount) == false) { }

            _accountsSystem.TryMoveBalance(_fromId, _toId, _amount);
        }

        public void Undo() => _accountsSystem.TryMoveBalance(_toId, _fromId, _amount);

        private bool TryInput(string message, out int id)
        {
            id = 0;
            Console.WriteLine(message);
            var input = Console.ReadLine();
            if (int.TryParse(input, out var accountIdFrom))
                id = accountIdFrom;
            else
                Console.WriteLine("Invalid input");

            return true;
        }

        private bool TryInput(string message, out float id)
        {
            id = 0;
            Console.WriteLine(message);
            var input = Console.ReadLine();
            if (float.TryParse(input, out var accountIdFrom))
                id = accountIdFrom;
            else
                Console.WriteLine("Invalid input");

            return true;
        }
    }
}