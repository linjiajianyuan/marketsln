using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AmazonService
{
    public class GenerateXml
    {
        public static XmlDocument BuildAmazonTrackingFeedXml(DataTable uploadTrackingDt)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(Declaration);
            XmlNode rootNode = xmlDoc.CreateElement("AmazonEnvelope");
            XmlAttribute rootAttribute5 = xmlDoc.CreateAttribute("xmlns:xsi");
            rootAttribute5.Value = "http://www.w3.org/2001/XMLSchema-instance";
            rootNode.Attributes.Append(rootAttribute5);
            //XmlAttribute rootAttribute4 = xmlDoc.CreateAttribute("xsi:noNamespaceSchemaLocation");
            XmlAttribute rootAttribute4 = xmlDoc.CreateAttribute("xsi", "noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
            rootAttribute4.Value = "amzn-envelope.xsd";
            rootNode.Attributes.Append(rootAttribute4);
            xmlDoc.AppendChild(rootNode);

            XmlNode headerNode = xmlDoc.CreateElement("Header");
            rootNode.AppendChild(headerNode);

            XmlNode documentVersionNode = xmlDoc.CreateElement("DocumentVersion");
            documentVersionNode.InnerText = "1.01";
            headerNode.AppendChild(documentVersionNode);

            XmlNode merchantIdentifierNode = xmlDoc.CreateElement("MerchantIdentifier");
            merchantIdentifierNode.InnerText = "M_CARPARTSDE_1214223";
            headerNode.AppendChild(merchantIdentifierNode);


            XmlNode messageTypeNode = xmlDoc.CreateElement("MessageType");
            messageTypeNode.InnerText = "OrderFulfillment";
            rootNode.AppendChild(messageTypeNode);

            int messageId = 1;
            foreach (DataRow uploadTrackingDr in uploadTrackingDt.Rows)
            {
                XmlNode messageNode = xmlDoc.CreateElement("Message");
                rootNode.AppendChild(messageNode);

                XmlNode messageIdNode = xmlDoc.CreateElement("MessageID");
                messageIdNode.InnerText = messageId.ToString();
                messageNode.AppendChild(messageIdNode);

                XmlNode orderFullfillmentNode = xmlDoc.CreateElement("OrderFulfillment");
                messageNode.AppendChild(orderFullfillmentNode);

                XmlNode MerchantOrderIdNode = xmlDoc.CreateElement("AmazonOrderID");
                string orderId = uploadTrackingDr["MerchantOrderID"].ToString();
                MerchantOrderIdNode.InnerText = orderId;
                orderFullfillmentNode.AppendChild(MerchantOrderIdNode);

                XmlNode fulfillmentDateNode = xmlDoc.CreateElement("FulfillmentDate");
                string shipDate = uploadTrackingDr["FulfillmentDate"].ToString();
                fulfillmentDateNode.InnerText = shipDate;
                orderFullfillmentNode.AppendChild(fulfillmentDateNode);


                XmlNode fulfillmentDataNode = xmlDoc.CreateElement("FulfillmentData");
                orderFullfillmentNode.AppendChild(fulfillmentDataNode);

                XmlNode carrierCodeNode = xmlDoc.CreateElement("CarrierCode");
                string carrierCode = uploadTrackingDr["CarrierCode"].ToString();
                carrierCodeNode.InnerText = carrierCode;
                fulfillmentDataNode.AppendChild(carrierCodeNode);

                XmlNode shippingMethodNode = xmlDoc.CreateElement("ShippingMethod");
                string shippingMethod = uploadTrackingDr["ShippingMethod"].ToString();
                shippingMethodNode.InnerText = shippingMethod;
                fulfillmentDataNode.AppendChild(shippingMethodNode);

                XmlNode trackingNode = xmlDoc.CreateElement("ShipperTrackingNumber");
                string tracking = uploadTrackingDr["ShipperTrackingNumber"].ToString();
                trackingNode.InnerText = tracking;
                fulfillmentDataNode.AppendChild(trackingNode);

                messageId = messageId + 1;
            }
            return xmlDoc;
        }
    }
}
