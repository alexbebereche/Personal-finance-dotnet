using personal_finance.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace personal_finance
{
    public partial class AddForm : Form
    {
        public AccountItem item;

        public AddForm(AccountItem item)
        {
            InitializeComponent();

            this.item = item;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (!ValidateChildren())
            {
                //MessageBox.Show("The form has errors",
                //    "Error",
                //MessageBoxButtons.OK,
                //MessageBoxIcon.Error);

                MessageBox.Show("The form has errors");
                return;
             
            }
            else
            {

                string name = tbName.Text.Trim();
                float price = float.Parse(tbPrice.Text.Trim());
                DateTime date = dtpDate.Value;
                string description = tbDescription.Text.Trim();

                item.Name = name;
                item.Price = price;
                item.Date = date;
                item.Description = description;

                this.Close();
                

            
            }
        }

        private void tbSum_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                errorProvider.SetError((Control)sender, "Set a name");
                e.Cancel = true;
            }
        }

        private void tbSum_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
        }

        private void tbPrice_Validating(object sender, CancelEventArgs e)
        {
            
            if(!(float.TryParse(tbPrice.Text.Trim(), out float j)) || (float.Parse(tbPrice.Text.Trim()) < 0))
            {
                errorProvider.SetError((Control)sender, "Set a positive price");
                e.Cancel = true;
            }
        }

        private void tbPrice_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
        }

        private void dtpDate_Validating(object sender, CancelEventArgs e)
        {
            if(dtpDate.Value > DateTime.Now)
            {
                errorProvider.SetError((Control)sender, "Set an earlier date");
                e.Cancel = true;
            }
        }

        private void dtpDate_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
        }

        private void tbDescription_Validating(object sender, CancelEventArgs e)
        {
            if(tbDescription.Text.Length < 3 || tbDescription.Text.Length > 50)
            {
                errorProvider.SetError((Control)sender, "Set other description");
                e.Cancel = true;
            }
        }

        private void tbDescription_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            tbName.Text = item.Name;
            tbPrice.Text = item.Price.ToString();

            DateTime dtCopy = item.Date;

            if ( (dtpDate.Value != default(DateTime)) && (dtCopy != default(DateTime)) )
                dtpDate.Value = dtCopy;
            else
                dtpDate.Value = DateTime.Now;

            tbDescription.Text = item.Description;
            
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
