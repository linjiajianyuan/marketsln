using MarketplaceWebService;
using MarketplaceWebService.Model;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonService
{
    public class RequestReport
    {
        public static string RequestAmazonInventoryReport(string accountName, string merchantId, string marketplaceId, string accessKeyId, string secretAccessKey)
        {
            string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
            string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
            string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
            string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
            int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

            string ReportRequestId = "";
            RequestReportRequest request = new RequestReportRequest();
            request.Merchant = merchantId;
            request.MarketplaceIdList = new IdList();
            request.MarketplaceIdList.Id = new List<string>(new string[] { marketplaceId });

            request.ReportType = "_GET_FLAT_FILE_OPEN_LISTINGS_DATA_";
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
                RequestReportResponse response = service.RequestReport(request);


                Console.WriteLine("Service Response");
                Console.WriteLine("=============================================================================");
                Console.WriteLine();

                Console.WriteLine("        RequestReportResponse");

                if (response.IsSetRequestReportResult())
                {
                    Console.WriteLine("            RequestReportResult");
                    RequestReportResult requestReportResult = response.RequestReportResult;
                    ReportRequestInfo reportRequestInfo = requestReportResult.ReportRequestInfo;
                    Console.WriteLine("                  ReportRequestInfo");

                    if (reportRequestInfo.IsSetReportProcessingStatus())
                    {
                        Console.WriteLine("               ReportProcessingStatus");
                        Console.WriteLine("                                  {0}", reportRequestInfo.ReportProcessingStatus);
                    }
                    if (reportRequestInfo.IsSetReportRequestId())
                    {
                        Console.WriteLine("                      ReportRequestId");
                        Console.WriteLine("                                  {0}", reportRequestInfo.ReportRequestId);
                        ReportRequestId = reportRequestInfo.ReportRequestId;
                    }
                    if (reportRequestInfo.IsSetReportType())
                    {
                        Console.WriteLine("                           ReportType");
                        Console.WriteLine("                                  {0}", reportRequestInfo.ReportType);
                    }
                    if (reportRequestInfo.IsSetStartDate())
                    {
                        Console.WriteLine("                            StartDate");
                        Console.WriteLine("                                  {0}", reportRequestInfo.StartDate);
                    }
                    if (reportRequestInfo.IsSetEndDate())
                    {
                        Console.WriteLine("                              EndDate");
                        Console.WriteLine("                                  {0}", reportRequestInfo.EndDate);
                    }
                    if (reportRequestInfo.IsSetSubmittedDate())
                    {
                        Console.WriteLine("                        SubmittedDate");
                        Console.WriteLine("                                  {0}", reportRequestInfo.SubmittedDate);
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
                exceptionUtility.CatchMethod(ex, "RequestAmazonInventoryReport ", accountName+" "+ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
            }

            return ReportRequestId;
        }
    }
}
