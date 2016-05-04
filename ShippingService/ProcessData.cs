using ShippingService.FirstMile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PebbledonUtilityLib;
using System.Configuration;

namespace ShippingService
{
    public class ProcessData
    {
        public static void TestProcessData()
        {



            DhlLabelRequest dhlLabelReq = new DhlLabelRequest();
            Credentials dhlCred = new Credentials();
            dhlCred.Username = "TestUsername";
            dhlCred.Password = "somePassword";
            dhlLabelReq.UserCredentials = dhlCred;
            dhlLabelReq.WeightOz = 4;
            Address shipToAddress = new Address();
            shipToAddress.Name = "James White";
            shipToAddress.Address1 = "15 Foggy Glen Dr";
            shipToAddress.City = "Arden";
            shipToAddress.Region = "NC";
            shipToAddress.RegionCode = "NC";
            shipToAddress.Country = "US";
            shipToAddress.CountryCode = "US";
            shipToAddress.PhoneNumber = "828 674 5026";
            dhlLabelReq.ShipToAddress = shipToAddress;
            Address fromAddress = new Address();
            fromAddress.Name = "Jack Ng";
            fromAddress.Address1 = "15046 Nelson Ave #15";
            fromAddress.City = "City Of Industry";
            fromAddress.Region = "CA";
            fromAddress.RegionCode = "CA";
            fromAddress.Country = "US";
            fromAddress.CountryCode = "US";
            fromAddress.PhoneNumber = "6264008832";
            dhlLabelReq.FromAddress = fromAddress;
            dhlLabelReq.IsTest = true;
            dhlLabelReq.LabelType = DomesticLabelType.DhlSmParcelsExpedited;
            dhlLabelReq.LabelImageFormat = ImageFormat.Gif;
            dhlLabelReq.LabelSize = LabelSize.Label4X6;
            dhlLabelReq.Reference1 = "testSKU";
            string endPointName = ConfigurationManager.AppSettings["pebbledon"];
            DhlWebApiClient client = new DhlWebApiClient("DhlApi", "http://ifslabelservice.com/api/DhlApi.svc");
            client.Open();
            DhlLabelResponse dhlLabelRes = client.GetLabel(dhlLabelReq);
            string tackingNum = dhlLabelRes.TrackingNumber;
            DomesticRateResponse drres = dhlLabelRes.RateInfo;
            Console.WriteLine("OK");

        }
        
    }
}
