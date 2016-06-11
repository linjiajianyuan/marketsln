using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMarketplaceComm
{
    public class AmazonOrderType
    {
        private AmazonOrderHeaderType _Header;
        public AmazonOrderHeaderType Header
        {
            set
            {
                this._Header = value;
            }
            get
            {
                if (_Header == null)
                {
                    _Header = new AmazonOrderHeaderType();
                }
                return _Header;
            }
        }

        private List<AmazonOrderLineType> _Lines;
        public List<AmazonOrderLineType> Lines
        {
            set
            {
                this._Lines = value;
            }
            get
            {
                if (_Lines == null)
                {
                    _Lines = new List<AmazonOrderLineType>();
                }
                return _Lines;
            }
        }
    }
}
