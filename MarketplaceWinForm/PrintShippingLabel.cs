using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceWinForm
{
    public class PrintShippingLabel
    {
        static Bitmap printBm;
        static Image image;

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
        public static string Print2(Dictionary<string, byte[]> labelDict)
        {
            foreach (KeyValuePair<string, byte[]> labelInfo in labelDict)
            {
                string orderNum = labelInfo.Key.ToString();
                //string labelBinary = labelInfo.Value;
                Bitmap bm;
                using (MemoryStream ms = new MemoryStream(labelInfo.Value))
                {
                    image = Image.FromStream(ms);
                    bm = new Bitmap(ms);
                    image.Save("gif.gif");

                }
                //_LabelImage.Image = bm;
                //byte[] data = Convert.FromBase64String(labelBinary);
                //string encodedLabelBinary = Encoding.UTF8.GetString(data);
                printBm = new Bitmap(image, 400, 600);
                //RawPrinterHelper.SendStringToPrinter("Zebra ZP 500 (ZPL)", encodedLabelBinary);
                PrintImg("PrimoPDF");
            }

            return "Printed";
        }
        public static void PrintImg(string PrinterName)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings.PrinterName = PrinterName;
            doc.PrintPage += new PrintPageEventHandler(PrintHandler);
            doc.Print();
        }

        private static void PrintHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            Graphics g = ppeArgs.Graphics;
            g.DrawImage(printBm, 0, 0);
        }

    }
}
