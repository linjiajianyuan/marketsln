﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMdl
{
    public class Mdl
    {
        public static DataTable GetOrderView(int shipped,string startDate, string endDate, string ebayItemNum, string buyerUserId, string name, string email)
        {
            DataTable dt = new DataTable();
            dt=MarketplaceDb.Db.GetOrderInfoDt(shipped,startDate,endDate, ebayItemNum, buyerUserId, name, email);
            return dt;
        }
        public static string GetSKUMapColumnName()
        {
            DataRow dr = MarketplaceDb.Db.GetSKUMapColumnName();
            string columnName = dr["Columns"].ToString();
            return columnName;
        }
        public static DataRow CheckIsVendorSKUExist(string vendorSKU)
        {
            DataRow dr = MarketplaceDb.Db.CheckIsVendorSKUExist(vendorSKU.ToUpper());
            return dr;
        }
    }
}
