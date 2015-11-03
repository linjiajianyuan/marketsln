﻿using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayMarketplaceCon
{
    class Program
    {
        static void Main(string[] args)
        {
            string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
            string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
            string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
            string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
            int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);
            try
            {
                EbayMarketplaceMdl.Mdl.AddOrderToDb();
            }
            catch (Exception ex)
            {
                ExceptionUtility exceptionUtility = new ExceptionUtility();
                exceptionUtility.CatchMethod(ex,  "Main ", ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
            }
            
        }
    }
}
