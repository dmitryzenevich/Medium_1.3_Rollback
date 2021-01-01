using System;
using Rollback.Accounts;

namespace Rollback.Command
{
    public class CreateAccountCommand : ICommand
    {
        private AccountsSystem _accountsSystem;
        private float _startBalance;
        private Account _account;

        public CreateAccountCommand(AccountsSystem accountsSystem) => _accountsSystem = accountsSystem;

        public void Execute()
        {
            var result = false;
            while (result == false)
            {
                Console.WriteLine("Input start balance:");
                var input = Console.ReadLine();
                if (float.TryParse(input, out var startBalance))
                {
                    result = true;
                    _startBalance = startBalance;
                }
                else
                    Console.WriteLine("Invalid input");
            }

            _account = _accountsSystem.AddAccountOrDefault(_startBalance);
            var hashCode = _account.GetHashCode();
            Console.WriteLine($"Created new account with ID - {hashCode}");
        }


        public void Undo() => _accountsSystem.CloseAccount(_account);
    }
}