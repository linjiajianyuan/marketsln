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
    public class LoadXml
    {
        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);
        public static DataSet LoadAmazonTrackingUploadResultXml()
        {
            DataSet amazonResultDs = new DataSet();
            try
            {
                amazonResultDs.ReadXml(ConfigurationManager.AppSettings["AmazonTrackingResult"], XmlReadMode.InferSchema);
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex, "LoadAmazonTrackingUploadResultXml: ", ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                return null;
            }

            return amazonResultDs;
        }
    }
}
