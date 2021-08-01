using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personal_finance.Entities
{
    [Serializable]
    public class Account
    {
        public float CurrentSum { get; set; }
        public float Debt { get; set; }
        public float Balance { get; set; }

        public List<AccountItem> Items { get; set; }

        public Account(List<AccountItem> items)
        {
            this.Items = new List<AccountItem>();
            this.Items = items;
        }

        public Account(float currentSum, float debt, float balance, List<AccountItem> items)
        {
            CurrentSum = currentSum;
            Debt = debt;
            Balance = balance;

            this.Items = new List<AccountItem>();
            this.Items = items;
        }

        public Account()
        {
            this.Items = new List<AccountItem>();
        }
    }
}
