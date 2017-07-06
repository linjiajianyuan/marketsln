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
    public partial class SKUMapForm : Form
    {
        public SKUMapForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string skuMapColumnName = MarketplaceMdl.Mdl.GetSKUMapColumnName();
            List<string> skuMapColumnNameList = skuMapColumnName.Split(',').ToList();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataTable csvTable = new DataTable();
                csvTable = CsvUtility.LoadCsvFileAsDataTable(openFileDialog1.FileName);
                var csvColumnName = csvTable.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                List<string> csvColumnNameList = csvColumnName.ToList();
                bool isFileOk = true;
                foreach (string columName in csvColumnName)
                {
                    if(!skuMapColumnNameList.Contains(columName))
                    {
                        isFileOk = false;
                        MessageBox.Show("Error, unknow column from CSV file: "+ columName);
                        break;
                    }
                }
                if (isFileOk == true)
                {
                    Dictionary<string, string> d = new Dictionary<string, string>();
                    foreach (DataRow dr in csvTable.Rows)
                    {
                        string vendorSku = dr["vendorSku"].ToString().Trim();
                        DataRow vendorSKUInfoDr = MarketplaceMdl.Mdl.CheckIsVendorSKUExist(vendorSku);
                        if(vendorSKUInfoDr==null || vendorSKUInfoDr["VendorSKU"].ToString()=="")
                        {
                            //insert
                            foreach(string csvTableColumnName in csvColumnNameList)
                            {

                            }
                        }
                        else
                        {
                            //update
                        }
                    }
                }
            }
        }
    }
}
