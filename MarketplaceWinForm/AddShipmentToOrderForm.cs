using MarketplaceDb;
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
    public partial class AddShipmentToOrderForm : Form
    {
        public AddShipmentToOrderForm()
        {
            InitializeComponent();
            DataTable dt = Db.GetAllChannel();
            foreach (DataRow dr in dt.Rows)
            {
                this._ChannelComboBox.Items.Add(dr["Channel"].ToString());
            }
            DataTable dt2 = Db.GetAllAccount();
            foreach (DataRow dr in dt2.Rows)
            {
                this._AccountCombox.Items.Add(dr["AccountName"].ToString());
            }
        }

        private void _SaveBtn_Click(object sender, EventArgs e)
        {
            string orderNum = this._OrderNumTxt.Text.Trim();
            string trackingNum = this._TrackingNumTxt.Text.Trim();
            string carrier = this._CarrierTxt.Text.Trim();
            string channel = this._ChannelComboBox.Text.Trim();
            string account = this._AccountCombox.Text.Trim();
            string combineToOrderNum = this._combineToOrderNum.Text.Trim();
            if (orderNum==""||trackingNum==""||carrier=="")
            {
                MessageBox.Show("Please fill up all fields!");
            }
            else
            {
                DataRow orderHeaderDr = Db.GetOrderHeaderDrByOrderNum(orderNum, channel);
                if (orderHeaderDr["ShippdedDate"].ToString() != "1753-01-01 00:00:00.000")
                {
                    DialogResult dialog = MessageBox.Show("This order has been shipped already, Are you sure want to save tracking again?","Warning", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        //Db.SaveSingleShipmentInfo(orderNum, account, channel, trackingNum, carrier, combineToOrderNum);
                    }
                    else if (dialog == DialogResult.No)
                    {

                    }
                }
                else
                {
                   // Db.SaveSingleShipmentInfo(orderNum, account, channel, trackingNum, carrier, combineToOrderNum);
                }
            }
        }
    }
}
