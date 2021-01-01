using System.Collections.Generic;

namespace Rollback.Accounts
{
    public class AccountsSystem
    {
        private Dictionary<int, Account> _accounts = new Dictionary<int, Account>();
        private Dictionary<int, Account> _deletedAccounts = new Dictionary<int, Account>();

        public Account AddAccountOrDefault(float startBalance)
        {
            var account = new Account(startBalance);
            var hashCode = account.GetHashCode();

            return _accounts.TryAdd(hashCode, account) ? account : null;
        }

        public bool RestoreAccount(Account account)
        {
            if (_deletedAccounts.TryGetValue(account.GetHashCode(), out account) == false)
                return false;

            return _accounts.TryAdd(account.GetHashCode(), account);
        }

        public bool RestoreAccount(int accountId)
        {
            if (_accounts.TryGetValue(accountId, out var account))
                return false;

            if (_deletedAccounts.TryGetValue(accountId, out var deletedAccount))
            {
                _accounts.TryAdd(accountId, deletedAccount);
                _deletedAccounts.Remove(accountId);
            }

            return true;
        }

        public bool CloseAccount(int accountId)
        {
            if (_accounts.TryGetValue(accountId, out var account))
                if (_accounts.Remove(accountId))
                {
                    _deletedAccounts.TryAdd(accountId, account);
                    return true;
                }

            return false;
        }

        public bool CloseAccount(Account account)
        {
            if (_accounts.Remove(account.GetHashCode()))
            {
                _deletedAccounts.TryAdd(account.GetHashCode(), account);
                return true;
            }

            return false;
        }

        public bool TryMoveBalance(int accountIdFrom, int accountIdTo, float amount)
        {
            if (_accounts.TryGetValue(accountIdFrom, out var accountFrom))
            {
                if (_accounts.TryGetValue(accountIdTo, out var accountTo))
                {
                    if (accountFrom.TrySpendBalance(amount))
                    {
                        accountTo.AddBalance(amount);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}