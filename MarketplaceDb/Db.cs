using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceDb
{
    public class Db
    {
        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

        public static DataTable GetOrderInfoDt(int shipped, string startDate, string endDate)
        {
            // 0 = not shipped; 1 = shipped; 2 = both
            string sql = "";
            if (shipped==0)
            {
                sql = "select OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipZip,ShipPhone,ShippedDate,EnterDate,Channel,TrackingNum from OrderHeader where ShippedDate='1753-01-01' and OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
            }
            else if (shipped ==1)
            {
                sql = "select OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipZip,ShipPhone,ShippedDate,EnterDate,Channel,TrackingNum from OrderHeader where ShippedDate <>'1753-01-01' and OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
            }
            else
            {
                sql = "select OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipZip,ShipPhone,ShippedDate,EnterDate,Channel,TrackingNum from OrderHeader where OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
            }
            try
            {
                return SqlHelper.ExecuteDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataRow GetOrderHeaderDrByOrderNum(string orderNum, string channel)
        {
            string sql = "select * from OrderHeader where Channel = '"+channel+"' and OrderNum = '"+ orderNum + "'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataRow GetItemInfoBySKU(string sku)
        {
            string sql = "select * from Item where SKU = '" + sku + "'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable GetOrderLineDtByOrderNum(string orderNum,string channel)
        {
            DataTable dt = new DataTable();
            string sql = "select LineNum,ChannelItemNum,ItemNum, Quantity, ItemDesc,ShippingMethod,TrackingNum,TrackingNumUploadDate,TransactionPrice from OrderLine where Channel = '" + channel + "' and OrderNum = '" + orderNum + "'";
            try
            {
                return SqlHelper.ExecuteDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void SaveShipmentInfo(string orderNum, string channel,string trackingNum,string reference, decimal cost)
        {
            try
            {
                string sql = @"insert into  ShipmentInfo 
                    (TrackingNum, OrderNum, Channel, Cost, Reference1) 
             values ('" + trackingNum + "','" + orderNum + "','" + channel + "','" + cost + "','" + reference + "')";
                SqlHelper.ExecuteNonQuery(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }

            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, orderNum + ": SaveShipmentInfo ", orderNum + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }
    }
}
