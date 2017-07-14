using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Model;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmazonService
{
    public class ListOrders
    {
        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);


        public static DataSet ListAmazonOrderLineByNextToken(string accountName, string token, string merchantId, string marketplaceId, string accessKeyId, string secretAccessKey)
        {
            DataSet amazonOrderLineListDs = new DataSet();
            const string applicationName = "<Your Application Name>";
            const string applicationVersion = "<Your Application Version>";
            ListOrderItemsByNextTokenRequest request = new ListOrderItemsByNextTokenRequest();
            string sellerId = merchantId;
            request.SellerId = sellerId;
            request.NextToken = token;
            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = "https://mws.amazonservices.com";
            MarketplaceWebServiceOrdersClient client = new MarketplaceWebServiceOrdersClient(accessKeyId, secretAccessKey, applicationName, applicationVersion, config);

            try
            {
                IMWSResponse response = null;
                response = client.ListOrderItemsByNextToken(request);
                ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                System.IO.File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonOrderLineList.xml", responseXml);
                amazonOrderLineListDs.ReadXml(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonOrderLineList.xml", XmlReadMode.InferSchema);
                return amazonOrderLineListDs;
            }
            catch (MarketplaceWebServiceOrdersException ex)
            {
                ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                //Console.WriteLine("Service Exception:");
                //if (rhmd != null)
                //{
                //    Console.WriteLine("RequestId: " + rhmd.RequestId);
                //    Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                //}
                //Console.WriteLine("Message: " + ex.Message);
                //Console.WriteLine("StatusCode: " + ex.StatusCode);
                //Console.WriteLine("ErrorCode: " + ex.ErrorCode);
                //Console.WriteLine("ErrorType: " + ex.ErrorType);
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.ErrorWarningMethod("Amazon List Next Order Line Service Exception: " + accountName, "Message: " + ex.Message, senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                return amazonOrderLineListDs;
            }
            catch(Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.ErrorWarningMethod("Amazon List Next Order Line Code General Error: " + accountName, "ex: " + ex.Message, senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                return amazonOrderLineListDs;
            }
        }



        public static DataSet ListAmazonOrderHeader(string accountName, string merchantId, string marketplaceId, string accessKeyId, string secretAccessKey)
        {
            DataSet amazonOrderHeaderListDs = new DataSet();
            const string applicationName = "<Your Application Name>";
            const string applicationVersion = "<Your Application Version>";
            ListOrdersRequest request = new ListOrdersRequest();
            string sellerId = merchantId;
            request.SellerId = sellerId;
            DateTime createdAfter = System.DateTime.Now.AddDays(-15);
            request.CreatedAfter = createdAfter;
            List<string> orderStatusList = new List<string>();
            orderStatusList.Add("Unshipped");
            orderStatusList.Add("PartiallyShipped");
            //orderStatusList.Add("Shipped");
            //orderStatusList.Add("Pending");
            //orderStatusList.Add("Canceled");
            request.OrderStatus = orderStatusList;
            List<string> marketplaceIdList = new List<string>();
            marketplaceIdList.Add(marketplaceId);
            request.MarketplaceId = marketplaceIdList;
            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = "https://mws.amazonservices.com";
            MarketplaceWebServiceOrdersClient client = new MarketplaceWebServiceOrdersClient(accessKeyId, secretAccessKey, applicationName, applicationVersion, config);
            //MarketplaceWebServiceOrdersSample sample = new MarketplaceWebServiceOrdersSample(client);
            try
            {
                IMWSResponse response = null;
                response = client.ListOrders(request);
                ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
                string responseXml = response.ToXML();
                System.IO.File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonOrderHeaderList.xml", responseXml);
                amazonOrderHeaderListDs.ReadXml(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonOrderHeaderList.xml", XmlReadMode.InferSchema);
                return amazonOrderHeaderListDs;
            }
            catch (MarketplaceWebServiceOrdersException ex)
            {
                ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                //Console.WriteLine("Service Exception:");
                //if (rhmd != null)
                //{
                //    Console.WriteLine("RequestId: " + rhmd.RequestId);
                //    Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                //}
                //Console.WriteLine("Message: " + ex.Message);
                //Console.WriteLine("StatusCode: " + ex.StatusCode);
                //Console.WriteLine("ErrorCode: " + ex.ErrorCode);
                //Console.WriteLine("ErrorType: " + ex.ErrorType);
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.ErrorWarningMethod("Amazon List Order Header Service Exception: " + accountName, "Message: " + ex.Message, senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                return amazonOrderHeaderListDs;
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.ErrorWarningMethod("Amazon List Order Header Code General Error: " + accountName, "ex: " + ex.Message, senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                return amazonOrderHeaderListDs;
            }
        }

        public static DataSet ListAmazonOrderHeaderByNextToken(string accountName,string nextToken, string merchantId, string marketplaceId, string accessKeyId, string secretAccessKey)
        {
            DataSet amazonOrderHeaderListDs = new DataSet();
            const string applicationName = "<Your Application Name>";
            const string applicationVersion = "<Your Application Version>";
            ListOrdersByNextTokenRequest request = new ListOrdersByNextTokenRequest();
            string sellerId = merchantId;
            request.SellerId = sellerId;
            request.NextToken = nextToken;
            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = "https://mws.amazonservices.com";
            MarketplaceWebServiceOrdersClient client = new MarketplaceWebServiceOrdersClient(accessKeyId, secretAccessKey, applicationName, applicationVersion, config);

            try
            {
                IMWSResponse response = null;
                response = client.ListOrdersByNextToken(request);
                ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                System.IO.File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonOrderHeaderListNextToken.xml", responseXml);
                amazonOrderHeaderListDs.ReadXml(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonOrderHeaderListNextToken.xml", XmlReadMode.InferSchema);
                return amazonOrderHeaderListDs;
            }
            catch (MarketplaceWebServiceOrdersException ex)
            {
                // Exception properties are important for diagnostics.
                ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                Console.WriteLine("Service Exception:");
                if (rhmd != null)
                {
                    Console.WriteLine("RequestId: " + rhmd.RequestId);
                    Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                }
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("StatusCode: " + ex.StatusCode);
                Console.WriteLine("ErrorCode: " + ex.ErrorCode);
                Console.WriteLine("ErrorType: " + ex.ErrorType);
                throw ex;
            }
        }

        public static DataSet ListAmazonOrderLine(string amazonOrderid, string merchantId, string marketplaceId, string accessKeyId, string secretAccessKey)
        {
            DataSet amazonOrderLineDs = new DataSet();
            const string applicationName = "<Your Application Name>";
            const string applicationVersion = "<Your Application Version>";
            ListOrderItemsRequest request = new ListOrderItemsRequest();
            string sellerId = merchantId;
            request.SellerId = sellerId;
            request.AmazonOrderId = amazonOrderid;
            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = "https://mws.amazonservices.com";
            MarketplaceWebServiceOrdersClient client = new MarketplaceWebServiceOrdersClient(accessKeyId, secretAccessKey, applicationName, applicationVersion, config);
            try
            {
                IMWSResponse response = null;
                response = client.ListOrderItems(request);
                ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                System.IO.File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonOrderLineList.xml", responseXml);
                amazonOrderLineDs.ReadXml(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AmazonOrderLineList.xml",XmlReadMode.InferSchema);
                return amazonOrderLineDs;
            }
            catch (MarketplaceWebServiceOrdersException ex)
            {
                // Exception properties are important for diagnostics.
                ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                Console.WriteLine("Service Exception:");
                if (rhmd != null)
                {
                    Console.WriteLine("RequestId: " + rhmd.RequestId);
                    Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                }
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("StatusCode: " + ex.StatusCode);
                Console.WriteLine("ErrorCode: " + ex.ErrorCode);
                Console.WriteLine("ErrorType: " + ex.ErrorType);
                throw ex;
            }
        }



    }
}
