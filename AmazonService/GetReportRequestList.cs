using MarketplaceWebService;
using MarketplaceWebService.Model;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonService
{
    public class GetReportRequestList
    {
        public static DataTable GetAmazonReportRequestList(string accountName, string merchantId, string marketplaceId, string accessKeyId, string secretAccessKey)
        {
            string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
            string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
            string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
            string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
            int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

            DataTable GeneratedReportDt = new DataTable();
            GeneratedReportDt.Columns.Add("ReportRequestId", typeof(System.String));
            GeneratedReportDt.Columns.Add("ReportGenerateId", typeof(System.String));
            GeneratedReportDt.Columns.Add("Status", typeof(System.String));
            GetReportRequestListRequest request = new GetReportRequestListRequest();
            request.Merchant = merchantId;
            const string applicationName = "<Your Application Name>";
            const string applicationVersion = "<Your Application Version>";
            MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
            config.ServiceURL = "https://mws.amazonservices.com";
            config.SetUserAgentHeader(
             applicationName,
             applicationVersion,
             "C#",
             "<Parameter 1>", "<Parameter 2>");
            request.MaxCount = 3;
            MarketplaceWebService.MarketplaceWebService service = new MarketplaceWebServiceClient(accessKeyId, secretAccessKey, config);
            try
            {
                GetReportRequestListResponse response = service.GetReportRequestList(request);


                Console.WriteLine("Service Response");
                Console.WriteLine("=============================================================================");
                Console.WriteLine();

                Console.WriteLine("        GetReportRequestListResponse");

                if (response.IsSetGetReportRequestListResult())
                {
                    Console.WriteLine("            GetReportRequestListResult");
                    GetReportRequestListResult getReportRequestListResult = response.GetReportRequestListResult;
                    List<ReportRequestInfo> reportRequestInfoList = getReportRequestListResult.ReportRequestInfo;

                    foreach (ReportRequestInfo reportRequestInfo in reportRequestInfoList)
                    {
                        DataRow GeneratedReportDr = GeneratedReportDt.NewRow();
                        Console.WriteLine("                  ReportRequestInfo");

                        if (reportRequestInfo.IsSetReportProcessingStatus())
                        {
                            Console.WriteLine("               ReportProcessingStatus");
                            Console.WriteLine("                                  {0}", reportRequestInfo.ReportProcessingStatus);
                            GeneratedReportDr["Status"] = reportRequestInfo.ReportProcessingStatus;
                        }
                        if (reportRequestInfo.IsSetReportRequestId())
                        {
                            Console.WriteLine("                      ReportRequestId");
                            Console.WriteLine("                                  {0}", reportRequestInfo.ReportRequestId);
                            GeneratedReportDr["ReportRequestId"] = reportRequestInfo.ReportRequestId;
                        }
                        if (reportRequestInfo.IsSetGeneratedReportId())
                        {
                            Console.WriteLine("                      GeneratedReportId");
                            Console.WriteLine("                                  {0}", reportRequestInfo.GeneratedReportId);
                            GeneratedReportDr["ReportGenerateId"] = reportRequestInfo.GeneratedReportId;
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
                        GeneratedReportDt.Rows.Add(GeneratedReportDr);
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
                exceptionUtility.CatchMethod(ex, "GetAmazonReportRequestList ", accountName + " " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
            }
            return GeneratedReportDt;
        }
    }
}
