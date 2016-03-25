using EbayComm;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            string sql="select OrderID from EbayOrderHeader where OrderID='"+orderId+"'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["marketplace"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable GetShippedOrderInfo(string accountName)
        {
            string sql = "select * from ShipmentInfo where IsUpload=0 and AccountName='" + accountName + "'";
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
            string sql = "update ShipmentInfo set isUpload = 1, UploadTime='"+uploadDate+"' where OrderNum='"+orderNum+"' and AccountName='"+accountName+"'";
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
    }
}
