using MarketplaceDb;
using Microsoft.VisualBasic;
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
    public partial class OrderDetailForm : Form
    {
        public OrderDetailForm(string orderNum, string channel)
        {
            InitializeComponent();
            this._OrderNumTxt.ReadOnly = true;
            this._OrderNumTxt.Text = orderNum;
            this._ChannelTxt.Text = channel;
            DataRow orderHeaderDr = Db.GetOrderHeaderDrByOrderNum(orderNum,channel);
            if(orderHeaderDr["ShippedDate"].ToString()!= "1753-01-01 00:00:00.000")
            {
                this._ReprintBtn.Enabled = true;
            }
            else
            {
                this._ReprintBtn.Enabled = false;
            }
            DataTable orderLineDt = new DataTable();
            if(orderHeaderDr==null || orderHeaderDr["OrderNum"]==null)
            {
                MessageBox.Show("Error!! No result!!! Please contact Administrator!!!");
            }
            else
            {
                try
                {
                    orderLineDt = Db.GetOrderLineDtByOrderNum(orderNum, channel);
                    this._AccountNameTxt.Text = orderHeaderDr["AccountName"].ToString();
                    this._BuyerUserIDTxt.Text = orderHeaderDr["BuyerUserID"].ToString();
                    this._OrderDateTxt.Text = orderHeaderDr["OrderDate"].ToString();
                    this._ShipNameTxt.Text = orderHeaderDr["ShipName"].ToString();
                    this._ShipEmailTxt.Text = orderHeaderDr["ShipEmail"].ToString();
                    this._EnterDateTxt.Text = orderHeaderDr["EnterDate"].ToString();
                    this._TrackingLinkLab.Text = orderHeaderDr["TrackingNum"].ToString();
                    this._Address1Txt.Text = orderHeaderDr["ShipAddress1"].ToString();
                    this._Address2Txt.Text = orderHeaderDr["ShipAddress2"].ToString();
                    this._ShipCityTxt.Text = orderHeaderDr["ShipCity"].ToString();
                    this._ShipStateTxt.Text = orderHeaderDr["ShipState"].ToString();
                    this._ShipZipTxt.Text = orderHeaderDr["ShipZip"].ToString();
                    this._ShipCountry.Text = orderHeaderDr["ShipCountry"].ToString();
                    this._ShipPhoneTxt.Text = orderHeaderDr["ShipPhone"].ToString();
                    this._ShippedDateTxt.Text = orderHeaderDr["ShippedDate"].ToString();
                    this._ShippingCarrierTxt.Text = orderHeaderDr["ShippingCarrier"].ToString();
                    this._SubTotalTxt.Text = ConvertUtility.ToDouble(orderHeaderDr["SubTotal"]).ToString();
                    this._ShippingFeeTxt.Text = ConvertUtility.ToDouble(orderHeaderDr["ShippingServiceCost"]).ToString() + ConvertUtility.ToDouble(orderHeaderDr["ShippingServiceAdditionalCost"]).ToString();
                    this._TaxTxt.Text = ConvertUtility.ToDouble(orderHeaderDr["SalesTaxAmount"]).ToString();
                    this._OtherFeeTxt.Text = "";
                    this._NoteTxt.Text = orderHeaderDr["Note"].ToString();
                    this._TotalTxt.Text = ConvertUtility.ToDouble(orderHeaderDr["Total"]).ToString();
                    this._PaidAmountTxt.Text = ConvertUtility.ToDouble(orderHeaderDr["AmountPaid"]).ToString();
                    if(ConvertUtility.ToDouble(orderHeaderDr["AmountPaid"])!= ConvertUtility.ToDouble(orderHeaderDr["Total"]))
                    {
                        this._PaidAmountTxt.ForeColor = Color.Red;
                        this._TotalTxt.ForeColor = Color.Red;
                    }
                    this._OrderLineDgv.DataSource = orderLineDt.DefaultView;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message+ " Error!! Please contact admin for OrderDetailForm ");
                }
            }
        }
        private void OrderDetailForm_Load(object sender, EventArgs e)
        {

        }

        private void _ReprintBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Reprint existing label(Yes) or create new label(No)?","Warning", MessageBoxButtons.YesNo);
            if(dialog==DialogResult.Yes)
            {
                DataRow dr = Db.GetShipmentInfoByOrder(this._OrderNumTxt.Text, this._ChannelTxt.Text);
                string nativeCommand = dr["LabelCommand"].ToString();
                byte[] data = Convert.FromBase64String(nativeCommand);
                string encodedLabelBinary = Encoding.UTF8.GetString(data);
                RawPrinterHelper.SendStringToPrinter("Zebra ZP 500 (ZPL)", encodedLabelBinary);
            }
            else if(dialog==DialogResult.No)
            {
                if (this._ShipCountry.Text != "US")
                {
                    MessageBox.Show("Not Support Yet!");
                    //Dictionary<string, string> labelDict = ProcessShippingLabel.GetInternationalLabel(this._OrderNumTxt.Text, this._ChannelTxt.Text);
                    //string printResult = PrintShippingLabel.Print(labelDict);
                    //if (printResult == "Error, Email Sent")
                    //{
                     //   MessageBox.Show("Error, Email Sent");
                    //}
                }
                else
                {

                    string customizedWeight = (Interaction.InputBox("Input Weight", this._OrderNumTxt.Text));
                    Dictionary<string, string> labelDict = ProcessShippingLabel.GetDomesticLabel(this._OrderNumTxt.Text, this._ChannelTxt.Text,customizedWeight,"");
                    string printResult = PrintShippingLabel.Print(labelDict);
                    if (printResult == "Error, Email Sent")
                    {
                        MessageBox.Show("Error, Email Sent");
                    }
                }
            }
        }

        private void _NoteTxtDoubleClick(object sender, EventArgs e)
        {
            _NoteTxt.ReadOnly = false;
        }

        private void _NoteTxtMouseLeave(object sender, EventArgs e)
        {
            _NoteTxt.ReadOnly = true;
            Db.SaveNoteToDb(_OrderNumTxt.Text,_ChannelTxt.Text,_NoteTxt.Text);
        }
    }
}
