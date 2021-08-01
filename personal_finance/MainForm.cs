using personal_finance.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace personal_finance
{
    public partial class MainForm : Form
    {
        public Account account;
        //public AccountItem items;

        public MainForm()
        {
            account = new Account();
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AccountItem item = new AccountItem(/*"asd",1,DateTime.Now, "asd"*/);

            AddForm addForm = new AddForm(item);
            
            
            if ((addForm.ShowDialog() == DialogResult.OK) && ValidateChildren())
            {
                account.Items.Add(addForm.item);
                Display();
            }

            


        }

        private float getTotal()
        {
            float sum = 0;

            foreach (AccountItem ai in account.Items)
            {
                sum += ai.Price;
            }

            return sum;
        }

        private void Display()
        {
            lvAccount.Items.Clear();
            

            foreach (AccountItem item in account.Items)
            {
                var lvItem = new ListViewItem(item.Name);

                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.Date.ToString());
                lvItem.SubItems.Add(item.Description);

                lvItem.Tag = item;

                lvAccount.Items.Add(lvItem);

               
                lbTotal.Text = getTotal().ToString();
                

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                using (FileStream stream = File.OpenRead("data.bin"))
                {
                    account.Items = (List < AccountItem >)formatter.Deserialize(stream);
                    Display();
                }
            }
            catch(Exception s)
            {
                //MessageBox.Show(s.Message);
            }


        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                using(FileStream stream = File.Create("data.bin"))
                {
                    formatter.Serialize(stream, account.Items);
                }
            }
            catch(Exception s)
            {
                MessageBox.Show(s.Message);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvAccount.SelectedItems.Count != 0)
            {
                ListViewItem lvi = lvAccount.SelectedItems[0];
                AccountItem acc = (AccountItem)lvi.Tag;


                account.Items.Remove(acc);

                Display();
            }
            else
            {
                MessageBox.Show("Choose other item");
            }

        }

        private void clrBtn_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                while(account.Items.Count != 0)
                {
                    
                    account.Items.Remove(account.Items[0]);
                    
                }

                Display();
            }

        }

        private void btnLower_Click(object sender, EventArgs e)
        {
            account.Items.Sort();
            Display();
        }

        private void btnHigher_Click(object sender, EventArgs e)
        {
            account.Items.Sort((x, y) => y.CompareTo(x));
            Display();

        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "Save as CSV File";
            dialog.Filter = "CSV File | *.csv";
            
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                using(StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    writer.WriteLine("Name , Price, Date, Description");

                    foreach(AccountItem ai in account.Items)
                    {
                        writer.WriteLine("{0}, {1}, {2}, {3}", 
                            ai.Name,
                            ai.Price,
                            ai.Date.ToShortDateString(),
                            ai.Description);
                    }
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(lvAccount.SelectedItems.Count != 1)
            {
                MessageBox.Show("Choose an item!");
            }
            else
            {
                ListViewItem lvi = lvAccount.SelectedItems[0];
                AccountItem ai = (AccountItem)lvi.Tag;

                AddForm addForm = new AddForm(ai);
                

                if(addForm.ShowDialog() == DialogResult.OK)
                {
                    Display();
                }

            }
        }

        private void lvAccount_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvAccount.SelectedItems.Count != 0)
            {
                ListViewItem lvi = lvAccount.SelectedItems[0];
                AccountItem acc = (AccountItem)lvi.Tag;


                account.Items.Remove(acc);

                Display();
            }
            else
            {
                MessageBox.Show("Choose other item");
            }
        }

        private void saveAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "Save as CSV File";
            dialog.Filter = "CSV File | *.csv";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    writer.WriteLine("Name , Price, Date, Description");

                    foreach (AccountItem ai in account.Items)
                    {
                        writer.WriteLine("{0}, {1}, {2}, {3}",
                            ai.Name,
                            ai.Price,
                            ai.Date.ToShortDateString(),
                            ai.Description);
                    }
                }
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            float sumNow = 0;
            float sumLastMonth = 0;

            foreach (AccountItem ai in account.Items)
            {

                if(ai.Date.Month == DateTime.Now.Month)
                {
                    sumNow += ai.Price;
                }
                else if (ai.Date.Month == DateTime.Now.Month - 1)
                {
                    sumLastMonth += ai.Price;
                }


            }
            

            if (sumNow > sumLastMonth)
            {
                MessageBox.Show("This month's sum: " + sumNow.ToString() + " \nLast month's sum was: " + sumLastMonth.ToString()
               + "\nSo far, you spent with " + Math.Abs(sumNow - sumLastMonth) + " more money compared to last month");
            }
            else if(sumNow < sumLastMonth)
            {
                MessageBox.Show("This month's sum: " + sumNow.ToString() + " \nLast month's sum was: " + sumLastMonth.ToString()
               + "\nSo far, you spent with " + Math.Abs(sumNow - sumLastMonth) + " less money compared to last month");
            }
            else
            {
                MessageBox.Show("This month's sum: " + sumNow.ToString() + " \nLast month's sum was: " + sumLastMonth.ToString()
              + "\nSo far, you spent the same amount of money compared to last month");
            }
            

        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            if (float.TryParse(tbBudget.Text.Trim(), out float j) && float.Parse(tbBudget.Text.Trim()) > 0)
            {
                account.CurrentSum = float.Parse(tbBudget.Text.Trim());
            }
            else
            {
                account.CurrentSum = 0;
            }

            float sum = 0;

            //calculate monthly expense
            foreach (AccountItem ai in account.Items)
            {
                if (ai.Date.Month == DateTime.Now.Month)
                    sum += ai.Price;
            }
            account.Debt = sum;

            account.Balance = account.CurrentSum - account.Debt;
           
            lbBudget.Text = account.Balance.ToString();

            

            tbBudget.Clear();
            MessageBox.Show("You can spend: " + account.Balance + " more this month so far");


        }

    }
}
