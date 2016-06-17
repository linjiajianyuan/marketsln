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
    public class GetFeedSubmissionResult
    {
        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);
        public static void GetAmazonTrackingFeedSubmissionResult(string feedSubmissionId, string merchantId, string marketplaceId, string accessKeyId, string secretAccessKey)
        {
            GetFeedSubmissionResultRequest request = new GetFeedSubmissionResultRequest();
            request.Merchant = merchantId;
            request.FeedSubmissionId = feedSubmissionId;
            //request.FeedSubmissionResult = File.Open("feedSubmissionResult.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            request.FeedSubmissionResult = new FileStream(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonTrackingResult.xml", FileMode.Truncate, FileAccess.ReadWrite);

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
            string requestId="";
            try
            {
                GetFeedSubmissionResultResponse response = service.GetFeedSubmissionResult(request);


                Console.WriteLine("Service Response");
                Console.WriteLine("=============================================================================");
                Console.WriteLine();

                Console.WriteLine("        GetFeedSubmissionResultResponse");
                if (response.IsSetGetFeedSubmissionResultResult())
                {
                    Console.WriteLine("            GetFeedSubmissionResult");
                    GetFeedSubmissionResultResult getFeedSubmissionResultResult = response.GetFeedSubmissionResultResult;
                    if (getFeedSubmissionResultResult.IsSetContentMD5())
                    {
                        Console.WriteLine("                ContentMD5");
                        Console.WriteLine("                    {0}", getFeedSubmissionResultResult.ContentMD5);
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
                exceptionUtility.CatchMethod(ex, "AmazonTrackingResult->GetAmazonTrackingResult: ", feedSubmissionId + ": " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);

                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
                Console.WriteLine("ResponseHeaderMetadata: " + ex.ResponseHeaderMetadata);
            }

            request.FeedSubmissionResult.Close();
            request.FeedSubmissionResult.Dispose();
        }
    }
}
