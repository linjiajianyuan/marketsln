using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayComm
{
    public class EbayOrderPaymentTransactionType
    {
        public string OrderID { set; get; }
        public string ExternalTransactionID { set; get; }
        public string ExternalTransactionStatus { set; get; }
        public DateTime ExternalTransactionTime { set; get; }
        public double FeeOrCreditAmount { set; get; }
        public double PaymentOrRefundAmount { set; get; }
        public DateTime EnterDate { set; get; }
        public DateTime UpdateDate { set; get; }
    }
}
