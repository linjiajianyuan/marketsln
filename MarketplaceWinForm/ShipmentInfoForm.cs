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
    public partial class ShipmentInfoForm : Form
    {
        public ShipmentInfoForm()
        {
            InitializeComponent();
            DataTable shipmentInfoDt = MarketplaceDb.Db.GetShipmentInfo();
            _ShipmentInfoDgv.DataSource = shipmentInfoDt.DefaultView;
        }
    }
}
