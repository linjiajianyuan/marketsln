﻿using PebbledonUtilityLib;
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

        public static DataTable GetMonthlySales()
        {
            string sql = @"SELECT YEAR(OrderDate) as SalesYear,MONTH(OrderDate) as SalesMonth,SUM(Total) AS TotalSales
                            FROM Pebbledon.dbo.OrderHeader
                            GROUP BY YEAR(OrderDate), MONTH(OrderDate)
                            ORDER BY YEAR(OrderDate), MONTH(OrderDate)";
            try
            {
                return SqlHelper.GetDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable GetMonthlySalesByAccount()
        {
            string sql = @"SELECT YEAR(OrderDate) as SalesYear,MONTH(OrderDate) as SalesMonth,SUM(Total) AS TotalSales, Channel, AccountName
                            FROM Pebbledon.dbo.OrderHeader
                            GROUP BY YEAR(OrderDate), MONTH(OrderDate), Channel, AccountName
                            ORDER BY YEAR(OrderDate), MONTH(OrderDate), Channel, AccountName";
            try
            {
                return SqlHelper.GetDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable GetMonthlySalesByChannel()
        {
            string sql = @"SELECT YEAR(OrderDate) as SalesYear,MONTH(OrderDate) as SalesMonth,SUM(Total) AS TotalSales, Channel
                            FROM Pebbledon.dbo.OrderHeader
                            GROUP BY YEAR(OrderDate), MONTH(OrderDate), Channel
                            ORDER BY YEAR(OrderDate), MONTH(OrderDate), Channel";
            try
            {
                return SqlHelper.GetDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
                sql = "select h.OrderNum,AccountName,OrderDate,BuyerUserID,Note,LEFT([AddressType],1) AS AddressType,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipCountry,ShipZip,ShipPhone,ShippedDate,h.EnterDate,h.Channel,h.TrackingNum from OrderHeader h left join OrderLine l ON h.OrderNum = l.OrderNum where " + addCondition + " ShippedDate='1753-01-01 00:00:00.000' and OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
            }
            else if (shipped ==1)
            {
                sql = "select h.OrderNum,AccountName,OrderDate,BuyerUserID,Note,LEFT([AddressType],1) AS AddressType,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipCountry,ShipZip,ShipPhone,ShippedDate,h.EnterDate,h.Channel,h.TrackingNum from OrderHeader h left join OrderLine l ON h.OrderNum = l.OrderNum where " + addCondition + " ShippedDate <>'1753-01-01 00:00:00.000' and OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
            }
            else
            {
                sql = "select h.OrderNum,AccountName,OrderDate,BuyerUserID,Note,LEFT([AddressType],1) AS AddressType,ShipName,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipCountry,ShipZip,ShipPhone,ShippedDate,h.EnterDate,h.Channel,h.TrackingNum from OrderHeader h left join OrderLine l ON h.OrderNum = l.OrderNum where " + addCondition + " OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "' order by OrderDate desc";
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
            note = note.Replace("'","''");
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
        public static void CancelOrder(string orderNum, string accountName, string channel)
        {
            string sqlUpdate = @"update OrderHeader set TrackingNum='CANCELLED ORDER', Reference2 = 'CANCELLED ORDER', ShippedDate ='2000-01-01 00:00:00.000'
                                 where OrderNum ='" + orderNum + "' and Channel='"
                               + channel + "'";
            try
            {
                SqlHelper.ExecuteNonQuery(sqlUpdate, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, orderNum + ": CancelOrder ", orderNum + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }

        public static void SaveSingleShipmentInfo(string orderNum,string accountName,string channel, string trackingNum, string carrier)
        {
            string sqlUpdate = @"update OrderHeader set TrackingNum='" + trackingNum + "', ShippedDate ='" + System.DateTime.Now +"', ShippingCarrier='"+carrier
                               + "' where OrderNum ='" + orderNum + "' and Channel='" 
                               + channel + "'";
            try
            {
                SqlHelper.ExecuteNonQuery(sqlUpdate, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch(Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, orderNum + ": SaveShipmentInfo ", orderNum + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
            /*List<string> sqlList = new List<string>();
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
            }*/
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

        public static void SaveShipmentInfo(string orderNum, string accountName, string channel,string trackingNum,string reference, string reference2, decimal cost, string nativeCommand)
        {
            try
            {
                List<string> sqlList = new List<string>();
                string sqlInsert = @"insert into  ShipmentInfo 
                    (TrackingNum, AccountName, OrderNum, Channel, Cost, Reference1,Reference2, LabelCommand) 
             values ('" + trackingNum + "','" +accountName + "','" + orderNum + "','" + channel + "','" + cost + "','" + reference + "','" + reference2 + "','" + nativeCommand + "')";

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
        public static DataRow GetSKUMapColumnName()
        {
            string sql = @"Select Stuff(
                                (
                                Select ',' + C.COLUMN_NAME
                                From INFORMATION_SCHEMA.COLUMNS As C
                                Where C.TABLE_SCHEMA = T.TABLE_SCHEMA
                                    And C.TABLE_NAME = T.TABLE_NAME
                                Order By C.ORDINAL_POSITION
                                For Xml Path('')
                                ), 1, 2, '') As Columns
                        From INFORMATION_SCHEMA.TABLES As T
                        WHERE TABLE_NAME = 'SKUMap'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataRow CheckIsVendorSKUExist(string vendorSKU)
        {
            string sql = "select * from SKUMap where VendorSKU ='"+ vendorSKU + "'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetShipmentInfo()
        {
            string sql = "select * from ShipmentInfo";
            try
            {
                return SqlHelper.GetDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable GetEndOfDayInfo(string startDateTime, string endDateTime)
        {
            string sql = "select distinct count(TrackingNum) as package,Reference2 from ShipmentInfo where EnterDate>='"+ startDateTime + "' and EnterDate<='"+endDateTime+"' group by Reference2";
            try
            {
                return SqlHelper.GetDataTable(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataRow CheckNewVisionSKU(string visionSku)
        {
            string sql = "select * from SKUMap where VendorSKU ='"+visionSku+"'";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void UpdateVisionReferenceInventory(string visionSku, string visionInventory, string visionNjInventory, string visionTxInventory)
        {
            try
            {
                string sql = "update SKUMap set Reference1 ='" + visionInventory + "',Reference2 ='"+visionNjInventory +"', Reference3 ='"+visionTxInventory+"' where VendorSKU = '" + visionSku + "'";
                SqlHelper.ExecuteNonQuery(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, "UpdateVisionReferenceInventory", ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }
        public static DataRow GetMaxItemID()
        {
            string sql = "  select max([ItemID]) as maxItemID  FROM [Pebbledon].[dbo].[SKUMap]";
            try
            {
                return SqlHelper.GetDataRow(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void InsertNewVisionItem(int maxItemId, string vendorSKU, string visionQty, string visionNjQty, string visionTxQty)
        {
            try
            {
                string sql = @"insert into SKUMap (VendorSKU, ItemID, iteza0923, 
                                    motovehicleparts, framegeneration, kalegend, 
                                    beautyequation,kadepot, Reference1, Reference2, Reference3) 
                            values ('"+ vendorSKU + "','"+maxItemId + "','VIE" + maxItemId
                            + "','VME" + maxItemId + "','VFE" + maxItemId + "','VKA" + maxItemId 
                            + "','VBA" + maxItemId+ "','VKAA" + maxItemId + "','" + visionQty
                            + "','" + visionNjQty + "','" + visionTxQty
                            + "')";
                SqlHelper.ExecuteNonQuery(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, "InsertNewVisionItem", ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }
        public static void FinalUpdateVisionQty()
        {
            try
            {
                string sql = @"
                                 update  aa set aa.qty = convert(int, convert(varchar(max),(select 
				                                case 
			                                when aa.Reference1 like 'USE%' then isnull((select 
												                                case when Reference1 like '' then cast(0 as nvarchar)
												
												                                else Reference1
												                                end as reference1
												                                from  SKUMap where VendorSKU= cast(replace(cast(aa.Reference1 as nvarchar(max)), 'USE ', '')  as nvarchar  )),cast(0 as nvarchar))  
			                                when aa.Reference1 like '' then cast(0 as nvarchar)
			                                else aa.Reference1 
			                                end as test
			                                ))
                                 )
                                 from SKUMap aa";
                SqlHelper.ExecuteNonQuery(sql, ConfigurationManager.AppSettings["pebbledon"]);
            }
            catch(Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, "FinalUpdateVisionQty", ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                throw ExceptionUtility.GetCustomizeException(ex);
            }
        }
    }
}
