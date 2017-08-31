using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayService
{
    public class UpdateInventoryByReviseFixedPriceItem
    {
        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

        public static void UpdateInventory(string accountName, string token, string itemId, int qty,string sku,int isVariation,int soldQty, double startPrice)
        {
            ApiContext context = new ApiContext();
            context.ApiCredential.eBayToken = token;
            context.SoapApiServerUrl = "https://api.ebay.com/wsapi";
            context.ApiLogManager = new ApiLogManager();
            context.ApiLogManager.ApiLoggerList.Add(new FileLogger("log.txt", false, false, false));
            context.ApiLogManager.EnableLogging = true;
            context.Version = "861";
            context.Site = SiteCodeType.US;
            ReviseFixedPriceItemCall reviseFixedPriceItemCall = new ReviseFixedPriceItemCall(context);
            ItemType item = new ItemType();
            try
            {
                if(isVariation==0)
                {
                    item.ItemID = itemId;
                    item.QuantityAvailable = qty;
                    item.Quantity = qty;
                    //item.StartPrice.Value = startPrice;
                    reviseFixedPriceItemCall.Item = item;
                    reviseFixedPriceItemCall.Execute();
                }
                else
                {
                    item.ItemID = itemId;
                    VariationType simpleType = new VariationType();
                    VariationTypeCollection vtc = new VariationTypeCollection();
                    VariationsType vsType = new VariationsType();
                    
                    simpleType.SKU = sku;
                    simpleType.Quantity = qty;
                    vtc.Add(simpleType);
                    vsType.Variation = vtc;
                    item.Variations = vsType;
                    reviseFixedPriceItemCall.Item = item;
                    reviseFixedPriceItemCall.Execute();
                }

            }
            catch(Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, "UpdateInventoryByReviseFixedPriceItem ", accountName+":"+itemId+"("+sku+")"+" "+ ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
            }
        }
    }
}
