using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceWinForm
{
    public class PrintShippingLabel
    {
        public static string Print(Dictionary<string, string> labelDict)
        {
            foreach (KeyValuePair<string, string> labelInfo in labelDict)
            {
                string orderNum = labelInfo.Key.ToString();
                if(orderNum== "EmailSent")
                {
                    return "Error, Email Sent";
                }
                string labelBinary = labelInfo.Value;
                byte[] data = Convert.FromBase64String(labelBinary);
                string encodedLabelBinary = Encoding.UTF8.GetString(data);
                RawPrinterHelper.SendStringToPrinter(ConfigurationManager.AppSettings["printerName"], encodedLabelBinary);
            }
            return "Printed";
        }

    }
}
