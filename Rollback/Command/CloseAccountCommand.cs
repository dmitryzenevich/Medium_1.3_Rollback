using System;
using Rollback.Accounts;

namespace Rollback.Command
{
    public class CloseAccountCommand : ICommand
    {
        private AccountsSystem _accountsSystem;
        private int _accountId;

        public CloseAccountCommand(AccountsSystem accountsSystem)
        {
            _accountsSystem = accountsSystem;
        }

        public void Execute()
        {
            var result = false;
            while (result == false)
            {
                Console.WriteLine("Input account Id:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var accountId))
                {
                    result = true;
                    _accountId = accountId;
                }
                else
                    Console.WriteLine("Invalid input");
            }

            if (_accountsSystem.CloseAccount(_accountId))
                Console.WriteLine($"Closed account with ID - {_accountId.ToString()}");
        }

        public void Undo()
        {
            _accountsSystem.RestoreAccount(_accountId);
        }
    }
}