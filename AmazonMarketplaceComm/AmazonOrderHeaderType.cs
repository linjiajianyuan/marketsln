using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMarketplaceComm
{
    public class AmazonOrderHeaderType
    {
        public string order_id { set; get; }
        public string purchase_date { set; get; }
        public string payments_date { set; get; }
        public string buyer_email { set; get; }
        public string buyer_name { set; get; }
        public string buyer_phone_number { set; get; }
        public string currency { set; get; }
        public string recipient_name { set; get; }
        public string ship_address_1 { set; get; }
        public string ship_address_2 { set; get; }
        public string ship_address_3 { set; get; }
        public string ship_city { set; get; }
        public string ship_state { set; get; }
        public string ship_postal_code { set; get; }
        public string ship_country { set; get; }
        public string ship_phone_number { set; get; }
        public string tax_location_code { set; get; }
        public string tax_location_city { set; get; }
        public string tax_location_county { set; get; }
        public string tax_location_state { set; get; }
        public string delivery_Instructions { set; get; }
        public string sales_channel { set; get; }
        public int dataTransferStatus { set; get; }
        public DateTime enterDate { set; get; }
        public DateTime updateDate { set; get; }
        public string orderStatus { set; get; }
        public string customizedMessage { set; get;}
        public string accountName { set; get; }
    }
}
