using MarketplaceWebService;
using MarketplaceWebService.Model;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmazonService
{
    public class GetReport
    {
        public static void GetAmazonReport(string accountName, string merchantId, string marketplaceId, string accessKeyId, string secretAccessKey, string reportGenerateId)
        {
            string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
            string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
            string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
            string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
            int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

            GetReportRequest request = new GetReportRequest();
            request.Merchant = merchantId;
            request.ReportId = reportGenerateId;
            //request.Report = File.Open(ConfigurationManager.AppSettings["filePath"], FileMode.OpenOrCreate, FileAccess.ReadWrite);
            request.Report = new FileStream(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\"+ accountName + "_AmazonInventoryReport.xml", FileMode.Truncate, FileAccess.ReadWrite);

            const string applicationName = "<Your Application Name>";
            const string applicationVersion = "<Your Application Version>";
            MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
            config.ServiceURL = "https://mws.amazonservices.com";
            config.SetUserAgentHeader(
             applicationName,
             applicationVersion,
             "C#",
             "<Parameter 1>", "<Parameter 2>");
            MarketplaceWebService.MarketplaceWebService service = new MarketplaceWebServiceClient(accessKeyId, secretAccessKey, config);
            try
            {
                GetReportResponse response = service.GetReport(request);


                Console.WriteLine("Service Response");
                Console.WriteLine("=============================================================================");
                Console.WriteLine();

                Console.WriteLine("        GetReportResponse");
                if (response.IsSetGetReportResult())
                {
                    Console.WriteLine("            GetReportResult");
                    GetReportResult getReportResult = response.GetReportResult;
                    if (getReportResult.IsSetContentMD5())
                    {
                        Console.WriteLine("                ContentMD5");
                        Console.WriteLine("                    {0}", getReportResult.ContentMD5);
                    }
                }
                if (response.IsSetResponseMetadata())
                {
                    Console.WriteLine("            ResponseMetadata");
                    ResponseMetadata responseMetadata = response.ResponseMetadata;
                    if (responseMetadata.IsSetRequestId())
                    {
                        Console.WriteLine("                RequestId");
                        Console.WriteLine("                    {0}", responseMetadata.RequestId);
                    }
                }

                Console.WriteLine("            ResponseHeaderMetadata");
                Console.WriteLine("                RequestId");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.RequestId);
                Console.WriteLine("                ResponseContext");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.ResponseContext);
                Console.WriteLine("                Timestamp");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.Timestamp);
            }
            catch (MarketplaceWebServiceException ex)
            {

                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, "GetAmazonReport ", accountName + " " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
            }
            request.Report.Close();
            request.Report.Dispose();
        }
    }
}
