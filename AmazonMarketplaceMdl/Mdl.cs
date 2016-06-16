using AmazonMarketplaceComm;
using PebbledonUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace AmazonMarketplaceMdl
{
    public class Mdl
    {
        public static void AddAmazonOrderHeader(AmazonOrderType amazonOrderType, DataRow headerDr, DataRow[] shippingAddressDr, DataRow[] orderTotalDr, string orderStatus)
        {
            amazonOrderType.Header.order_id = headerDr["AmazonOrderId"].ToString();
            amazonOrderType.Header.purchase_date = headerDr["PurchaseDate"].ToString();
            amazonOrderType.Header.payments_date = headerDr["PurchaseDate"].ToString();
            amazonOrderType.Header.buyer_email = headerDr["BuyerEmail"].ToString().Replace("'", "''");
            amazonOrderType.Header.buyer_name = shippingAddressDr[0]["Name"].ToString().Replace("'", "''");
            amazonOrderType.Header.buyer_phone_number = shippingAddressDr[0]["Phone"].ToString();
            amazonOrderType.Header.currency = orderTotalDr[0]["CurrencyCode"].ToString();
            amazonOrderType.Header.recipient_name = shippingAddressDr[0]["Name"].ToString().Replace("'", "''");
            amazonOrderType.Header.ship_address_1 = shippingAddressDr[0]["AddressLine1"].ToString().Replace("'", "''");
            if (shippingAddressDr[0].Table.Columns.Contains("AddressLine2"))
            {
                amazonOrderType.Header.ship_address_2 = shippingAddressDr[0]["AddressLine2"].ToString().Replace("'", "''");
            }
            else
            {
                amazonOrderType.Header.ship_address_2 = "";
            }

            amazonOrderType.Header.ship_address_3 = "";
            amazonOrderType.Header.ship_city = shippingAddressDr[0]["City"].ToString().Replace("'", "''");
            amazonOrderType.Header.ship_state = shippingAddressDr[0]["StateOrRegion"].ToString().Replace("'", "''");
            string zipCode = shippingAddressDr[0]["PostalCode"].ToString().Replace("'", "''");
            if (zipCode.Length <= 4)
            {
                zipCode = '0' + zipCode;
                amazonOrderType.Header.ship_postal_code = zipCode;
            }
            else
            {
                amazonOrderType.Header.ship_postal_code = zipCode;
            }
            amazonOrderType.Header.ship_country = shippingAddressDr[0]["CountryCode"].ToString().Replace("'", "''");
            amazonOrderType.Header.ship_phone_number = shippingAddressDr[0]["Phone"].ToString().Replace("'", "''");
            amazonOrderType.Header.tax_location_code = "";
            amazonOrderType.Header.tax_location_city = "";
            amazonOrderType.Header.tax_location_county = "";
            amazonOrderType.Header.tax_location_state = "";
            amazonOrderType.Header.delivery_Instructions = headerDr["ShipmentServiceLevelCategory"].ToString().Replace("'", "''");
            amazonOrderType.Header.sales_channel = "Amazon";
            amazonOrderType.Header.dataTransferStatus = 0;
            amazonOrderType.Header.enterDate = DateTime.Now;
            amazonOrderType.Header.updateDate = DateTime.Now;
            amazonOrderType.Header.orderStatus = orderStatus;
            amazonOrderType.Header.customizedMessage = "";
        }

        private static string GetCustomizedDataFromURL(string url)
        {
            string customizedInfo = "";
            using (var client = new WebClient())
            {
                client.DownloadFile(url, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CustomizedInfo.zip");
            }
            ZipFile.ExtractToDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CustomizedInfo.zip", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CustomizedInfo");
            var jsonFiles = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CustomizedInfo").GetFiles("*.json");
            foreach(var jasonFile in jsonFiles)
            {
                string fileName = jasonFile.Name;
                JObject o1 = JObject.Parse(File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CustomizedInfo\\"+fileName));
                var version30 = o1["version3.0"];
                var customInfo = version30["customizationInfo"];
                var surfaces = customInfo["surfaces"];
                var surfaces0 = surfaces[0];
                var areas = surfaces0["areas"];
                var areas0 = areas[0];
                customizedInfo = customizedInfo + "|" + areas0["fontFamily"].ToString() + "," + areas0["label"].ToString() + "," + areas0["fill"].ToString() + "," + areas0["text"].ToString();
                File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CustomizedInfo\\" + fileName);
            }
            return customizedInfo;
        }
        public static void AddAmazonOrderLine(AmazonOrderType amazonOrderType, string amazonOrderId, AmazonOrderLineType lineType, DataRow[] orderItemDr, DataRow[] itemPriceDr, DataRow[] shippingPriceDr, DataRow[] promotionDiscountDr, DataRow[] itemTaxDr, DataRow[] itemShippingDiscountDr, DataRow[] itemShippingTaxDr,DataRow[] itemCustomizedInfoDr)
        {
            lineType.amazon_order_id = amazonOrderId;
            lineType.order_item_id = orderItemDr[0]["OrderItemId"].ToString();
            lineType.sku = orderItemDr[0]["SellerSKU"].ToString();
            lineType.product_name = orderItemDr[0]["Title"].ToString().Replace("'", "''");
            lineType.quantity_purchased = ConvertUtility.ToInt16(orderItemDr[0]["QuantityOrdered"]);
            lineType.item_price = ConvertUtility.ToDecimal(itemPriceDr[0]["Amount"]);
            lineType.item_tax = ConvertUtility.ToDecimal(itemTaxDr[0]["Amount"]);
            lineType.shipping_price = shippingPriceDr == null ? 0: Convert.ToDecimal(shippingPriceDr[0]["Amount"]);
            lineType.shipping_tax = itemShippingTaxDr==null?0: ConvertUtility.ToDecimal(itemShippingTaxDr[0]["Amount"]);
            lineType.item_promotion_discount = ConvertUtility.ToDecimal(promotionDiscountDr[0]["Amount"]);
            lineType.ship_promotion_discount = itemShippingDiscountDr==null?0: ConvertUtility.ToDecimal(itemShippingDiscountDr[0]["Amount"]);
            string customizedUrl = itemCustomizedInfoDr==null?null: itemCustomizedInfoDr[0]["CustomizedURL"].ToString();
            if(customizedUrl==null)
            {
                amazonOrderType.Header.customizedMessage = amazonOrderType.Header.customizedMessage + "";
            }
            else
            {
                string customizedMessage = GetCustomizedDataFromURL(customizedUrl);
                amazonOrderType.Header.customizedMessage = amazonOrderType.Header.customizedMessage + "|" + lineType.sku + "(" + customizedMessage + ")";
            }
            
            lineType.dataTransferStatus = 0;
        }

        private static string senderEmail = ConfigurationManager.AppSettings["senderEmail"];
        private static string messageFromPassword = ConfigurationManager.AppSettings["messageFromPassword"];
        private static string messageToEmail = ConfigurationManager.AppSettings["messageToEmail"];
        private static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private static int smtpPortNum = ConvertUtility.ToInt(ConfigurationManager.AppSettings["smtpPortNum"]);

        public static void GetAmazonOrder()
        {
            DataTable sellerAccountDt = Db.Db.GetAmazonDeveloperInfo();
            foreach(DataRow sellerAccountDr in sellerAccountDt.Rows)
            {
                try
                {
                    string merchantId = sellerAccountDr["SellerID"].ToString();
                    string marketplaceId = sellerAccountDr["MarketplaceID"].ToString();
                    string accessKeyId = sellerAccountDr["AccessKeyID"].ToString();
                    string secretAccessKey = sellerAccountDr["SecretKey"].ToString();
                    string accountName = sellerAccountDr["AccountName"].ToString();
                    string channel = sellerAccountDr["Channel"].ToString();
                    DataSet amazonOrderHeaderDs = new DataSet();
                    amazonOrderHeaderDs = AmazonService.ListOrders.ListAmazonOrderHeader(accountName, merchantId, marketplaceId, accessKeyId, secretAccessKey);
                    System.Threading.Thread.Sleep(2000);
                    DataTable amazonOrderHeaderListOrdersResultDt = amazonOrderHeaderDs.Tables["ListOrdersResult"];
                    // CHECK IF FIRST CALL WITH NEXT TOKEN
                    string amazonOrderHeaderNextToken = "";
                    foreach (DataRow amazonOrderHeaderListOrdersResultDr in amazonOrderHeaderListOrdersResultDt.Rows)
                    {
                        DataColumnCollection columns = amazonOrderHeaderListOrdersResultDt.Columns;
                        if (columns.Contains("NextToken"))
                        {
                            amazonOrderHeaderNextToken = amazonOrderHeaderListOrdersResultDr["NextToken"].ToString();
                            //Console.WriteLine(amazonOrderHeaderNextToken);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    // END
                    // use NEXT TOKEN to get additional orders
                    while (amazonOrderHeaderNextToken != "")
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(2000);
                            DataSet amazonOrderHeaderNextTokenDs = AmazonService.ListOrders.ListAmazonOrderHeaderByNextToken(accountName,amazonOrderHeaderNextToken, merchantId, marketplaceId, accessKeyId, secretAccessKey);
                            DataTable amazonOrderHeaderListOrdersNextTokenResultDt = amazonOrderHeaderNextTokenDs.Tables["ListOrdersByNextTokenResult"];
                            foreach (DataRow amazonOrderHeaderListOrdersNextTokenResultDr in amazonOrderHeaderListOrdersNextTokenResultDt.Rows)
                            {
                                DataColumnCollection columns = amazonOrderHeaderListOrdersNextTokenResultDt.Columns;
                                if (columns.Contains("NextToken"))
                                {
                                    amazonOrderHeaderNextToken = amazonOrderHeaderListOrdersNextTokenResultDr["NextToken"].ToString();
                                }
                                else
                                {
                                    amazonOrderHeaderNextToken = "";
                                    continue;
                                }
                            }

                            DataTable amazonOrderHeaderNextTokenDt = amazonOrderHeaderNextTokenDs.Tables["Order"];
                            DataTable amazonOrderHeaderShippingAddressNextTokenDt = amazonOrderHeaderNextTokenDs.Tables["ShippingAddress"];
                            DataTable amazonOrderHeaderTotalNextTokenDt = amazonOrderHeaderNextTokenDs.Tables["OrderTotal"];
                            DataRow[] shippingAddressNextTokenDr;
                            DataRow[] orderTotalNextTokenDr;

                            foreach (DataRow headerDr in amazonOrderHeaderNextTokenDt.Rows)
                            {
                                AmazonOrderType amazonOrderType = new AmazonOrderType();
                                int internalOrderId = ConvertUtility.ToInt(headerDr["Order_Id"]);
                                string amazonOrderId = headerDr["AmazonOrderId"].ToString();
                                if(amazonOrderId== "114-1651563-9917828")
                                {
                                    Console.WriteLine("");
                                }
                                string orderStatus = headerDr["OrderStatus"].ToString();
                                DataRow checker = Db.Db.CheckAmazonOrderDuplicatedDb(amazonOrderId);
                                if (checker == null)
                                {
                                    try
                                    {
                                        shippingAddressNextTokenDr = amazonOrderHeaderShippingAddressNextTokenDt.Select("Order_Id='" + internalOrderId + "'");
                                        orderTotalNextTokenDr = amazonOrderHeaderTotalNextTokenDt.Select("Order_Id='" + internalOrderId + "'");
                                        AddAmazonOrderHeader(amazonOrderType, headerDr, shippingAddressNextTokenDr, orderTotalNextTokenDr, orderStatus);
                                        DataSet amazonOrderLineNextTokenDs = AmazonService.ListOrders.ListAmazonOrderLine(amazonOrderId, merchantId, marketplaceId, accessKeyId, secretAccessKey);

                                        System.Threading.Thread.Sleep(2000);
                                        DataTable amazonOrderLineListOrderItemsResultNextTokenDt = amazonOrderLineNextTokenDs.Tables["ListOrderItemsResult"];

                                        DataTable amazonOrderLineOrderItemNextTokenDt = amazonOrderLineNextTokenDs.Tables["OrderItem"];
                                        DataTable amazonOrderLineItemPriceNextTokenDt = amazonOrderLineNextTokenDs.Tables["ItemPrice"];
                                        DataTable amazonOrderLineShippingPriceNextTokenDt = amazonOrderLineNextTokenDs.Tables["ShippingPrice"];
                                        DataTable amazonOrderLinePromotionDiscountNextTokenDt = amazonOrderLineNextTokenDs.Tables["PromotionDiscount"];
                                        DataTable amazonOrderLineItemTaxNextTokenDt = amazonOrderLineNextTokenDs.Tables["ItemTax"];
                                        DataTable amazonOrderLineShippingDiscountNextTokenDt = amazonOrderLineNextTokenDs.Tables["ShippingDiscount"];
                                        DataTable amazonOrderLineShippingTaxNextTokenDt = amazonOrderLineNextTokenDs.Tables["ShippingTax"];
                                        DataTable amazonOrderLineCustomizedDt = amazonOrderLineNextTokenDs.Tables["BuyerCustomizedInfo"];
                                        foreach (DataRow amazonOrderLineOrderItemNextTokenDr in amazonOrderLineOrderItemNextTokenDt.Rows)
                                        {
                                            int internalItemId = ConvertUtility.ToInt(amazonOrderLineOrderItemNextTokenDr["OrderItem_Id"]);
                                            DataRow[] orderItemDr = amazonOrderLineOrderItemNextTokenDt.Select("OrderItem_Id='" + internalItemId + "'");
                                            DataRow[] itemPriceDr = amazonOrderLineItemPriceNextTokenDt.Select("OrderItem_Id='" + internalItemId + "'");
                                            DataRow[] shippingPriceDr = amazonOrderLineShippingPriceNextTokenDt == null ? null : amazonOrderLineShippingPriceNextTokenDt.Select("OrderItem_Id='" + internalItemId + "'");
                                            DataRow[] promotionDiscountDr = amazonOrderLinePromotionDiscountNextTokenDt.Select("OrderItem_Id='" + internalItemId + "'");
                                            DataRow[] itemTaxDr = amazonOrderLineItemTaxNextTokenDt.Select("OrderItem_Id='" + internalItemId + "'");
                                            DataRow[] itemShippingDiscountDr = amazonOrderLineShippingDiscountNextTokenDt==null?null: amazonOrderLineShippingDiscountNextTokenDt.Select("OrderItem_Id='" + internalItemId + "'");
                                            DataRow[] itemShippingTaxDr = amazonOrderLineShippingTaxNextTokenDt == null ? null : amazonOrderLineShippingTaxNextTokenDt.Select("OrderItem_Id='" + internalItemId + "'");
                                            DataRow[] itemCustomizedInfoDr = amazonOrderLineCustomizedDt == null ? null : amazonOrderLineCustomizedDt.Select("OrderItem_Id ='" + internalItemId + "'");
                                            AmazonOrderLineType lineType = new AmazonOrderLineType();
                                            AddAmazonOrderLine(amazonOrderType, amazonOrderId, lineType, orderItemDr, itemPriceDr, shippingPriceDr, promotionDiscountDr, itemTaxDr, itemShippingDiscountDr, itemShippingTaxDr, itemCustomizedInfoDr);
                                            amazonOrderType.Lines.Add(lineType);
                                        }
                                        try
                                        {
                                            Db.Db.AddAmazonOrderTran(accountName, amazonOrderType);
                                        }
                                        catch (Exception ex)
                                        {
                                            ExceptionUtility exceptionUtility = new ExceptionUtility();
                                            exceptionUtility.CatchMethod(ex, "AddAmazonOrderTran Next Token:", accountName + " " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                            continue;
                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        ExceptionUtility exceptionUtility = new ExceptionUtility();
                                        exceptionUtility.CatchMethod(ex, "Amazon Order was canceled:", accountName + " " + amazonOrderId+" "+ ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                        continue;
                                    }
                                }
                                else
                                {
                                    Db.Db.UpdateAmazonOrderToCancel(amazonOrderId, orderStatus);
                                    continue;
                                }
                             }

                        }
                        catch (Exception ex)
                        {
                            ExceptionUtility exceptionUtility = new ExceptionUtility();
                            exceptionUtility.CatchMethod(ex, "use NEXT TOKEN to get additional orders error:", accountName + " " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                            continue;
                        }
                    }


                    DataTable amazonOrderHeaderDt = amazonOrderHeaderDs.Tables["Order"];
                    DataTable amazonOrderHeaderShippingAddressDt = amazonOrderHeaderDs.Tables["ShippingAddress"];
                    DataTable amazonOrderHeaderTotalDt = amazonOrderHeaderDs.Tables["OrderTotal"];
                    DataRow[] shippingAddressDr;
                    DataRow[] orderTotalDr;
                    foreach (DataRow headerDr in amazonOrderHeaderDt.Rows)  //GET FIRST EACH ORDER HEADER LINE
                    {
                        AmazonOrderType amazonOrderType = new AmazonOrderType();
                        int internalOrderId = ConvertUtility.ToInt(headerDr["Order_Id"]);
                        string amazonOrderId = headerDr["AmazonOrderId"].ToString();
                        string orderStatus = headerDr["OrderStatus"].ToString();
                        if (amazonOrderId == "114-1651563-9917828")
                        {

                            Console.WriteLine("");
                        }

                        DataRow checker = Db.Db.CheckAmazonOrderDuplicatedDb(amazonOrderId);
                        if (checker == null) // if order is not exist
                        {
                            try
                            {
                                shippingAddressDr = amazonOrderHeaderShippingAddressDt.Select("Order_Id='" + internalOrderId + "'");
                                orderTotalDr = amazonOrderHeaderTotalDt.Select("Order_Id='" + internalOrderId + "'");
                                AddAmazonOrderHeader(amazonOrderType, headerDr, shippingAddressDr, orderTotalDr, orderStatus);
                                DataSet amazonOrderLineDs = new DataSet();
                                try
                                {
                                    amazonOrderLineDs = AmazonService.ListOrders.ListAmazonOrderLine(amazonOrderId, merchantId, marketplaceId, accessKeyId, secretAccessKey);
                                    System.Threading.Thread.Sleep(2000);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionUtility exceptionUtility = new ExceptionUtility();
                                    exceptionUtility.CatchMethod(ex, "AmazonService.ListOrders.ListAmazonOrderLine error:", accountName + " " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                    continue;
                                }

                                DataTable amazonOrderLineListOrderItemsResultDt = amazonOrderLineDs.Tables["ListOrderItemsResult"];

                                // CHECK IF FIRST ITEM CALL WITH NEXT TOKEN
                                string amazonOrderLineNextToken = "";
                                foreach (DataRow amazonOrderLineListOrderItemsResultDr in amazonOrderLineListOrderItemsResultDt.Rows)
                                {
                                    DataColumnCollection columns = amazonOrderLineListOrderItemsResultDt.Columns;
                                    if (columns.Contains("NextToken"))
                                    {
                                        amazonOrderLineNextToken = amazonOrderLineListOrderItemsResultDr["NextToken"].ToString();
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                // END

                                DataTable amazonOrderLineOrderItemDt = amazonOrderLineDs.Tables["OrderItem"];
                                DataTable amazonOrderLineItemPriceDt = amazonOrderLineDs.Tables["ItemPrice"];
                                DataTable amazonOrderLineShippingPriceDt = amazonOrderLineDs.Tables["ShippingPrice"];
                                DataTable amazonOrderLinePromotionDiscountDt = amazonOrderLineDs.Tables["PromotionDiscount"];
                                DataTable amazonOrderLineItemTaxDt = amazonOrderLineDs.Tables["ItemTax"];
                                DataTable amazonOrderLineShippingDiscountDt = amazonOrderLineDs.Tables["ShippingDiscount"];
                                DataTable amazonOrderLineShippingTaxDt = amazonOrderLineDs.Tables["ShippingTax"];
                                DataTable amazonOrderLineCustomizedDt = amazonOrderLineDs.Tables["BuyerCustomizedInfo"];
                                foreach (DataRow amazonOrderLineOrderItemDr in amazonOrderLineOrderItemDt.Rows)
                                {
                                    int internalItemId = ConvertUtility.ToInt(amazonOrderLineOrderItemDr["OrderItem_Id"]);
                                    DataRow[] orderItemDr = amazonOrderLineOrderItemDt.Select("OrderItem_Id='" + internalItemId + "'");
                                    DataRow[] itemPriceDr = amazonOrderLineItemPriceDt.Select("OrderItem_Id='" + internalItemId + "'");
                                    if (itemPriceDr.Length > 0)
                                    {
                                        DataRow[] shippingPriceDr = amazonOrderLineShippingPriceDt==null?null: amazonOrderLineShippingPriceDt.Select("OrderItem_Id='" + internalItemId + "'");
                                        DataRow[] promotionDiscountDr = amazonOrderLinePromotionDiscountDt.Select("OrderItem_Id='" + internalItemId + "'");
                                        DataRow[] itemTaxDr = amazonOrderLineItemTaxDt.Select("OrderItem_Id='" + internalItemId + "'");
                                        DataRow[] itemShippingDiscountDr = amazonOrderLineShippingDiscountDt == null ? null : amazonOrderLineShippingDiscountDt.Select("OrderItem_Id='" + internalItemId + "'");
                                        DataRow[] itemShippingTaxDr = amazonOrderLineShippingTaxDt == null ? null : amazonOrderLineShippingTaxDt.Select("OrderItem_Id='" + internalItemId + "'");
                                        DataRow[] itemCustomizedInfoDr = amazonOrderLineCustomizedDt == null ? null : amazonOrderLineCustomizedDt.Select("OrderItem_Id ='" + internalItemId + "'");
                                        AmazonOrderLineType lineType = new AmazonOrderLineType();
                                        AddAmazonOrderLine(amazonOrderType, amazonOrderId, lineType, orderItemDr, itemPriceDr, shippingPriceDr, promotionDiscountDr, itemTaxDr, itemShippingDiscountDr, itemShippingTaxDr, itemCustomizedInfoDr);
                                        amazonOrderType.Lines.Add(lineType);
                                    }
                                    else
                                    {
                                        ExceptionUtility exceptionUtility = new ExceptionUtility();
                                        exceptionUtility.ErrorWarningMethod("AddAmazonOrderTran Transaction:", accountName + " " + amazonOrderId + ": one item has been canceled", senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                        continue;
                                    }
                                }

                                try
                                {
                                    Db.Db.AddAmazonOrderTran(accountName,amazonOrderType);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionUtility exceptionUtility = new ExceptionUtility();
                                    exceptionUtility.CatchMethod(ex, "AddAmazonOrderTran Transaction error:", accountName + " " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                    continue;
                                }
                            }
                            catch(Exception ex)
                            {
                                ExceptionUtility exceptionUtility = new ExceptionUtility();
                                exceptionUtility.CatchMethod(ex, "Order was canceled:", accountName + " "+ amazonOrderId+" " + ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                                continue;
                            }
                        }
                        else
                        {
                            Db.Db.UpdateAmazonOrderToCancel(amazonOrderId, orderStatus);
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility exceptionUtility = new ExceptionUtility();
                    exceptionUtility.CatchMethod(ex, "GetAmazonOrder Error:", ex.Message.ToString(), senderEmail, messageFromPassword, messageToEmail, smtpClient, smtpPortNum);
                    continue; 
                }
            }
        }
    }
}
