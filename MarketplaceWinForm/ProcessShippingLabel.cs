using MarketplaceWinForm.DhlApi;
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
        public static Dictionary<string, string> GetShippingLabel(string orderNum,string channel)
        {
            Dictionary<string, string> shippingLabelDic = new Dictionary<string, string>();
            DataRow orderHeaderInfoDr = MarketplaceDb.Db.GetOrderHeaderDrByOrderNum(orderNum, channel);
            DataTable orderRowInfoDt = MarketplaceDb.Db.GetOrderLineDtByOrderNum(orderNum, channel);
            DhlLabelRequest dhlLabelReq = new DhlLabelRequest();
            Credentials dhlCred = new Credentials();
            dhlCred.Username = ConfigurationManager.AppSettings["firstMileAccount"];
            dhlCred.Password = ConfigurationManager.AppSettings["firstMilePassword"];
            dhlLabelReq.UserCredentials = dhlCred;
            dhlLabelReq.WeightOz = 4;
            Address shipToAddress = new Address();
            shipToAddress.Name = orderHeaderInfoDr[""].ToString();
            shipToAddress.Address1 = orderHeaderInfoDr[""].ToString();
            shipToAddress.City = orderHeaderInfoDr[""].ToString();
            shipToAddress.Region = orderHeaderInfoDr[""].ToString();
            shipToAddress.RegionCode = orderHeaderInfoDr[""].ToString();
            shipToAddress.Country = orderHeaderInfoDr[""].ToString();
            shipToAddress.CountryCode = orderHeaderInfoDr[""].ToString();
            shipToAddress.PhoneNumber = orderHeaderInfoDr[""].ToString();
            dhlLabelReq.ShipToAddress = shipToAddress;
            Address fromAddress = new Address();
            fromAddress.Name = "Jack Ng";
            fromAddress.Address1 = "15046 Nelson Ave #15";
            fromAddress.City = "City Of Industry";
            fromAddress.Region = "CA";
            fromAddress.RegionCode = "90601";
            fromAddress.Country = "US";
            fromAddress.CountryCode = "US";
            fromAddress.PhoneNumber = "6264008832";
            dhlLabelReq.FromAddress = fromAddress;
            dhlLabelReq.IsTest = true;
            dhlLabelReq.LabelType = DomesticLabelType.DhlSmParcelsExpedited;
            dhlLabelReq.LabelImageFormat = ImageFormat.ZPL;
            dhlLabelReq.LabelSize = LabelSize.Label4X6;
            dhlLabelReq.Reference1 = orderHeaderInfoDr[""].ToString();// order number
            List<string> infoList = new List<string>();
            infoList.Add("test1test1test1");
            dhlLabelReq.DocTabValues = infoList.ToArray();
            dhlLabelReq.GetRate = true;
            dhlLabelReq.OriginZipCode = "90601";
            DhlWebApiClient client = new DhlWebApiClient("DhlApi", "http://ifslabelservice.com/api/DhlApi.svc");
            DhlLabelResponse dhlLabelRes = client.GetLabel(dhlLabelReq);
            byte[] labelImage = dhlLabelRes.LabelImage;
            string nativeCommand = dhlLabelRes.NativePrinterCommand;
            string format = dhlLabelRes.LabelImageFormat.ToString();
            string trackingNum = dhlLabelRes.TrackingNumber;
            DomesticRateResponse drres = dhlLabelRes.RateInfo;
            shippingLabelDic.Add(trackingNum, nativeCommand);
            Console.WriteLine("OK");
            return shippingLabelDic;
        }
    }
}
