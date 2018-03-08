using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketplaceWinForm
{
    public partial class MonthlySalesByAccountForm : Form
    {
        public MonthlySalesByAccountForm()
        {
            InitializeComponent();
        }

        private void MonthlySalesByAccountForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = MarketplaceMdl.Mdl.GetMonthlySalesByAccount();
            DataGridViewColumn column1 = dataGridView1.Columns[0];
            DataGridViewColumn column2 = dataGridView1.Columns[1];
            DataGridViewColumn column3 = dataGridView1.Columns[2];
            DataGridViewColumn column4 = dataGridView1.Columns[3];
            DataGridViewColumn column5 = dataGridView1.Columns[4];
            column1.Width = 60;
            column2.Width = 50;
            column3.Width = 80;
            column4.Width = 60;
            column4.Width = 60;
        }
    }
}
