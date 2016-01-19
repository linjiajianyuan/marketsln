using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayService
{
    public class GetMyEbaySelling
    {
        private static DataTable CreateSellingInventoryDt()
        {
            DataTable sellingInventoryDt = new DataTable();
            sellingInventoryDt.Columns.Add("ItemID", typeof(String));
            sellingInventoryDt.Columns.Add("SKU", typeof(String));
            sellingInventoryDt.Columns.Add("Name", typeof(String));
            sellingInventoryDt.Columns.Add("Qty", typeof(int));
            sellingInventoryDt.Columns.Add("StartPrice", typeof(double));
            sellingInventoryDt.Columns.Add("SoldQty", typeof(int));
            sellingInventoryDt.Columns.Add("QuantityAvailable", typeof(int));
            sellingInventoryDt.Columns.Add("IsVariation",typeof(int));
            return sellingInventoryDt;
        }

        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

        public static DataTable GetMySelling(string accoutName, string token, List<string> exceptSKUList)
        {
            DataTable sellingInventoryDt = CreateSellingInventoryDt();
            ApiContext context = new ApiContext();
            context.ApiCredential.eBayToken = token;
            context.SoapApiServerUrl = "https://api.ebay.com/wsapi";
            context.ApiLogManager = new ApiLogManager();
            //context.ApiLogManager.ApiLoggerList.Add(new FileLogger("log.txt", false, false, true));
            context.ApiLogManager.EnableLogging = true;
            context.Version = "861";
            context.Site = SiteCodeType.US;

            GetMyeBaySellingCall getMyeBaySellingCall = new GetMyeBaySellingCall(context);
            getMyeBaySellingCall.ActiveList = new ItemListCustomizationType();
            getMyeBaySellingCall.ActiveList.Include = true;

            int pageNumber = 1;
            while (pageNumber < 100)
            {
                try
                {
                    PaginationType pagination = new PaginationType();
                    pagination.EntriesPerPage = 200;
                    getMyeBaySellingCall.ActiveList.Pagination = pagination;
                    getMyeBaySellingCall.ActiveList.Sort = ItemSortTypeCodeType.QuantityAvailable;
                    pagination.PageNumber = pageNumber;
                    getMyeBaySellingCall.Execute();
                    int totalPageNumber = getMyeBaySellingCall.ActiveListReturn.PaginationResult.TotalNumberOfPages;
                    if (pageNumber > totalPageNumber)
                    {
                        break;
                    }
                    else
                    {
                        if (getMyeBaySellingCall.ApiResponse.Ack != AckCodeType.Failure)
                        {
                            foreach (ItemType itemType in getMyeBaySellingCall.ActiveListReturn.ItemArray)
                            {
                                try
                                {
                                    string productId = itemType.ItemID;
                                    string productSku = itemType.SKU;
                                    string productName = itemType.Title;
                                    if (exceptSKUList.Contains(productSku))
                                    {
                                        continue;
                                    }
                                    else if (itemType.Variations == null)
                                    {
                                        //continue;
                                        int quantityAvailable = itemType.QuantityAvailable;
                                        int quantity = itemType.Quantity;
                                        int soldQty = quantity - quantityAvailable;
                                        double startPrice = itemType.StartPrice.Value;
                                        if (quantity == 1 || quantityAvailable == 1)
                                        {
                                            DataRow sellingInventoryDr = sellingInventoryDt.NewRow();
                                            sellingInventoryDr["ItemID"] = productId;
                                            sellingInventoryDr["SKU"] = itemType.SKU;
                                            sellingInventoryDr["Name"] = productName;
                                            sellingInventoryDr["Qty"] = quantity;
                                            sellingInventoryDr["StartPrice"] = startPrice;
                                            sellingInventoryDr["SoldQty"] = soldQty;
                                            sellingInventoryDr["QuantityAvailable"] = quantityAvailable;
                                            sellingInventoryDr["IsVariation"] = 0;
                                            sellingInventoryDt.Rows.Add(sellingInventoryDr);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        foreach (VariationType simpleType in itemType.Variations.Variation)
                                        {
                                            productSku = simpleType.SKU;
                                            int quantity = simpleType.Quantity;
                                            double startPrice = simpleType.StartPrice.Value;
                                            if (quantity == 1)
                                            {
                                                DataRow sellingInventoryDr = sellingInventoryDt.NewRow();
                                                sellingInventoryDr["ItemID"] = productId;
                                                sellingInventoryDr["SKU"] = productSku;
                                                sellingInventoryDr["Name"] = productName;
                                                sellingInventoryDr["Qty"] = simpleType.Quantity;
                                                sellingInventoryDr["StartPrice"] = startPrice;
                                                sellingInventoryDr["SoldQty"] = -1;
                                                sellingInventoryDr["QuantityAvailable"] = -1;
                                                sellingInventoryDr["IsVariation"] = 1;
                                                sellingInventoryDt.Rows.Add(sellingInventoryDr);
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExceptionUtility exceptionUtility = new ExceptionUtility();
                                    exceptionUtility.CatchMethod(ex, "GetMyEbaySelling foreach", accoutName+" "+ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                    continue;
                                }
                            }
                        }
                    }
                    pageNumber = pageNumber + 1;
                }
                catch (Exception ex)
                {
                    ExceptionUtility exceptionUtility = new ExceptionUtility();
                    exceptionUtility.CatchMethod(ex, "GetMyEbaySelling", accoutName+" "+ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                }
            }

            return sellingInventoryDt;
        }
    }
}
