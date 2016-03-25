using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayInventoryMdl
{
    public class Mdl
    {
        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);
        public static void UpdateEbayInventory()
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
                        DataTable adjustInventoryDt = EbayService.GetMyEbaySelling.GetMySelling(accountName, token, exceptSKUList);
                        foreach (DataRow adjustInventoryDr in adjustInventoryDt.Rows)
                        {
                            string itemId = adjustInventoryDr["ItemID"].ToString();
                            string sku = adjustInventoryDr["SKU"].ToString();
                            string name = adjustInventoryDr["Name"].ToString();
                            try
                            {
                                int qty = ConvertUtility.ToInt(adjustInventoryDr["qty"]);
                                double startPrice = ConvertUtility.ToDouble(adjustInventoryDr["startPrice"]);
                                int soldQty = ConvertUtility.ToInt(adjustInventoryDr["soldQty"]);
                                int quantityAvailable = ConvertUtility.ToInt(adjustInventoryDr["quantityAvailable"]);
                                int isVariation = ConvertUtility.ToInt(adjustInventoryDr["isVariation"]);
                                EbayService.UpdateInventoryByReviseFixedPriceItem.UpdateInventory(accountName, token, itemId, qty, sku, isVariation, soldQty, startPrice);
                            }
                            catch (Exception ex)
                            {
                                ExceptionUtility exceptionUtility = new ExceptionUtility();
                                exceptionUtility.CatchMethod(ex, "UpdateInventoryByReviseFixedPriceItem ", accountName + ":" + itemId + "(" + sku + ")" + " " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                continue;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility exceptionUtility = new ExceptionUtility();
                    exceptionUtility.CatchMethod(ex, "Filter Account ", accountName + " " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                    continue;
                }
               
            }
        }
        
    }
}
