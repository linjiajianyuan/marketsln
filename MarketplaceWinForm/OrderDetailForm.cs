using MarketplaceDb;
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
    }
}
