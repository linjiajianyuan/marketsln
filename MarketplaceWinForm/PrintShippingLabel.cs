using System;
using System.Collections.Generic;
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
                RawPrinterHelper.SendStringToPrinter("Zebra ZP 500 (ZPL)", encodedLabelBinary);
            }
            return "Printed";
        }

    }
}
