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
        public static DataTable GetOrderInfoDt(int shipped, string startDate, string endDate)
        {
            // 0 = not shipped; 1 = shipped; 2 = both
            DataTable dt = new DataTable();
            string sql = "";
            if (shipped==0)
            {
                sql = "select OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipCity,ShipState,ShipZip,ShipPhone,ShippedDate,EnterDate,Channel from OrderHeader where ShippedDate='1753-01-01' and OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "'";
            }
            else if (shipped ==1)
            {
                sql = "select OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipCity,ShipState,ShipZip,ShipPhone,Note,ShippedDate,EnterDate,Channel from OrderHeader where ShippedDate <>'1753-01-01' and OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "'";
            }
            else
            {
                sql = "select OrderNum,AccountName,OrderDate,BuyerUserID,Note,ShipName,ShipAddress1,ShipCity,ShipState,ShipZip,ShipPhone,Note,ShippedDate,EnterDate,Channel from OrderHeader where OrderDate >='" + startDate + "' and OrderDate <='" + endDate + "'";
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
    }
}
