using MarketplaceWinForm.DhlApi;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceWinForm
{
    public class ProcessShippingLabel
    {
        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

        public static Dictionary<string, string> GetInternationalLabel(string orderNum, string channel)
        {
            Dictionary<string, string> shippingLabelDic = new Dictionary<string, string>();
            DataRow orderHeaderInfoDr = MarketplaceDb.Db.GetOrderHeaderDrByOrderNum(orderNum, channel);
            DataTable orderRowInfoDt = MarketplaceDb.Db.GetOrderLineDtByOrderNum(orderNum, channel);
            int weightOz = 0;
            string reference = "";
            List<string> infoList = new List<string>();
            foreach (DataRow dr in orderRowInfoDt.Rows)
            {
                string sku = dr["SKU"].ToString();
                DataRow itemDr = MarketplaceDb.Db.GetItemInfoBySKU(sku);
                int tempWeightOz = ConvertUtility.ToInt(itemDr["Weight"]);
                weightOz = weightOz + tempWeightOz;
                reference = reference + sku + "x" + ConvertUtility.ToInt(dr["Quantity"]) + "|";
            }
            infoList.Add(reference);
            InternationalLabelRequest intelLabelReq = new InternationalLabelRequest();
            Credentials dhlCred = new Credentials();
            dhlCred.Username = ConfigurationManager.AppSettings["firstMileAccount"];
            dhlCred.Password = ConfigurationManager.AppSettings["firstMilePassword"];
            intelLabelReq.UserCredentials = dhlCred;
            intelLabelReq.WeightLbs = weightOz/16;
            Address shipToAddress = new Address();
            shipToAddress.Name = orderHeaderInfoDr["ShipName"].ToString();
            shipToAddress.Address1 = orderHeaderInfoDr["ShipAddress1"].ToString();
            shipToAddress.Address2 = orderHeaderInfoDr["ShipAddress2"].ToString();
            shipToAddress.City = orderHeaderInfoDr["ShipCity"].ToString();
            shipToAddress.Region = orderHeaderInfoDr["ShipState"].ToString();
            shipToAddress.RegionCode = orderHeaderInfoDr["ShipZip"].ToString();
            shipToAddress.Country = orderHeaderInfoDr["ShipCountry"].ToString();
            shipToAddress.CountryCode = orderHeaderInfoDr["ShipCountry"].ToString();
            shipToAddress.PhoneNumber = orderHeaderInfoDr["ShipPhone"].ToString();
            intelLabelReq.ShipToAddress = shipToAddress;
            Address fromAddress = new Address();
            fromAddress.Name = ConfigurationManager.AppSettings["FromName"];
            fromAddress.Address1 = ConfigurationManager.AppSettings["FromAddress1"];
            fromAddress.Address2 = ConfigurationManager.AppSettings["FromAddress2"];
            fromAddress.City = ConfigurationManager.AppSettings["FromCity"];
            fromAddress.Region = ConfigurationManager.AppSettings["FromRegion"];
            fromAddress.RegionCode = ConfigurationManager.AppSettings["FromRegionCode"];
            fromAddress.Country = ConfigurationManager.AppSettings["FromCountry"];
            fromAddress.CountryCode = ConfigurationManager.AppSettings["FromCountryCode"];
            fromAddress.PhoneNumber = ConfigurationManager.AppSettings["FromPhoneNum"];
            intelLabelReq.FromAddress = fromAddress;
            intelLabelReq.IsoCurrencyCode = "USD";
            string shipCountry = orderHeaderInfoDr["ShipCountry"].ToString();
            List<string> dhlGmParcelDirectCountryList = ConfigurationManager.AppSettings["DHLGMParcelDirectCountryList"].Split(',').ToList();
            if(weightOz >= 704)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.ErrorWarningMethod("Internation Shipping Over Max Weight: " + orderNum, "Internation Shipping Over Max Weight: " + orderNum, senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                shippingLabelDic.Add("EmailSent", "EmailSent");
                return shippingLabelDic;
            }
            else
            {
                if (dhlGmParcelDirectCountryList.Contains(shipCountry))
                {
                    intelLabelReq.LabelType = LabelType.DhlGMParcelDirect;
                }
                else
                {
                    intelLabelReq.LabelType = LabelType.DhlGMParcelPriority;
                }
            }
            intelLabelReq.TaxIdNumber = ConfigurationManager.AppSettings["TaxId"];
            intelLabelReq.IsTest = false;
            List<CustomsItem> customsItemList = new List<CustomsItem>();
            CustomsItem customsItem = new CustomsItem();
            customsItem.CountryOfOrigin = "CN";
            //etc;
            customsItemList.Add(customsItem);
            intelLabelReq.CustomsItems = customsItemList.ToArray();
            intelLabelReq.OrderValue= ConvertUtility.ToDecimal(orderHeaderInfoDr["Subtotal"]);
            intelLabelReq.PackageDescription = ConfigurationManager.AppSettings["PackageDescription"];
            intelLabelReq.GetRate = true;
            intelLabelReq.OriginZipCode = ConfigurationManager.AppSettings["FromRegionCode"];
            DhlWebApiClient client = new DhlWebApiClient("DhlApi", "http://ifslabelservice.com/api/DhlApi.svc");
            InternationalLabelResponse intelLabelRes = client.GetInternationalLabel(intelLabelReq);
            string[] labelImagesBase64 = intelLabelRes.LabelImagesBase64;

            string trackingNum = intelLabelRes.TrackingNumber;
            InternationalRateResponse irres = intelLabelRes.RateInfo;
            decimal cost = irres.Cost;
            string nativeCommand = labelImagesBase64.ToString();
            shippingLabelDic.Add(trackingNum, nativeCommand);
            MarketplaceDb.Db.SaveShipmentInfo(orderNum, channel, trackingNum, reference, cost, nativeCommand);
            return shippingLabelDic;
        }

        public static Dictionary<string, string> GetDomesticLabel(string orderNum,string channel)
        {
            Dictionary<string, string> shippingLabelDic = new Dictionary<string, string>();
            DataRow orderHeaderInfoDr = MarketplaceDb.Db.GetOrderHeaderDrByOrderNum(orderNum, channel);
            DataTable orderRowInfoDt = MarketplaceDb.Db.GetOrderLineDtByOrderNum(orderNum, channel);
            int weightOz = 0;
            string reference = "";
            List<string> infoList = new List<string>();
            
            foreach (DataRow dr in orderRowInfoDt.Rows)
            {
                string sku = dr["SKU"].ToString();
                DataRow itemDr = MarketplaceDb.Db.GetItemInfoBySKU(sku);
                int tempWeightOz = ConvertUtility.ToInt(itemDr["Weight"]);
                weightOz = weightOz + tempWeightOz;
                reference = reference + sku + "x" + ConvertUtility.ToInt(dr["Quantity"]) + "|";
            }
            infoList.Add(reference);
            DhlLabelRequest dhlLabelReq = new DhlLabelRequest();
            Credentials dhlCred = new Credentials();
            dhlCred.Username = ConfigurationManager.AppSettings["firstMileAccount"];
            dhlCred.Password = ConfigurationManager.AppSettings["firstMilePassword"];
            dhlLabelReq.UserCredentials = dhlCred;
            dhlLabelReq.WeightOz = weightOz;
            Address shipToAddress = new Address();
            shipToAddress.Name = orderHeaderInfoDr["ShipName"].ToString();
            shipToAddress.Address1 = orderHeaderInfoDr["ShipAddress1"].ToString();
            shipToAddress.Address2 = orderHeaderInfoDr["ShipAddress2"].ToString();
            shipToAddress.City = orderHeaderInfoDr["ShipCity"].ToString();
            shipToAddress.Region = orderHeaderInfoDr["ShipState"].ToString();
            shipToAddress.RegionCode = orderHeaderInfoDr["ShipZip"].ToString();
            shipToAddress.Country = orderHeaderInfoDr["ShipCountry"].ToString();
            shipToAddress.CountryCode = orderHeaderInfoDr["ShipCountry"].ToString();
            shipToAddress.PhoneNumber = orderHeaderInfoDr["ShipPhone"].ToString();
            dhlLabelReq.ShipToAddress = shipToAddress;
            Address fromAddress = new Address();
            fromAddress.Name = ConfigurationManager.AppSettings["FromName"];
            fromAddress.Address1 = ConfigurationManager.AppSettings["FromAddress1"];
            fromAddress.Address2 = ConfigurationManager.AppSettings["FromAddress2"];
            fromAddress.City = ConfigurationManager.AppSettings["FromCity"];
            fromAddress.Region = ConfigurationManager.AppSettings["FromRegion"];
            fromAddress.RegionCode = ConfigurationManager.AppSettings["FromRegionCode"];
            fromAddress.Country = ConfigurationManager.AppSettings["FromCountry"];
            fromAddress.CountryCode = ConfigurationManager.AppSettings["FromCountryCode"];
            fromAddress.PhoneNumber = ConfigurationManager.AppSettings["FromPhoneNum"];
            dhlLabelReq.FromAddress = fromAddress;
            dhlLabelReq.IsTest = false;
            string shipCountry = orderHeaderInfoDr["ShipCountry"].ToString();

            if (weightOz >= 16)
            {
                dhlLabelReq.LabelType = DomesticLabelType.DhlSmParcelPlusGround;
            }
            else
            {
                if(weightOz>240)
                {
                    ExceptionUtility exceptionUtility = new ExceptionUtility();
                    exceptionUtility.ErrorWarningMethod("Domestic Shipping Over Max Weight: " + orderNum, "Domestic Shipping Over Max Weight: " + orderNum, senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                    shippingLabelDic.Add("EmailSent", "EmailSent");
                    return shippingLabelDic;
                }
                else
                {
                    dhlLabelReq.LabelType = DomesticLabelType.DhlSmParcelsGround;
                } 
            }
            dhlLabelReq.LabelImageFormat = ImageFormat.ZPL;
            dhlLabelReq.LabelSize = LabelSize.Label4X6;
            dhlLabelReq.Reference1 = orderHeaderInfoDr["OrderNum"].ToString();// order number
            dhlLabelReq.DocTabValues = infoList.ToArray();
            dhlLabelReq.GetRate = true;
            dhlLabelReq.OriginZipCode = ConfigurationManager.AppSettings["FromRegionCode"];
            DhlWebApiClient client = new DhlWebApiClient("DhlApi", "http://ifslabelservice.com/api/DhlApi.svc");
            DhlLabelResponse dhlLabelRes = client.GetLabel(dhlLabelReq);
            string nativeCommand = dhlLabelRes.NativePrinterCommand;
            string trackingNum = dhlLabelRes.TrackingNumber;
            DomesticRateResponse drres = dhlLabelRes.RateInfo;
            decimal cost = drres.Cost;
            shippingLabelDic.Add(trackingNum, nativeCommand);
            MarketplaceDb.Db.SaveShipmentInfo(orderNum,channel,trackingNum, reference, cost, nativeCommand);
            return shippingLabelDic;
        }
    }
}
