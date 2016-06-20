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
        }

        private void DomesticEndOfDayBtn_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> domesticEndOfDayDic=EndOfDay.DomesticEndOfDay();
            if (domesticEndOfDayDic == null)
            {
                MessageBox.Show("Done");
            }
            else
            {
                string[] arr = ((IEnumerable)domesticEndOfDayDic["ErrorMessages"]).Cast<object>().Select(x => x.ToString()).ToArray();
                foreach (string msg in arr)
                {
                    MessageBox.Show("Error Message: " + msg);
                }
            }
        }
    }
}
