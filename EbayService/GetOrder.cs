using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using EbayComm;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayService
{
    public class GetOrder
    {
        private static EbayOrderHeaderType AddOrderHeader(OrderType orderType)
        {
            EbayOrderHeaderType ebayOrderHeaderType = new EbayOrderHeaderType();
            ebayOrderHeaderType.OrderID = orderType.OrderID;
            ebayOrderHeaderType.AdjustmentAmount = orderType.AdjustmentAmount.Value;
            ebayOrderHeaderType.AmountPaid = orderType.AmountPaid.Value;
            ebayOrderHeaderType.AmountSaved = orderType.AmountSaved.Value;
            ebayOrderHeaderType.BuyerCheckoutMessage = orderType.BuyerCheckoutMessage == null ? "" : orderType.BuyerCheckoutMessage.ToString().Replace("'", "''");
            ebayOrderHeaderType.BuyerUserID = orderType.BuyerUserID.ToString().Replace("'", "''");
            ebayOrderHeaderType.eBayPaymentStatus = orderType.CheckoutStatus.eBayPaymentStatus.ToString().Replace("'", "''");
            ebayOrderHeaderType.PaymentMethod = orderType.CheckoutStatus.PaymentMethod.ToString().Replace("'", "''");
            if (orderType.CreatedTime.ToLocalTime() <= ConvertUtility.ToDateTime("1901-01-01 00：00：00"))
            {
                ebayOrderHeaderType.CreatedTime = (DateTime)SqlDateTime.MinValue;
            }
            else
            {
                ebayOrderHeaderType.CreatedTime = orderType.CreatedTime.ToLocalTime();
            }
            ebayOrderHeaderType.ExtendedOrderID = orderType.ExtendedOrderID == null ? "" : orderType.ExtendedOrderID.ToString();


            ebayOrderHeaderType.OrderStatus = orderType.OrderStatus.ToString();
            if (orderType.PaidTime.ToLocalTime() <= ConvertUtility.ToDateTime("1901-01-01 00：00：00"))
            {
                ebayOrderHeaderType.PaidTime = (DateTime)SqlDateTime.MinValue;
            }
            else
            {
                ebayOrderHeaderType.PaidTime = orderType.PaidTime.ToLocalTime();
            }
            if (orderType.PaymentHoldDetails == null)
            {
                ebayOrderHeaderType.PaymentExpectedReleaseDate = (DateTime)SqlDateTime.MinValue;
                ebayOrderHeaderType.PaymentHoldReason = "";
            }
            else
            {
                if (orderType.PaymentHoldDetails.ExpectedReleaseDate.ToLocalTime() <= ConvertUtility.ToDateTime("1901-01-01 00：00：00"))
                {
                    ebayOrderHeaderType.PaymentExpectedReleaseDate = (DateTime)SqlDateTime.MinValue;
                }
                else
                {
                    ebayOrderHeaderType.PaymentExpectedReleaseDate = orderType.PaymentHoldDetails.ExpectedReleaseDate.ToLocalTime();
                }
                ebayOrderHeaderType.PaymentHoldReason = orderType.PaymentHoldDetails.PaymentHoldReason.ToString();
            }
            ebayOrderHeaderType.PaymentHoldStatus = orderType.PaymentHoldStatus.ToString();
            ebayOrderHeaderType.SellerEmail = orderType.SellerEmail.Replace("'", "''");
            ebayOrderHeaderType.SellerUserID = orderType.SellerUserID.Replace("'", "''");
            ebayOrderHeaderType.AddressID = orderType.ShippingAddress.AddressID;
            ebayOrderHeaderType.CityName = orderType.ShippingAddress.CityName.Replace("'", "''");
            ebayOrderHeaderType.Country = orderType.ShippingAddress.Country.ToString();
            ebayOrderHeaderType.CountryName = orderType.ShippingAddress.CountryName.ToString().Replace("'", "''");
            ebayOrderHeaderType.ExternalAddressID = orderType.ShippingAddress.ExternalAddressID == null ? "" : orderType.ShippingAddress.ExternalAddressID.ToString();
            ebayOrderHeaderType.Name = orderType.ShippingAddress.Name == null ? "" : orderType.ShippingAddress.Name.ToString().Replace("'", "''");
            ebayOrderHeaderType.Phone = orderType.ShippingAddress.Phone == null ? "" : orderType.ShippingAddress.Phone.ToString().Replace("'", "''"); ;
            ebayOrderHeaderType.PostalCode = orderType.ShippingAddress.PostalCode == null ? "" : orderType.ShippingAddress.PostalCode.ToString().Replace("'", "''");
            ebayOrderHeaderType.StateOrProvince = orderType.ShippingAddress.StateOrProvince == null ? "" : orderType.ShippingAddress.StateOrProvince.ToString().Replace("'", "''");
            ebayOrderHeaderType.Street1 = orderType.ShippingAddress.Street1 == null ? "" : orderType.ShippingAddress.Street1.ToString().Replace("'", "''");
            ebayOrderHeaderType.Street2 = orderType.ShippingAddress.Street2 == null ? "" : orderType.ShippingAddress.Street2.ToString().Replace("'", "''");
            ebayOrderHeaderType.SalesTaxAmount = orderType.ShippingDetails.SalesTax.SalesTaxAmount == null ? 0 : orderType.ShippingDetails.SalesTax.SalesTaxAmount.Value;
            ebayOrderHeaderType.SalesTaxPerecent = ConvertUtility.ToDouble(orderType.ShippingDetails.SalesTax.SalesTaxPercent.ToString());
            ebayOrderHeaderType.SalesTaxState = orderType.ShippingDetails.SalesTax.SalesTaxState == null ? "" : orderType.ShippingDetails.SalesTax.SalesTaxState.ToString();
            ebayOrderHeaderType.ShippingIncludedInTax = orderType.ShippingDetails.SalesTax.ShippingIncludedInTax == true ? 1 : 0;
            ebayOrderHeaderType.SellingManagerSalesRecordNumber = orderType.ShippingDetails.SellingManagerSalesRecordNumber.ToString();
            ebayOrderHeaderType.ExpeditedService = orderType.ShippingServiceSelected.ExpeditedService == true ? 1 : 0;
            ebayOrderHeaderType.ShippingService = orderType.ShippingServiceSelected.ShippingService == null ? "" : orderType.ShippingServiceSelected.ShippingService.ToString();
            ebayOrderHeaderType.ShippingServiceCost = orderType.ShippingServiceSelected.ShippingServiceCost == null ? 0 : orderType.ShippingServiceSelected.ShippingServiceCost.Value;
            ebayOrderHeaderType.ShippingServiceAdditionalCost = orderType.ShippingServiceSelected.ShippingServiceAdditionalCost == null ? 0 : orderType.ShippingServiceSelected.ShippingServiceAdditionalCost.Value;
            ebayOrderHeaderType.ShippingServicePriority = orderType.ShippingServiceSelected.ShippingServicePriority;
            ebayOrderHeaderType.Subtotal = orderType.Subtotal == null ? 0 : orderType.Subtotal.Value;
            ebayOrderHeaderType.Total = orderType.Total == null ? 0 : orderType.Total.Value;
            ebayOrderHeaderType.EnterDate = System.DateTime.Now;
            ebayOrderHeaderType.UpdateDate = System.DateTime.Now;
            return ebayOrderHeaderType;
        }
        private static EbayOrderPaymentTransactionType AddOrderPaymentTransaction(string orderID, ExternalTransactionType externalTransactionType)
        {
            EbayOrderPaymentTransactionType ebayOrderPaymentTransactionType = new EbayOrderPaymentTransactionType();
            ebayOrderPaymentTransactionType.OrderID = orderID;
            ebayOrderPaymentTransactionType.ExternalTransactionID = externalTransactionType.ExternalTransactionID.ToString();
            ebayOrderPaymentTransactionType.ExternalTransactionStatus = externalTransactionType.ExternalTransactionStatus.ToString();
            if (externalTransactionType.ExternalTransactionTime.ToLocalTime() <= ConvertUtility.ToDateTime("1901-01-01 00：00：00"))
            {
                ebayOrderPaymentTransactionType.ExternalTransactionTime = (DateTime)SqlDateTime.MinValue;
            }
            else
            {
                ebayOrderPaymentTransactionType.ExternalTransactionTime = externalTransactionType.ExternalTransactionTime.ToLocalTime();
            }
            ebayOrderPaymentTransactionType.FeeOrCreditAmount = externalTransactionType.FeeOrCreditAmount == null ? 0 : externalTransactionType.FeeOrCreditAmount.Value;
            ebayOrderPaymentTransactionType.PaymentOrRefundAmount = externalTransactionType.PaymentOrRefundAmount == null ? 0 : externalTransactionType.PaymentOrRefundAmount.Value;
            ebayOrderPaymentTransactionType.EnterDate = System.DateTime.Now;
            ebayOrderPaymentTransactionType.UpdateDate = System.DateTime.Now;
            return ebayOrderPaymentTransactionType;
        }
        private static EbayOrderLineType AddOrderLine(string orderID, TransactionType transactionType,int lineId)
        {
            EbayOrderLineType ebayOrderLineType = new EbayOrderLineType();
            ebayOrderLineType.OrderID = orderID;
            ebayOrderLineType.BuyerEmail = transactionType.Buyer.Email == null ? "" : transactionType.Buyer.Email.ToString().Replace("'", "''");
            ebayOrderLineType.BuyerFirstName = transactionType.Buyer.UserFirstName == null ? "" : transactionType.Buyer.UserFirstName.ToString().Replace("'", "''");
            ebayOrderLineType.BuyerLastName = transactionType.Buyer.UserLastName==null?"":transactionType.Buyer.UserLastName.ToString().Replace("'", "''");
            if (transactionType.CreatedDate.ToLocalTime() <= ConvertUtility.ToDateTime("1901-01-01 00：00：00"))
            {
                ebayOrderLineType.CreatedDate = (DateTime)SqlDateTime.MinValue;
            }
            else
            {
                ebayOrderLineType.CreatedDate = transactionType.CreatedDate.ToLocalTime();
            }
            ebayOrderLineType.ExtendedOrderID = transactionType.ExtendedOrderID == null ? "" : transactionType.ExtendedOrderID.ToString();
            ebayOrderLineType.FinalValueFee = transactionType.FinalValueFee == null ? 0 : transactionType.FinalValueFee.Value;
            ebayOrderLineType.Gift = transactionType.Gift == true ? 1 : 0;
            ebayOrderLineType.GiftMessage = transactionType.GiftSummary==null?"":transactionType.GiftSummary.Message.ToString().Replace("'", "''");
            ebayOrderLineType.InventoryReservationID = transactionType.InventoryReservationID == null ? "" : transactionType.InventoryReservationID.ToString();
            if (transactionType.InvoiceSentTime.ToLocalTime() <= ConvertUtility.ToDateTime("1901-01-01 00：00：00"))
            {
                ebayOrderLineType.InvoiceSentTime = (DateTime)SqlDateTime.MinValue;
            }
            else
            {
                ebayOrderLineType.InvoiceSentTime = transactionType.InvoiceSentTime.ToLocalTime();
            }
            ebayOrderLineType.ItemID = transactionType.Item.ItemID == null ? "": transactionType.Item.ItemID.ToString();
            ebayOrderLineType.OrderLineItemID = transactionType.OrderLineItemID == null ? "" : transactionType.OrderLineItemID.ToString();
            ebayOrderLineType.PrivateNotes = transactionType.Item.PrivateNotes==null?"":transactionType.Item.PrivateNotes.ToString().Replace("'", "''");
            ebayOrderLineType.Site = transactionType.Item.Site.ToString().Replace("'", "''");
            ebayOrderLineType.LineID = lineId;
            ebayOrderLineType.QuantityPurchased = transactionType.QuantityPurchased;
            ebayOrderLineType.SKU = transactionType.Item.SKU == null ? "" : transactionType.Item.SKU.ToString();
            if (transactionType.Variation == null)
            {
                ebayOrderLineType.VariationSKU = "";
                ebayOrderLineType.VariationNameValueList = "";
                ebayOrderLineType.VariationTitle = "";
            }
            else
            {
                ebayOrderLineType.VariationSKU = transactionType.Variation.SKU == null ? "" : transactionType.Variation.SKU.ToString().Replace("'", "''");
                string variationNameValueList = "";
                foreach (NameValueListType nameValueListType in transactionType.Variation.VariationSpecifics)
                {
                    variationNameValueList = variationNameValueList+nameValueListType.Name.ToString().Replace("'", "''") + ":";
                    foreach (string value in nameValueListType.Value)
                    {
                        variationNameValueList = variationNameValueList+value.ToString().Replace("'", "''") + ";";
                    }
                }
                ebayOrderLineType.VariationNameValueList = variationNameValueList;
                ebayOrderLineType.VariationTitle = transactionType.Variation.VariationTitle == null ? "" : transactionType.Variation.VariationTitle.ToString().Replace("'", "''");
            }

            ebayOrderLineType.Title = transactionType.Item.Title == null ? "" : transactionType.Item.Title.ToString().Replace("'", "''");
            ebayOrderLineType.TransactionID = transactionType.TransactionID == null ? "" : transactionType.TransactionID.ToString();
            ebayOrderLineType.TransactionPrice = transactionType.TransactionPrice == null ? 0 : transactionType.TransactionPrice.Value;
            ebayOrderLineType.EnterDate = System.DateTime.Now;
            ebayOrderLineType.Updatedate = System.DateTime.Now;
            return ebayOrderLineType;
        }
        public static List<EbayOrderType> GetOrderFromEbay(string token,string accountNum)
        {
            string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
            string messageFromPassword= ConfigurationManager.AppSettings["messageFromPassword"];
            string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
            string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
            int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

            List<EbayOrderType> OrderList = new List<EbayOrderType>();
            ApiContext context = new ApiContext();
            context.ApiCredential.eBayToken = token;
            context.SoapApiServerUrl = "https://api.ebay.com/wsapi";
            context.Site = SiteCodeType.US;

            DateTime createTimeFrom, createTimeTo;
            GetOrdersCall GetOrderCall = new GetOrdersCall(context);
            GetOrderCall.DetailLevelList = new DetailLevelCodeTypeCollection();
            GetOrderCall.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);

            createTimeFrom = DateTime.Now.AddDays(-3).ToUniversalTime();
            createTimeTo = DateTime.Now.ToUniversalTime();

            int pageNumber = 1;
            int errorCount = 0;
            GetOrderCall.CreateTimeFrom = createTimeFrom;
            GetOrderCall.CreateTimeTo = createTimeTo;
            GetOrderCall.OrderStatus = OrderStatusCodeType.All;

            while (pageNumber<100)
            {
                try
                {
                    PaginationType pagination = new PaginationType();
                    pagination.EntriesPerPage = 100;
                    GetOrderCall.Pagination = pagination;
                    pagination.PageNumber = pageNumber;
                    GetOrderCall.Execute();

                    int totalPageNumber = GetOrderCall.PaginationResult.TotalNumberOfPages;
                    if (pageNumber > totalPageNumber)
                    {
                        break;
                    }
                    else
                    {
                        if (GetOrderCall.ApiResponse.Ack != AckCodeType.Failure)
                        {
                            if (GetOrderCall.ApiResponse.OrderArray.Count != 0)
                            {
                                foreach (OrderType orderType in GetOrderCall.ApiResponse.OrderArray)
                                {
                                    try
                                    {
                                        EbayOrderType ebayOrderType = new EbayOrderType();
                                        string orderId = orderType.OrderID;
                                        DataRow checkDuplidatedDr = Db.Db.CheckDuplicatedOrderID(orderId);
                                        if (checkDuplidatedDr!=null)
                                        {
                                            continue; // continue if order already exist
                                        }
                                        EbayOrderHeaderType ebayOrderHeaderType = AddOrderHeader(orderType);//add header info
                                        foreach (ExternalTransactionType externalTransactionType in orderType.ExternalTransaction)//add external trasnaction info
                                        {
                                            EbayOrderPaymentTransactionType ebayOrderPaymentTransactionType = AddOrderPaymentTransaction(orderId, externalTransactionType);
                                            ebayOrderType.paymentTransaction.Add(ebayOrderPaymentTransactionType);
                                        }
                                        ebayOrderType.Header = ebayOrderHeaderType;
                                        int lineId = 0;
                                        foreach (TransactionType transactionType in orderType.TransactionArray)//add line info
                                        {
                                            lineId = lineId + 1;
                                            EbayOrderLineType ebayOrderLineType = AddOrderLine(orderId, transactionType, lineId);
                                            ebayOrderType.Line.Add(ebayOrderLineType);
                                        }
                                        OrderList.Add(ebayOrderType);
                                    }
                                    catch(Exception ex)
                                    {
                                        ExceptionUtility exceptionUtility = new ExceptionUtility();
                                        exceptionUtility.CatchMethod(ex, accountNum + ": GetOrderCall--foreach ", ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                        continue; //continue next single order if current order error
                                    }
                                }
                            }
                            else
                            {
                                continue; // continue next account if no order
                            }
                        }
                        else
                        {
                            ExceptionUtility exceptionUtility = new ExceptionUtility();
                            exceptionUtility.ErrorWarningMethod("AckCodeType Failure: " + accountNum, "AckCodeType Failure: " + accountNum, senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                            continue; // continue next account if ack error
                        }
                    }
                    pageNumber = pageNumber + 1;
                }
                catch (Exception ex)
                {
                    if (errorCount > 4)
                    {
                        ExceptionUtility exceptionUtility = new ExceptionUtility();
                        exceptionUtility.CatchMethod(ex,accountNum+ ": While ",ex.Message.ToString(),senderEmail,messageFromPassword,messageToEmail,smtpClient,smtpPortNum);
                        break;
                    }
                    else
                    {
                        errorCount = errorCount + 1;
                        continue; // continue try if error less 4 times
                    }
                }
            }

            return OrderList;
        }
    }
}
