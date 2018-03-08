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
    public partial class MonthlySalesByChannelForm : Form
    {
        public MonthlySalesByChannelForm()
        {
            InitializeComponent();
        }

        private void MonthlySalesByChannelForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = MarketplaceMdl.Mdl.GetMonthlySalesByChannel();
            DataGridViewColumn column1 = dataGridView1.Columns[0];
            DataGridViewColumn column2 = dataGridView1.Columns[1];
            DataGridViewColumn column3 = dataGridView1.Columns[2];
            DataGridViewColumn column4 = dataGridView1.Columns[3];
            column1.Width = 60;
            column2.Width = 50;
            column3.Width = 80;
            column4.Width = 60;
        }
    }
}
