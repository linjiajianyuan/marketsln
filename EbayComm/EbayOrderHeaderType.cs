using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayComm
{
    public class EbayOrderHeaderType
    {
        public string OrderID { set; get; }
        public double AdjustmentAmount { set; get; }
        public double AmountPaid { set; get; }
        public double AmountSaved { set; get; }
        public string BuyerCheckoutMessage { set; get; }
        public string BuyerUserID { set; get; }
        public string eBayPaymentStatus { set; get; }
        public string PaymentMethod { set; get; }
        public DateTime CreatedTime { set; get; }
        public string ExtendedOrderID { set; get; }
        public string OrderStatus { set; get; }
        public DateTime PaidTime { set; get; }
        public DateTime PaymentExpectedReleaseDate { set; get; }
        public string PaymentHoldReason { set; get; }
        public string PaymentHoldStatus { set; get; }
        public string SellerEmail { set; get; }
        public string SellerUserID { set; get; }
        public string AddressID { set; get; }
        public string CityName { set; get; }
        public string Country { set; get; }
        public string CountryName { set; get; }
        public string ExternalAddressID { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public string PostalCode { set; get; }
        public string StateOrProvince { set; get; }
        public string Street1 { set; get; }
        public string Street2 { set; get; }
        public double SalesTaxAmount { set; get; }
        public double SalesTaxPerecent { set; get; }
        public string SalesTaxState { set; get; }
        public int ShippingIncludedInTax { set; get; }
        public string SellingManagerSalesRecordNumber { set; get; }
        public int ExpeditedService { set; get; }
        public string ShippingService { set; get; }
        public double ShippingServiceCost { set; get; }
        public double ShippingServiceAdditionalCost { set; get; }
        public int ShippingServicePriority { set; get; }
        public double Subtotal { set; get; }
        public double Total { set; get; }
        public int TransferStatus { set; get; }
        public DateTime EnterDate { set; get; }
        public DateTime UpdateDate { set; get; }
    }
}
