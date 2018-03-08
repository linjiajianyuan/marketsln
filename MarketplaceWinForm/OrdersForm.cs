using Microsoft.VisualBasic;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketplaceWinForm
{
    public partial class OrdersForm : Form
    {
        public OrdersForm()
        {
            InitializeComponent();
            this._OrderDgv.ReadOnly = true;
        }
        private const int cellOrderNum = 0;
        private const int cellChannel = 16;
        private const int cellShippedDate = 14;
        private const int cellShipCountry = 11;
        private const int cellCustomizedMessage = 4;

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            // 0 = not shipped; 1 = shipped; 2 = both
            string ebayItemNum = this._EbayItemNumTxt.Text.Trim();
            string buyerUserId = this._BuyerUserIdTxt.Text.Trim();
            string name = this._NameTxt.Text.Trim();
            string email = this._EmailTxt.Text.Trim();
            string startDate = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd")+ " 00:00:00";
            string endDate = DateTime.Now.ToString("yyyy-MM-dd")+ " 23:59:59";
            DataTable orderInfoDt = MarketplaceMdl.Mdl.GetOrderView(0, startDate, endDate, ebayItemNum, buyerUserId, name, email);
            this._OrderDgv.DataSource = orderInfoDt.DefaultView;
        }

        private void _SearchBtn_Click(object sender, EventArgs e)
        {
            string startDate = this._StartDatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            string endDate = this._EndDatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            string ebayItemNum = this._EbayItemNumTxt.Text.Trim();
            string buyerUserId = this._BuyerUserIdTxt.Text.Trim();
            string name = this._NameTxt.Text.Trim();
            string email = this._EmailTxt.Text.Trim();
            int shipped = 0;
            if (this._UnshippedCheckBox.Checked ==true && this._ShippedCheckBox.Checked==false)
            {
                shipped = 0;
            }
            else if(this._UnshippedCheckBox.Checked == false && this._ShippedCheckBox.Checked == true)
            {
                shipped = 1;
            }
            else
            {
                shipped = 2;
            }
            DataTable orderInfoDt = MarketplaceMdl.Mdl.GetOrderView(shipped, startDate, endDate,ebayItemNum,buyerUserId,name,email);
            this._OrderDgv.DataSource = orderInfoDt.DefaultView;
            this._TotalLabel.Text = orderInfoDt.Rows.Count.ToString();
            MessageBox.Show("Done");
        }

        private void _UnshippedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // 0 = not shipped; 1 = shipped; 2 = both
            string startDate = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd") + " 00:00:00";
            string endDate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            string ebayItemNum = this._EbayItemNumTxt.Text.Trim();
            string buyerUserId = this._BuyerUserIdTxt.Text.Trim();
            string name = this._NameTxt.Text.Trim();
            string email = this._EmailTxt.Text.Trim();
            int shipped = 0;
            if (this._UnshippedCheckBox.Checked == false && this._ShippedCheckBox.Checked == false)
            {
                MessageBox.Show("Must check one of them");
                this._UnshippedCheckBox.Checked = true;
            }
            else if (this._UnshippedCheckBox.Checked == true && this._ShippedCheckBox.Checked == false)
            {
                shipped = 0;
            }
            else if (this._UnshippedCheckBox.Checked == false && this._ShippedCheckBox.Checked == true)
            {
                shipped = 1;
            }
            else
            {
                shipped = 2;
            }
            DataTable orderInfoDt = MarketplaceMdl.Mdl.GetOrderView(shipped, startDate, endDate, ebayItemNum, buyerUserId, name, email);
            this._OrderDgv.DataSource = orderInfoDt.DefaultView;
            this._TotalLabel.Text = orderInfoDt.Rows.Count.ToString();
        }

        private void _ShippedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // 0 = not shipped; 1 = shipped; 2 = both
            string startDate = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd") + " 00:00:00";
            string endDate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            string ebayItemNum = this._EbayItemNumTxt.Text.Trim();
            string buyerUserId = this._BuyerUserIdTxt.Text.Trim();
            string name = this._NameTxt.Text.Trim();
            string email = this._EmailTxt.Text.Trim();
            int shipped = 0;
            if(this._UnshippedCheckBox.Checked == false && this._ShippedCheckBox.Checked == false)
            {
                MessageBox.Show("Must check one of them");
                this._ShippedCheckBox.Checked = true;
            }
            else if (this._UnshippedCheckBox.Checked == true && this._ShippedCheckBox.Checked == false)
            {
                shipped = 0;
            }
            else if (this._UnshippedCheckBox.Checked == false && this._ShippedCheckBox.Checked == true)
            {
                shipped = 1;
            }
            else
            {
                shipped = 2;
            }
            DataTable orderInfoDt = MarketplaceMdl.Mdl.GetOrderView(shipped, startDate, endDate, ebayItemNum, buyerUserId, name, email);
            this._OrderDgv.DataSource = orderInfoDt.DefaultView;
            this._TotalLabel.Text = orderInfoDt.Rows.Count.ToString();
        }

        private void _CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                string orderId = this._OrderDgv.Rows[e.RowIndex].Cells[cellOrderNum].Value.ToString();
                string channel = this._OrderDgv.Rows[e.RowIndex].Cells[cellChannel].Value.ToString();
                OrderDetailForm frm = new OrderDetailForm(orderId, channel);
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Please select a right row");
            }
        }

        private void _PrintShippingLabelBtn_Click(object sender, EventArgs e)
        {
            DataTable readyToPrintDt = new DataTable();
            readyToPrintDt.Columns.Add("OrderNum",typeof(string));
            readyToPrintDt.Columns.Add("Channel", typeof(string));
            readyToPrintDt.Columns.Add("ShipCountry", typeof(string));
            readyToPrintDt.Columns.Add("CustomizedWeight", typeof(string));
            readyToPrintDt.Columns.Add("CustomizedMessage", typeof(string));


            int selectedRowCount =this._OrderDgv.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                List<string> customizedItemKeyWordList = ConfigurationManager.AppSettings["CustomizedItemKeyWord"].Split(',').ToList();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string orderNum = this._OrderDgv.SelectedRows[i].Cells[cellOrderNum].Value.ToString();
                    string channel = this._OrderDgv.SelectedRows[i].Cells[cellChannel].Value.ToString();
                    string shippedDate = this._OrderDgv.SelectedRows[i].Cells[cellShippedDate].Value.ToString();
                    string shipCountry = this._OrderDgv.SelectedRows[i].Cells[cellShipCountry].Value.ToString();
                    string customizedMessage = this._OrderDgv.SelectedRows[i].Cells[cellCustomizedMessage].Value.ToString();
                    DataRow readyToPrintDr = readyToPrintDt.NewRow();
                    if (shippedDate == "1/1/1753 12:00:00 AM")
                    {
                        DataTable orderLineTitleDt = MarketplaceDb.Db.GetOrderLineDtByOrderNum(orderNum,channel);
                        string orderItems = "";
                        string itemCustomizedMessage = "";
                        foreach(DataRow orderLineTitleDr in orderLineTitleDt.Rows)
                        {
                            string itemTitle = orderLineTitleDr["ItemDesc"].ToString();
                            string sku = orderLineTitleDr["ItemNum"].ToString();
                            string qty = orderLineTitleDr["Quantity"].ToString();
                            orderItems = orderItems + sku + "(" + qty + ")" + "|";
                            var ifCustomizedItem = customizedItemKeyWordList.Any(w=>itemTitle.Contains(w));
                            if(ifCustomizedItem)
                            {
                                itemCustomizedMessage = itemCustomizedMessage+"~~~"+ (Interaction.InputBox("Input Customized Message", orderItems, customizedMessage));
                            }
                        }
                        MarketplaceDb.Db.SaveNoteToDb(orderNum,channel,itemCustomizedMessage);
                        DataRow existingWeightDr = MarketplaceDb.Db.CheckDuplicatedCustomizedWeight(orderItems);
                        if(existingWeightDr==null)
                        {
                            string customizedWeight = (Interaction.InputBox("Input Weight", orderItems));

                            while (ConvertUtility.ToInt(customizedWeight) <= 0)
                            {
                                MessageBox.Show("Weight must more than 0! Please re-enter one");
                                customizedWeight = (Interaction.InputBox("Input Weight", orderItems));
                            }
                            MarketplaceDb.Db.SaveCustomizedWeight(orderItems, ConvertUtility.ToInt(customizedWeight));
                            readyToPrintDr["CustomizedWeight"]= customizedWeight;
                        }
                        else
                        {
                            readyToPrintDr["CustomizedWeight"] = existingWeightDr["CustomizedWeight"].ToString();
                        }
                       
                        readyToPrintDr["OrderNum"]= orderNum;
                        readyToPrintDr["Channel"] = channel;
                        readyToPrintDr["ShipCountry"] = shipCountry;
                        readyToPrintDr["CustomizedMessage"] = itemCustomizedMessage;
                        readyToPrintDt.Rows.Add(readyToPrintDr);
                    }
                    else
                    {
                        MessageBox.Show("OrderNum: "+orderNum + " has been printed already. Please go to order details form to reprint it");
                    }
                }
                foreach(DataRow dr in readyToPrintDt.Rows)
                {
                    string orderNum = dr["OrderNum"].ToString();
                    string channel = dr["Channel"].ToString();
                    string shipCountry = dr["ShipCountry"].ToString();
                    string customizedWeight = dr["CustomizedWeight"].ToString();
                    string customizedMessage = dr["CustomizedMessage"].ToString();
                    if(shipCountry !="US")
                    {
                        MessageBox.Show("Not Support International Shipping Yet!");

                        //Dictionary<string, string> labelDict = ProcessShippingLabel.GetInternationalLabel(orderNum, channel);
                        //string printResult = PrintShippingLabel.Print(labelDict);
                        //if(printResult== "Error, Email Sent")
                        //{
                        //    MessageBox.Show("Error, Email Sent");
                        //}
                    }
                    else
                    {
                        Dictionary<string, string> labelDict = ProcessShippingLabel.GetDomesticLabel(orderNum,channel,customizedWeight,customizedMessage);
                        string printResult = PrintShippingLabel.Print(labelDict);
                        if(printResult== "Error, Email Sent")
                        {
                            MessageBox.Show("Error, Email Sent");
                        }
                    }
                }
                MessageBox.Show("Printed!!");
            }
            else
            {
                MessageBox.Show("Please select an unshipped order at least");
            }
        }

        private void _OrderDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
