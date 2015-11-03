using EbayComm;
using EbayService;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayMarketplaceMdl
{
    public class Mdl
    {
        public static void AddOrderToDb()
        {
            DataTable sellerAccountDt = Db.Db.GetEbayDeveloperInfo();
            foreach (DataRow sellerAccountDr in sellerAccountDt.Rows)
            {
                try
                {
                    string token = sellerAccountDr["Token"].ToString();
                    string accountName = sellerAccountDr["AccountName"].ToString();
                    List<EbayOrderType> orderList = GetOrder.GetOrderFromEbay(token, accountName);
                    foreach (EbayOrderType ebayOrderType in orderList)
                    {
                        try
                        {
                            Db.Db.AddEbayOrderToDb(ebayOrderType);
                        }
                        catch (Exception ex)
                        {
                            continue;// continue if there is an ex
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
