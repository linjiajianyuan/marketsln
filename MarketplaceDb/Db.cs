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
        
        public static DataTable GetAllChannel()
        {
            string sql = "select distinct Channel from OrderHeader";
            try
            {
                return SqlHelper.GetDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable GetAllAccount()
        {
            string sql = "select distinct AccountName from OrderHeader";
            try
            {
                return SqlHelper.GetDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable GetOrderInfoDt(int shipped, string startDate, string endDate, string ebayItemNum, string buyerUserId, string name, string email)
        {
            // 0 = not shipped; 1 = shipped; 2 = both
            string sql = "";
            string addCondition = "";
            if(ebayItemNum!="")
            {
                addCondition="l.ChannelItemNum='"+ebayItemNum+"' and ";
            }
            if(buyerUserId !="")
            {
                addCondition = addCondition + "h.BuyerUserID='" + buyerUserId + "' and ";
            }
            if (name != "")
            {
                addCondition = addCondition + "h.ShipName='" + name + "' and ";
            }
            if (email != "")
            {
                addCondition = addCondition + "h.ShipEmail='" + email + "' and ";
            }

            if (shipped==0)
            {
                sql = "select h.OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipCountry,ShipZip,ShipPhone,ShippedDate,h.EnterDate,h.Channel,h.TrackingNum from OrderHeader h where " + addCondition + " ShippedDate='1753-01-01 00:00:00.000' and OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
            }
            else if (shipped ==1)
            {
                sql = "select h.OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipCountry,ShipZip,ShipPhone,ShippedDate,h.EnterDate,h.Channel,h.TrackingNum from OrderHeader h where " + addCondition + " ShippedDate <>'1753-01-01 00:00:00.000' and OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
            }
            else
            {
                sql = "select h.OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipCountry,ShipZip,ShipPhone,ShippedDate,h.EnterDate,h.Channel,h.TrackingNum from OrderHeader h where " + addCondition + " OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
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
        public static void SaveNoteToDb(string orderNum, string channel, string note)
        {
            string updateSql = "update OrderHeader set Note='"+note+"' where Channel='"+ channel + "' and OrderNum='"+ orderNum + "'" ;
            try
            {
                SqlHelper.ExecuteNonQuery(updateSql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch(Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, orderNum + ": SaveNoteToDb ", orderNum + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }
        public static void SaveSingleShipmentInfo(string orderNum,string accountName,string channel, string trackingNum, string carrier)
        {
            List<string> sqlList = new List<string>();
            string sqlInsert = @"insert into  ShipmentInfo 
                    (TrackingNum, AccountName, OrderNum, Channel, Cost, Reference1,LabelCommand) 
             values ('" + trackingNum + "','" +accountName + "','" + orderNum + "','" + channel + "','" + 0 + "','" + carrier + "','" + "" + "')";
            string sqlUpdate = "update OrderHeader set TrackingNum='" + trackingNum + "' ShippedDate ='" + System.DateTime.Now + "' where OrderNum ='" + orderNum + "' and Channel='" + channel + "'";
            try
            {
                sqlList.Add(sqlInsert);
                sqlList.Add(sqlUpdate);
                SqlHelper.ExecuteNonQuery(sqlList, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch(Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, orderNum + ": SaveShipmentInfo ", orderNum + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }

        public static DataRow CheckDuplicatedCustomizedWeight(string customizedInfo)
        {
            string sql = "select * from CustomizedWeight where ProductInfo='" + customizedInfo + "'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SaveCustomizedWeight(string customizedInfo, int customizedWeight)
        {
            try
            {
                string sql = "insert into CustomizedWeight (ProductInfo, CustomizedWeight) values ('" + customizedInfo + "','" + customizedWeight + "')";
                SqlHelper.ExecuteNonQuery(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, customizedInfo + ": SaveCustomizedWeight ", customizedWeight + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }


        }

        public static void SaveShipmentInfo(string orderNum, string accountName, string channel,string trackingNum,string reference, decimal cost, string nativeCommand)
        {
            try
            {
                List<string> sqlList = new List<string>();
                string sqlInsert = @"insert into  ShipmentInfo 
                    (TrackingNum, AccountName, OrderNum, Channel, Cost, Reference1,LabelCommand) 
             values ('" + trackingNum + "','" +accountName + "','" + orderNum + "','" + channel + "','" + cost + "','" + reference + "','" + nativeCommand + "')";

                string sqlUpdate = "update OrderHeader set TrackingNum='"+ trackingNum + "', ShippedDate ='"+System.DateTime.Now+"' where OrderNum ='"+orderNum+"' and Channel='"+channel+"'";
                sqlList.Add(sqlInsert);
                sqlList.Add(sqlUpdate);
                SqlHelper.ExecuteNonQuery(sqlList, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, orderNum + ": SaveShipmentInfo ", orderNum + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }

        public static DataRow GetShipmentInfoByOrder(string orderNum,string channel )
        {
            string sql = "select * from ShipmentInfo where OrderNum ='"+orderNum +"' and Channel='"+channel+"'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
