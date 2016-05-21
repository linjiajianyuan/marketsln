using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMarketplaceMdl
{
    public class Mdl
    {
        public static void GetAmazonOrder()
        {
            DataTable sellerAccountDt = Db.Db.GetAmazonDeveloperInfo();
            foreach(DataRow sellerAccountDr in sellerAccountDt.Rows)
            {
                try
                {
                    string merchantId = ConfigurationManager.AppSettings["SellerID"];
                    string marketplaceId = ConfigurationManager.AppSettings["MarketplaceID"];
                    string accessKeyId = ConfigurationManager.AppSettings["AccessKeyID"];
                    string secretAccessKey = ConfigurationManager.AppSettings["Token"];
                    string accountName = ConfigurationManager.AppSettings["AccountName"];
                    string channel = ConfigurationManager.AppSettings["Channel"];
                    DataSet amazonOrderHeaderDs = new DataSet();
                    amazonOrderHeaderDs = AmazonService.ListOrders.ListAmazonOrderHeader(accountName, merchantId, marketplaceId, accessKeyId, secretAccessKey);
                    DataTable amazonOrderHeaderListOrdersResultDt = amazonOrderHeaderDs.Tables["ListOrdersResult"];
                    // CHECK IF FIRST CALL WITH NEXT TOKEN
                    string amazonOrderHeaderNextToken = "";
                    foreach (DataRow amazonOrderHeaderListOrdersResultDr in amazonOrderHeaderListOrdersResultDt.Rows)
                    {
                        DataColumnCollection columns = amazonOrderHeaderListOrdersResultDt.Columns;
                        if (columns.Contains("NextToken"))
                        {
                            amazonOrderHeaderNextToken = amazonOrderHeaderListOrdersResultDr["NextToken"].ToString();
                            //Console.WriteLine(amazonOrderHeaderNextToken);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    // END
                    // use NEXT TOKEN to get additional orders
                    while (amazonOrderHeaderNextToken != "")
                    {
                        try
                        {
                            DataSet amazonOrderHeaderNextTokenDs = AmazonService.ListOrders.ListAmazonOrderHeaderByNextToken(amazonOrderHeaderNextToken, merchantId, marketplaceId, accessKeyId, secretAccessKey);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
