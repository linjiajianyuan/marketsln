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
            pictureBox1.Visible = true;
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataTable csvTable = new DataTable();
                    csvTable = CsvUtility.LoadCsvFileAsDataTable(openFileDialog1.FileName);
                    foreach (DataRow csvDr in csvTable.Rows)
                    {
                        string visionSku = csvDr["Vision Item #"].ToString().Trim();
                        string visionQty = csvDr["CA Inventory"].ToString().Trim();
                        string visionNjQty = csvDr["NJ Inventory"].ToString().Trim();
                        string visionTxQty = csvDr["TX Inventory"].ToString().Trim();

                        DataRow isNewVisionSkuDr = MarketplaceDb.Db.CheckNewVisionSKU(visionSku);
                        if (isNewVisionSkuDr == null || isNewVisionSkuDr["VendorSKU"].ToString() == "")
                        {
                            DataRow dr = MarketplaceDb.Db.GetMaxItemID();
                            int nextMaxItemId = ConvertUtility.ToInt(dr["maxItemID"].ToString()) + 1;
                            MarketplaceDb.Db.InsertNewVisionItem(nextMaxItemId, visionSku, visionQty,visionNjQty, visionTxQty);
                        }
                        else
                        {
                            MarketplaceDb.Db.UpdateVisionReferenceInventory(visionSku, visionQty, visionNjQty, visionTxQty);
                        }
                    }
                    MarketplaceDb.Db.FinalUpdateVisionQty();
                    MessageBox.Show("Done");
                    pictureBox1.Visible = false;
                }
                else
                {
                    pictureBox1.Visible = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
            }
           
        }

        private void monthlySalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlySalesForm frm = new MonthlySalesForm();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
