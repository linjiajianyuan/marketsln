using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMarketplaceComm
{
    public class AmazonOrderLineType
    {
        public string order_item_id { set; get; }
        public string amazon_order_id { set; get; }
        public string sku { set; get; }
        public string product_name { set; get; }
        public int quantity_purchased { set; get; }
        public decimal item_price { set; get; }
        public decimal item_tax { set; get; }
        public decimal shipping_price { set; get; }
        public decimal shipping_tax { set; get; }
        public string ship_service_level { set; get; }
        public decimal per_unit_item_taxable_district { set; get; }
        public decimal per_unit_item_taxable_city { set; get; }
        public decimal per_unit_item_taxable_county { set; get; }
        public decimal per_unit_item_taxable_state { set; get; }
        public decimal per_unit_item_non_taxable_district { set; get; }
        public decimal per_unit_item_non_taxable_city { set; get; }
        public decimal per_unit_item_non_taxable_county { set; get; }
        public decimal per_unit_item_non_taxable_state { set; get; }
        public decimal per_unit_item_zero_rated_district { set; get; }
        public decimal per_unit_item_zero_rated_city { set; get; }
        public decimal per_unit_item_zero_rated_county { set; get; }
        public decimal per_unit_item_zero_rated_state { set; get; }
        public decimal per_unit_item_tax_collected_district { set; get; }
        public decimal per_unit_item_tax_collected_city { set; get; }
        public decimal per_unit_item_tax_collected_county { set; get; }
        public decimal per_unit_item_tax_collected_state { set; get; }
        public decimal per_unit_shipping_taxable_district { set; get; }
        public decimal per_unit_shipping_taxable_city { set; get; }
        public decimal per_unit_shipping_taxable_county { set; get; }
        public decimal per_unit_shipping_taxable_state { set; get; }
        public decimal per_unit_shipping_non_taxable_district { set; get; }
        public decimal per_unit_shipping_non_taxable_city { set; get; }
        public decimal per_unit_shipping_non_taxable_county { set; get; }
        public decimal per_unit_shipping_non_taxable_state { set; get; }
        public decimal per_unit_shipping_zero_rated_district { set; get; }
        public decimal per_unit_shipping_zero_rated_city { set; get; }
        public decimal per_unit_shipping_zero_rated_county { set; get; }
        public decimal per_unit_shipping_zero_rated_state { set; get; }
        public decimal per_unit_shipping_tax_collected_district { set; get; }
        public decimal per_unit_shipping_tax_collected_city { set; get; }
        public decimal per_unit_shipping_tax_collected_county { set; get; }
        public decimal per_unit_shipping_tax_collected_state { set; get; }
        public decimal item_promotion_discount { set; get; }
        public int item_promotion_id { set; get; }
        public decimal ship_promotion_discount { set; get; }
        public int ship_promotion_id { set; get; }
        //public string delivery_start_date { set; get; }
        //public string delivery_end_date { set; get; }
        public string delivery_time_zone { set; get; }
        public int dataTransferStatus { set; get; }
    }
}
