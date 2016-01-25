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
        public OrderDetailForm(string orderId)
        {
            InitializeComponent();
            this._OrderNumTxt.ReadOnly = true;
            this._OrderNumTxt.Text = orderId;
            
        }

        private void OrderDetailForm_Load(object sender, EventArgs e)
        {

        }
    }
}
