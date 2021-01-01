using System;

namespace Rollback.Accounts
{
    public class Account
    {
        private float _money;

        public Account(float money)
        {
            _money = money;
        }

        public bool TrySpendBalance(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (amount > _money)
                return false;
            
            _money -= amount;
            
            return true;
        }

        public void AddBalance(float amount)
        {
            _money += amount;
        }
    }
}