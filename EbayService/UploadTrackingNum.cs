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
    public class UploadTrackingNum
    {
        public static void UploadSingleTrackingNum(string trackingNum, string orderNum, string carrier, string accountName, string token)
        {
            string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
            string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
            string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
            string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
            int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

            ApiContext context = new ApiContext();
            context.ApiCredential.eBayToken = token;
            context.SoapApiServerUrl = "https://api.ebay.com/wsapi";
            context.ApiLogManager = new ApiLogManager();
            context.ApiLogManager.ApiLoggerList.Add(new FileLogger("log.txt", false, false, true));
            context.ApiLogManager.EnableLogging = true;
            context.Version = "861";
            context.Site = SiteCodeType.US;
            CompleteSaleCall completeSaleCall = new CompleteSaleCall(context);
            completeSaleCall.OrderID = orderNum;
            completeSaleCall.Shipped = true;
            completeSaleCall.Shipment = new ShipmentType();
            completeSaleCall.Shipment.ShipmentTrackingDetails = new ShipmentTrackingDetailsTypeCollection();
            ShipmentTrackingDetailsType shpmnt = new ShipmentTrackingDetailsType();
            shpmnt.ShipmentTrackingNumber = trackingNum;
            shpmnt.ShippingCarrierUsed = carrier;
            completeSaleCall.Shipment.ShipmentTrackingDetails.Add(shpmnt);
            try
            {
                completeSaleCall.Execute();
                try
                {
                    Db.Db.UpdateShipmentInfoDt(orderNum,accountName,System.DateTime.Now);
                }
                catch (Exception ex)
                {
                    ExceptionUtility exceptionUtility = new ExceptionUtility();
                    exceptionUtility.CatchMethod(ex, accountName + ": completeSaleCall " + orderNum, ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                }
            }
            catch(Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex,  accountName+ ": completeSaleCall " + orderNum, ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
            }
        }
    }
}
