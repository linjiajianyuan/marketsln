using PebbledonUtilityLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketplaceWinForm
{
    public partial class EndOfDayForm : Form
    {
        public EndOfDayForm()
        {
            InitializeComponent();
            _StartDateTime.Format = DateTimePickerFormat.Custom;
            _StartDateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss"; // Only use hours and minutes
            _StartDateTime.ShowUpDown = true;
            _EndDateTime.Format = DateTimePickerFormat.Custom;
            _EndDateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss"; // Only use hours and minutes
            _EndDateTime.ShowUpDown = true;
        }

        private void DomesticEndOfDayBtn_Click(object sender, EventArgs e)
        {
            string startDateTime = _StartDateTime.Value.ToString();
            string endDateTime = _EndDateTime.Value.ToString();
            DataTable dt = MarketplaceDb.Db.GetEndOfDayInfo(startDateTime, endDateTime);
            dataGridView1.DataSource = dt.DefaultView;
            //Dictionary<string, object> domesticEndOfDayDic=EndOfDay.DomesticEndOfDay();
            //if (domesticEndOfDayDic == null)
            //{
            //    MessageBox.Show("Done");
            //}
            //else
            //{
            //    string[] arr = ((IEnumerable)domesticEndOfDayDic["ErrorMessages"]).Cast<object>().Select(x => x.ToString()).ToArray();
            //    foreach (string msg in arr)
            //    {
            //        MessageBox.Show("Error Message: " + msg);
            //    }
            //}

        }

        private void _StartDateTime_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
