using AmazonMarketplaceComm;
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

namespace Db
{
    public class Db
    {
        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

        public static DataTable GetAmazonDeveloperInfo()
        {
            string sql = @"select AccountName, Channel, EbayDeveloperID as SellerID, EbayCertificateID as MarketplaceID, EbayApplicationID as AccessKeyID, Token as SecretKey 
                            from SellerAccount where Channel = 'Amazon'";
            try
            {
                return SqlHelper.ExecuteDataTable(sql, ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static DataTable GetEbayDeveloperInfo()
        {
            string sql = "select AccountName,Token from SellerAccount where Channel='eBay'";
            try
            {
                return SqlHelper.ExecuteDataTable(sql,ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private static string BuildEbayHeaderSql(EbayOrderHeaderType eohType)
        {
            string sql = @"INSERT INTO [dbo].[EbayOrderHeader]
           ([OrderID],[AdjustmentAmount],[AmountPaid],[AmountSaved],[BuyerCheckoutMessage]
           ,[BuyerUserID],[eBayPaymentStatus],[PaymentMethod],[CreatedTime],[ExtendedOrderID]
           ,[OrderStatus],[PaidTime],[PaymentExpectedReleaseDate],[PaymentHoldReason]
           ,[PaymentHoldStatus],[SellerEmail],[SellerUserID],[AddressID]
           ,[CityName],[Country],[CountryName],[ExternalAddressID]
           ,[Name],[Phone],[PostalCode],[StateOrProvince]
           ,[Street1] ,[Street2]
           ,[SalesTaxAmount],[SalesTaxPercent],[ShippingIncludedInTax]
           ,[SalesTaxState],[SellingManagerSalesRecordNumber],[ExpeditedService]
           ,[ShippingService],[ShippingServiceCost],[ShippingServiceAdditionalCost]
           ,[ShippingServicePriority],[Subtotal],[Total],[TransferStatus]
           ,[EnterDate],[UpdateDate])
     VALUES
           ('"
           + eohType.OrderID+"','"+ eohType.AdjustmentAmount + "','" + eohType.AmountPaid + "','" + eohType.AmountSaved + "','" + eohType.BuyerCheckoutMessage + "',N'" 
           + eohType.BuyerUserID + "','" + eohType.eBayPaymentStatus + "','" +eohType.PaymentMethod + "','" +eohType.CreatedTime + "','" +eohType.ExtendedOrderID + "','"
           + eohType.OrderStatus + "','" +eohType.PaidTime + "','" +eohType.PaymentExpectedReleaseDate + "','" +eohType.PaymentHoldReason + "','" 
           + eohType.PaymentHoldStatus + "','" +eohType.SellerEmail +"','" +eohType.SellerUserID + "','" +eohType.AddressID + "',N'" 
           + eohType.CityName + "',N'" +eohType.Country + "',N'" +eohType.CountryName + "','" +eohType.ExternalAddressID + "',N'"
           + eohType.Name + "','" +eohType.Phone + "',N'" +eohType.PostalCode + "',N'" +eohType.StateOrProvince + "',N'"
           + eohType.Street1 + "',N'" +eohType.Street2 + "','"
           + eohType.SalesTaxAmount + "','" +eohType.SalesTaxPerecent + "','" +eohType.ShippingIncludedInTax + "',N'" 
           + eohType.SalesTaxState + "','" +eohType.SellingManagerSalesRecordNumber + "','" +eohType.ExpeditedService + "','"
           + eohType.ShippingService + "','" +eohType.ShippingServiceCost + "','" +eohType.ShippingServiceAdditionalCost + "','"
           + eohType.ShippingServicePriority + "','" +eohType.Subtotal + "','" +eohType.Total + "','" +eohType.TransferStatus + "','"
           + eohType.EnterDate + "','" +eohType.UpdateDate+"')";
            return sql;
        }
        private static string BuildEbayPaymentTransactionSql(EbayOrderPaymentTransactionType eoptType)
        {
            string sql = @"INSERT INTO [dbo].[EbayPaymentTransaction]
           ([OrderID]
           ,[ExternalTransactionID]
           ,[ExternalTransactionStatus]
           ,[ExternalTransactionTime]
           ,[FeeOrCreditAmount]
           ,[PaymentOrRefundAmount]
           ,[EnterDate]
           ,[UpdateDate])
     VALUES
           ('"
            +eoptType.OrderID+"','"+eoptType.ExternalTransactionID + "','" +eoptType.ExternalTransactionStatus + "','"
            +eoptType.ExternalTransactionTime + "','" +eoptType.FeeOrCreditAmount + "','" +eoptType.PaymentOrRefundAmount + "','"
            +eoptType.EnterDate + "','" +eoptType.UpdateDate+"')";
            return sql;
        }
        private static string BuildEbayLineSql(EbayOrderLineType eolType)
        {
            string sql = @"INSERT INTO [dbo].[EbayOrderLine]
           ([OrderID],[BuyerEmail],[BuyerFirstName],[BuyerLastName]
           ,[CreatedDate],[ExtendedOrderID],[FinalValueFee],[Gift]
           ,[GiftMessage],[InventoryReservationID]
           ,[InvoiceSentTime],[ItemID],[OrderLineItemID],[PrivateNotes]
           ,[Site],[LineID],[QuantityPurchased],[SKU],[VariationSKU]
           ,[VariationNameValueList],[Title]
           ,[VariationTitle],[TransactionID]
           ,[TransactionPrice],[EnterDate],[Updatedate])
     VALUES
           ('"
           +eolType.OrderID + "','" +eolType.BuyerEmail + "','" +eolType.BuyerFirstName + "','" +eolType.BuyerLastName + "','"
           +eolType.CreatedDate + "','" + eolType.ExtendedOrderID + "','" +eolType.FinalValueFee + "','" +eolType.Gift + "','"
           +eolType.GiftMessage + "','" +eolType.InventoryReservationID + "','" 
           +eolType.InvoiceSentTime + "','" +eolType.ItemID + "','" +eolType.OrderLineItemID + "','" +eolType.PrivateNotes + "','" 
           +eolType.Site + "','" +eolType.LineID + "','" +eolType.QuantityPurchased + "','" + eolType.SKU + "','" +eolType.VariationSKU + "','" 
           +eolType.VariationNameValueList + "','" +eolType.Title + "','"
           +eolType.VariationTitle + "','" +eolType.TransactionID + "','" 
           +eolType.TransactionPrice + "','" +eolType.EnterDate + "','" +eolType.Updatedate+"')";
            return sql;
        }
        public static void AddEbayOrderToDb(EbayOrderType ebayOrderType)
        {
            try
            {
                List<string> sqlList = new List<string>();
                string headerSql = BuildEbayHeaderSql(ebayOrderType.Header);
                string eoptSql = "";
                string eolSql = "";
                sqlList.Add(headerSql);
                foreach (EbayOrderPaymentTransactionType eoptType in ebayOrderType.paymentTransaction)
                {
                    eoptSql = BuildEbayPaymentTransactionSql(eoptType);
                    sqlList.Add(eoptSql);
                }
                foreach (EbayOrderLineType eolType in ebayOrderType.Line)
                {
                    eolSql = BuildEbayLineSql(eolType);
                    sqlList.Add(eolSql);
                }
                SqlHelper.ExecuteNonQuery(sqlList, ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, ebayOrderType.Header.SellerUserID + ": AddEbayOrderToDb ", ebayOrderType.Header.OrderID+": "+ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
            

        }
        public static DataRow CheckDuplicatedOrderID(string orderId)
        {
            string sql="select OrderID, OrderStatus from EbayOrderHeader where OrderID='"+orderId+"'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable GetEbayShippedOrderInfo(string accountName)
        {
            string sql = "select * from OrderHeader where Reference2 = '' and TrackingNum <> '' and Channel = 'eBay' and AccountName='" + accountName + "'";
            try
            {
                return SqlHelper.ExecuteDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateShipmentInfoDt(string orderNum, string accountName, DateTime uploadDate)
        {
            string sql = "update OrderHeader set Reference2='" + uploadDate+"' where OrderNum='"+orderNum+"' and AccountName='"+accountName+"'";
            try
            {
                SqlHelper.ExecuteNonQuery(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, accountName + ": UpdateShipmentInfoDt ", orderNum + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }

        public static DataRow CheckAmazonOrderDuplicatedDb(string order_id)
        {
            string sql = "select [amazon-order-id] from [AmazonOrderLine] where [amazon-order-id] ='" + order_id + "'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateAmazonOrderToCancel(string orderId, string orderStatus)
        {
            string sql = "update AmazonOrderHeader set [order-status]='" + orderStatus + "' where [order-id] ='" + orderId + "'";
            try
            {
                SqlHelper.ExecuteNonQuery(sql, ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void AddAmazonOrderTran(string accountName,AmazonOrderType amazonOrder)
        {
            List<string> sqlList = BuildAddAmazonOrderTranSql(amazonOrder);
            try
            {
                SqlHelper.ExecuteNonQuery(sqlList, ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, accountName + ": AddAmazonOrderTran ", amazonOrder.Header.order_id + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }
        public static List<string> BuildAddAmazonOrderTranSql(AmazonOrderType amazonOrder)
        {
            List<string> sqlList = new List<string>();
            try
            {
                sqlList.Add(BuildAddAmazonOrderHeaderSql(amazonOrder.Header));
                foreach (AmazonOrderLineType orderLineType in amazonOrder.Lines)
                {
                    sqlList.Add(BuildAddAmazonOrderLineSql(orderLineType));
                }

                return sqlList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string BuildAddAmazonOrderHeaderSql(AmazonOrderHeaderType amazonOrderType)
        {
            return "INSERT INTO [AmazonOrderHeader] "
           + "([order-id],[purchase-date] ,[payments-date],[buyer-email],[buyer-name],[buyer-phone-number] ,[currency],"
           + "[recipient-name],[ship-address-1],[ship-address-2],[ship-address-3],[ship-city],[ship-state],"
           + "[ship-postal-code],[ship-country],[ship-phone-number],[tax-location-code],[tax-location-city],"
           + "[tax-location-county] ,[tax-location-state] ,[delivery-Instructions],[sales-channel],[dataTransferStatus],[EnterDate],[UpdateDate],[order-status],AccountName,Amount,CustomizedMessage) "
           + "VALUES"
           + "('" + amazonOrderType.order_id + "','"
           + amazonOrderType.purchase_date + "','"
           + amazonOrderType.payments_date + "','"
           + amazonOrderType.buyer_email + "','"
           + amazonOrderType.buyer_name + "','"
           + amazonOrderType.buyer_phone_number + "','"
           + amazonOrderType.currency + "','"
           + amazonOrderType.recipient_name + "','"
           + amazonOrderType.ship_address_1 + "','"
           + amazonOrderType.ship_address_2 + "','"
           + amazonOrderType.ship_address_3 + "','"
           + amazonOrderType.ship_city + "','"
           + amazonOrderType.ship_state + "','"
           + amazonOrderType.ship_postal_code + "','"
           + amazonOrderType.ship_country + "','"
           + amazonOrderType.ship_phone_number + "','"
           + amazonOrderType.tax_location_code + "','"
           + amazonOrderType.tax_location_city + "','"
           + amazonOrderType.tax_location_county + "','"
           + amazonOrderType.tax_location_state + "','"
           + amazonOrderType.delivery_Instructions + "','"
           + amazonOrderType.sales_channel + "','"
           + amazonOrderType.dataTransferStatus + "','"
           + amazonOrderType.enterDate + "','"
           + amazonOrderType.updateDate + "','"
           + amazonOrderType.orderStatus + "','"
           + amazonOrderType.accountName + "','"
           + amazonOrderType.amount + "','"
           + amazonOrderType.customizedMessage.Replace("'", "''")
            + "')";
        }

        private static string BuildAddAmazonOrderLineSql(AmazonOrderLineType amazonOrderLineType)
        {
            return "INSERT INTO [AmazonOrderLine] "
           + "([order-item-id],[amazon-order-id],[sku],[product-name],[quantity-purchased] ,[item-price],"
           + "[item-tax],[shipping-price],[shipping-tax],[item-promotion-discount],"
           + "[ship-promotion-discount],"
           + "[dataTransferStatus]) "
           + "VALUES"
           + "('" + amazonOrderLineType.order_item_id + "','"
           + amazonOrderLineType.amazon_order_id + "','"
           + amazonOrderLineType.sku + "','"
           + amazonOrderLineType.product_name + "','"
           + amazonOrderLineType.quantity_purchased + "','"
           + amazonOrderLineType.item_price + "','"
           + amazonOrderLineType.item_tax + "','"
           + amazonOrderLineType.shipping_price + "','"
           + amazonOrderLineType.shipping_tax + "','"
           //+ amazonOrderLineType.ship_service_level + "','"
           // + amazonOrderLineType.per_unit_item_taxable_district + "','"
           //+ amazonOrderLineType.per_unit_item_taxable_city + "','"
           // + amazonOrderLineType.per_unit_item_taxable_county + "','"
           //+ amazonOrderLineType.per_unit_item_taxable_state + "','"
           //+ amazonOrderLineType.per_unit_item_non_taxable_district + "','"
           //+ amazonOrderLineType.per_unit_item_non_taxable_city + "','"
           //+ amazonOrderLineType.per_unit_item_non_taxable_county + "','"
           //+ amazonOrderLineType.per_unit_item_non_taxable_state + "','"
           //+ amazonOrderLineType.per_unit_item_zero_rated_district + "','"
           //+ amazonOrderLineType.per_unit_item_zero_rated_city + "','"
           //+ amazonOrderLineType.per_unit_item_zero_rated_county + "','"
           //+ amazonOrderLineType.per_unit_item_zero_rated_state + "','"
           //+ amazonOrderLineType.per_unit_item_tax_collected_district + "','"
           //+ amazonOrderLineType.per_unit_item_tax_collected_city + "','"
           //+ amazonOrderLineType.per_unit_item_tax_collected_county + "','"
           //+ amazonOrderLineType.per_unit_item_tax_collected_state + "','"
           //+ amazonOrderLineType.per_unit_shipping_taxable_district + "','"
           //+ amazonOrderLineType.per_unit_shipping_taxable_city + "','"
           //+ amazonOrderLineType.per_unit_shipping_taxable_county + "','"
           //+ amazonOrderLineType.per_unit_shipping_taxable_state + "','"
           //+ amazonOrderLineType.per_unit_shipping_non_taxable_district + "','"
           //+ amazonOrderLineType.per_unit_shipping_non_taxable_city + "','"
           //+ amazonOrderLineType.per_unit_shipping_non_taxable_county + "','"
           //+ amazonOrderLineType.per_unit_shipping_non_taxable_state + "','"
           //+ amazonOrderLineType.per_unit_shipping_zero_rated_district + "','"
           //+ amazonOrderLineType.per_unit_shipping_zero_rated_city + "','"
           //+ amazonOrderLineType.per_unit_shipping_zero_rated_county + "','"
           //+ amazonOrderLineType.per_unit_shipping_zero_rated_state + "','"
           //+ amazonOrderLineType.per_unit_shipping_tax_collected_district + "','"
           //+ amazonOrderLineType.per_unit_shipping_tax_collected_city + "','"
           //+ amazonOrderLineType.per_unit_shipping_tax_collected_county + "','"
           //+ amazonOrderLineType.per_unit_shipping_tax_collected_state + "','"
           + amazonOrderLineType.item_promotion_discount + "','"
           // + amazonOrderLineType.item_promotion_id + "','"
           + amazonOrderLineType.ship_promotion_discount + "','"
           //+ amazonOrderLineType.ship_promotion_id + "','"
           //+ amazonOrderLineType.delivery_start_date + "','"
           //+ amazonOrderLineType.delivery_end_date + "','"
           //+ amazonOrderLineType.delivery_time_zone + "','"
           + amazonOrderLineType.dataTransferStatus
           + "')";
        }

        public static DataTable GetAmazonShippedOrderInfo(string accountName)
        {
            string sql = "select * from OrderHeader where Reference2 = '' and TrackingNum <> '' and Channel = 'Amazon' and AccountName='" + accountName + "'";
            try
            {
                return SqlHelper.ExecuteDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void BuildUpdateTrackingTableTran(string orderId, string note, string accountName)
        {
            //string sql = "update ShipmentInfo set UploadNote = '" + note + "', IsUpload ='" + status + "', UploadTime='" + (DateTime)SqlDateTime.MinValue + "' where Channel='Amazon' and OrderID ='" + orderId + "'";
            string sql = "update OrderHeader set Reference2='" + System.DateTime.Now + "' where OrderNum='" + orderId + "' and AccountName='" + accountName + "'";
            List<string> sqlList = new List<string>();
            try
            {
                sqlList.Add(sql);
                SqlHelper.ExecuteNonQuery(sqlList, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, orderId + ": BuildUpdateTrackingTableTran: ", orderId + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }
        
        public static void DeleteUncompletedEbayOrder(string orderId)
        {
            string sqlLine = "delete from EbayOrderLine where OrderID ='"+orderId+"'";
            string sqlHeader = "delete from EbayOrderHeader where OrderID = '"+orderId+"'";
            string sqlPaymentTransaction = "delete from EbayPaymentTransaction where OrderID = '"+orderId+"'";
            List<string> sqlList = new List<string>();
            try
            {
                sqlList.Add(sqlLine);
                sqlList.Add(sqlHeader);
                sqlList.Add(sqlPaymentTransaction);
                SqlHelper.ExecuteNonQuery(sqlList, ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, orderId + ": DeleteUncompletedEbayOrder: ", orderId + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }
        public static DataRow GetVisionSkuInfo(string sku)
        {
            try
            {
                string sql = @"select * from pebbledon.dbo.SKUMap where iteza0923 = '" + sku + "' or motovehicleparts = '" + sku + "' or framegeneration = '" + sku + "' or kalegend = '" + sku + "' or beautyequation = '" + sku + "' or kadepot = '" + sku + "'";
                try
                {
                    return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["marketplace"]);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch(Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, "GetVisionSkuInfo: ", sku + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }

    }
}
