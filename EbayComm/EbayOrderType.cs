using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayComm
{
    public class EbayOrderType
    {
        private EbayOrderHeaderType _Header;
        public EbayOrderHeaderType Header
        {
            set
            {
                this._Header = value;
            }
            get
            {
                if(_Header==null)
                {
                    _Header = new EbayOrderHeaderType();
                }
                return _Header;
            }
        }
        private List<EbayOrderLineType> _Line;
        public List<EbayOrderLineType> Line
        {
            set
            {
                this._Line = value;
            }
            get
            {
                if (_Line == null)
                {
                    _Line = new List<EbayOrderLineType>();
                }
                return _Line;
            }
        }
        private List<EbayOrderPaymentTransactionType> _paymentTransaction;
        public List<EbayOrderPaymentTransactionType> paymentTransaction
        {
            set
            {
                this._paymentTransaction = value;
            }
            get
            {
                if (_paymentTransaction == null)
                {
                    _paymentTransaction = new List<EbayOrderPaymentTransactionType>();
                }
                return _paymentTransaction;
            }
        }
    }
}
