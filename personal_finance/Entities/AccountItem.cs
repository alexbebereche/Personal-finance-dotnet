using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personal_finance.Entities
{
    [Serializable]
    public class AccountItem:IComparable<AccountItem>
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public AccountItem(string name, float price, DateTime date, string description)
        {
            Name = name;
            Price = price;
            Date = date;
            Description = description;
        }
        
        public AccountItem()
        {
        }

        public int CompareTo(AccountItem other)
        {
            return Price.CompareTo(other.Price);
        }

        public int CompareTo(object obj)
        {
            return -Price.CompareTo((AccountItem)obj);
        }
    }
}
