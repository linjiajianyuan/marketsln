using PebbledonUtilityLib;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void orderAndInvoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdersForm frm = new OrdersForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void addShipmentToOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddShipmentToOrderForm frm = new AddShipmentToOrderForm();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void EndOfDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //EndOfDayForm frm = new EndOfDayForm();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void checkShipmentInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ShipmentInfoForm frm = new ShipmentInfoForm();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SKUMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SKUMapForm frm = new SKUMapForm();
           // frm.MdiParent = this;
            //frm.Show();
        }

        private void importVisionInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataTable csvTable = new DataTable();
                csvTable = CsvUtility.LoadCsvFileAsDataTable(openFileDialog1.FileName);

            }
            else
            {
                //
            }
        }
    }
}
