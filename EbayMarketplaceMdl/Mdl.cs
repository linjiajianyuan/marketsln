using EbayComm;
using EbayService;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayMarketplaceMdl
{
    public class Mdl
    {
        public static void UploadTrackingNum()
        {
            DataTable sellerAccountDt = Db.Db.GetEbayDeveloperInfo();
            foreach (DataRow sellerAccountDr in sellerAccountDt.Rows)
            {
                string accountName = sellerAccountDr["AccountName"].ToString();
                try
                {
                    string token = sellerAccountDr["Token"].ToString();
                    List<string> exceptList = ConfigurationManager.AppSettings["exceptList"].Split(',').ToList();
                    List<string> exceptSKUList = ConfigurationManager.AppSettings["exceptSKUList"].Split(',').ToList();
                    if (exceptList.Contains(accountName))
                    {
                        continue;
                    }
                    else
                    {
                        DataTable shippedInfoDt = Db.Db.GetEbayShippedOrderInfo(accountName);
                        foreach (DataRow dr in shippedInfoDt.Rows)
                        {
                            string trackingNum = dr["TrackingNum"].ToString();
                            string orderNum = dr["OrderNum"].ToString();
                            string carrier = "";
                            if(dr["ShippingCarrier"]!=null || dr["ShippingCarrier"].ToString().Trim().ToUpper() == "FEDEX")
                            {
                                carrier = "Fedex";
                            }
                            else
                            {
                                carrier = ConfigurationManager.AppSettings["defaultCarrier"];
                            }
                            
                            EbayService.UploadTrackingNum.UploadSingleTrackingNum(trackingNum, orderNum, carrier, accountName, token);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ExceptionUtility.GetCustomizeException(ex);
                }
            }
        }



        public static void AddOrderToDb()
        {
            DataTable sellerAccountDt = Db.Db.GetEbayDeveloperInfo();
            foreach (DataRow sellerAccountDr in sellerAccountDt.Rows)
            {
                try
                {
                    string token = sellerAccountDr["Token"].ToString();
                    string accountName = sellerAccountDr["AccountName"].ToString();
                    List<string> exceptList = ConfigurationManager.AppSettings["exceptList"].Split(',').ToList();
                    if (exceptList.Contains(accountName))
                    {
                        continue;
                    }
                    else
                    {
                        List<EbayOrderType> orderList = GetOrder.GetOrderFromEbay(token, accountName);
                        foreach (EbayOrderType ebayOrderType in orderList)
                        {
                            try
                            {
                                Db.Db.AddEbayOrderToDb(ebayOrderType);
                            }
                            catch (Exception)
                            {
                                continue;// continue if order already exist in DB
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ExceptionUtility.GetCustomizeException(ex);
                }
            }
        }
    }
}
