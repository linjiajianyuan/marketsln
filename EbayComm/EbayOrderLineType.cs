using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayComm
{
    public class EbayOrderLineType
    {
        public string OrderID { set; get; }
        public string BuyerEmail { set; get; }
        public string BuyerFirstName { set; get; }
        public string BuyerLastName { set; get; }
        public DateTime CreatedDate { set; get; }
        public string ExtendedOrderID { set; get; }
        public double FinalValueFee { set; get; }
        public int Gift { set; get; }
        public string GiftMessage { set; get; }
        public string InventoryReservationID { set; get; }
        public DateTime InvoiceSentTime { set; get; }
        public string ItemID { set; get; }
        public string OrderLineItemID { set; get;}
        public string PrivateNotes { set; get; }
        public string Site { set; get; }
        public int LineID { set; get; }
        public int QuantityPurchased { set; get;}
        public string SKU { set; get; }
        public string VariationSKU { set; get; }
        public string VariationNameValueList { set; get; }
        public string Title { set; get; }
        public string VariationTitle { set; get; }
        public string TransactionID { set; get; }
        public double TransactionPrice { set; get; }
        public DateTime EnterDate { set; get; }
        public DateTime Updatedate { set; get; }
    }
}
