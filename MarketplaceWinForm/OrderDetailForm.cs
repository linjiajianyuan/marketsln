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
                this._SubTotalTxt.Text = orderHeaderDr["SubTotal"].ToString();
                this._ShippingFeeTxt.Text = orderHeaderDr["ShippingServiceCost"].ToString() + orderHeaderDr["ShippingServiceAdditionalCost"].ToString();
                this._TaxTxt.Text = orderHeaderDr["SalesTaxAmount"].ToString();
                this._OtherFeeTxt.Text = "";
                this._NoteTxt.Text = orderHeaderDr["Note"].ToString();
                this._TotalTxt.Text = orderHeaderDr["Total"].ToString();
                this._OrderLineDgv.DataSource = orderLineDt.DefaultView;
            }
            
        }

        private void OrderDetailForm_Load(object sender, EventArgs e)
        {

        }
    }
}
