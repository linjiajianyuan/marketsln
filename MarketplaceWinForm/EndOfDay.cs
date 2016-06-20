using MarketplaceWinForm.DhlApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceWinForm
{
    public class EndOfDay
    {
        public static Dictionary<string, object> DomesticEndOfDay()
        {
            Dictionary<string, object> domesticEndOfDayDic = new Dictionary<string, object>();
            CloseDomesticShippingDayRequest request = new CloseDomesticShippingDayRequest();
            Credentials dhlCred = new Credentials();
            dhlCred.Username = ConfigurationManager.AppSettings["firstMileAccount"];
            dhlCred.Password = ConfigurationManager.AppSettings["firstMilePassword"];
            request.UserCredentials = dhlCred;
            request.IsTest = true;
            DhlWebApiClient client = new DhlWebApiClient("DhlApi", "http://ifslabelservice.com/api/DhlApi.svc");
            CloseDomesticShippingDayResponse reponse = client.CloseDomesticShippingDay(request);
            domesticEndOfDayDic.Add("IsSuccessful", reponse.Successful);
            domesticEndOfDayDic.Add("ErrorMessages", reponse.ErrorMessages);
            domesticEndOfDayDic.Add("Manifest", reponse.Manifest);
            domesticEndOfDayDic.Add("BatchNumber", reponse.BatchNumber);
            domesticEndOfDayDic.Add("BOLDataPDF", reponse.BOLDataPDF);
            domesticEndOfDayDic.Add("ManifestedLabelCount", reponse.ManifestedLabelCount);
            domesticEndOfDayDic.Add("TrackingNumbers", reponse.TrackingNumbers);
            domesticEndOfDayDic.Add("MultipleManifestDetails", reponse.MultipleManifestDetails);

            if (reponse.ErrorMessages != null)
            {
                return domesticEndOfDayDic;
            }
            else
            {
                File.WriteAllBytes(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DomesticBOL.pdf", reponse.BOLDataPDF);
                return null;
            }
            
           
        }
    }
}
