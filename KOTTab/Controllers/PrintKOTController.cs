using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Drawing.Printing;
using System.IO;
using System.Drawing;
using KOTTab.BusinessEntities;
using System.Configuration;
using System.Data.SqlClient;

namespace KOTTab.Controllers
{
    public class PrintKOTController : ApiController
    {

        private Font printFont;
        public String printPath = "";
        public int printCopy = 0, printingIndex = -1;
        PrintEntity printerPrintData = new PrintEntity();
        List<PrintEntity> printerPrintDataList = new List<PrintEntity>();
        String isprint, isreprint, iscancelled, tableName;
        Dictionary<int, PrintData[]> printObject = new Dictionary<int, PrintData[]>();
        PrintFormat printFormat = new PrintFormat();

        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;

        [System.Web.Http.HttpGet]
        public String printAllKOT(String tableId, String print,String reprint, String cancelled)
        {
            isprint = print;
            isreprint = reprint;
            iscancelled = cancelled;
            SqlConnection connection = new SqlConnection(connectionString);

            // get print format
            String queryFormat = "select * from MstKOTPrintFormat";
            SqlCommand cmd01 = new SqlCommand(queryFormat, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd01.ExecuteReader();
                while (reader.Read())
                {
                    printFormat.companyName = reader.GetString(1);

                    printFormat.normalPrint = reader.GetString(2);

                    printFormat.rePrint = reader.GetString(3);

                    printFormat.cancelledPrint = reader.GetString(4);

                    printFormat.showCompanyName = reader.GetString(5);

                    printFormat.lineBreakOnTop = Convert.ToInt32(reader.GetString(6));

                    printFormat.lineBreakOnBottom = Convert.ToInt32(reader.GetString(7));
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }


            //get table Name
            String querytable = "select TBLName from MstTBL where TBLID = '"+tableId+"' ";
            try
            {
                SqlCommand cmd = new SqlCommand(querytable, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tableName = reader.GetString(0);   
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }




            String query = "";
                // get data from HdtKOT
            if(iscancelled == "N")
            {
                 query = "select top 1 KOTNO,TblID,WtrID,PAX from TrnHdrKOT where TblID = '" + tableId + "' and Billed ='N' order by KOTNO desc";
            } else
            {
                query = "select  KOTNO,TblID,WtrID,PAX from TrnHdrKOT where TblID = '" + tableId + "' and Billed ='N' order by KOTNO asc";
            }
            

            try
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var tempData = new PrintEntity();
                    printerPrintData.kotNumber = reader.GetInt32(0);
                    tempData.kotNumber = reader.GetInt32(0);
                    printerPrintData.tableNumber = reader.GetInt32(1);
                    printerPrintData.waiterId = reader.GetInt32(2);
                    printerPrintData.PAX = reader.GetInt32(3);
                    printerPrintDataList.Add(tempData);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


            // get data from DtlKOT
            var PrintList = new List<PrintData>();
                for (int i = 0; i < printerPrintDataList.ToArray().Length; i++)
                {
                    String query2 = "select i.ItemName,i.KCATID,d.KOTQty,d.AdnlInst  from TrnDtlKOT as d, MstItem as i where d.KOTNO = '" + printerPrintDataList[i].kotNumber + "' and d.ItemID = i.ItemID";

                    try
                    {
                        SqlCommand cmd = new SqlCommand(query2, connection);
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var printData = new PrintData();
                            printData.ItemName = reader.GetString(0);
                            printData.KOTCATID = reader.GetInt32(1);
                            printData.KOTQuantity = reader.GetDecimal(2);
                            printData.AdditionalInstructions = reader.GetString(3);
                            PrintList.Add(printData);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        connection.Close();
                        printerPrintData.printData = PrintList.ToArray();
                    }
                }

            // separte item into array before printing
            PrintData[] temp = printerPrintData.printData;
            
            for (int i =0; i < temp.Length; i++)
            {
                int tempKOTID = temp[i].KOTCATID;
                var printingData = new List<PrintData>();
                for ( int j=0; j < temp.Length; j++)
                {
                    var extractData = new PrintData();
                    if (printerPrintData.printData[j].KOTCATID == tempKOTID && tempKOTID != 0 )
                    {
                        extractData = printerPrintData.printData[j];
                        printingData.Add(extractData);
                        temp[j].KOTCATID = 0;
                    }
                }
                if ( tempKOTID != 0)
                {
                    printObject.Add(tempKOTID, printingData.ToArray());
                }
               
            }

            for (int i = 0; i < printObject.Count; i++)
            {

                try
                {


                    String query3 = "select PrntPath1,PrntCopy from MstKCatPrnTablet where KCATID = " + printObject.Keys.ElementAt(i) + "";
                    SqlCommand cmd = new SqlCommand(query3, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        printPath = reader.GetString(0);
                        printCopy = reader.GetInt32(1);
                    }
                    try
                    {
                        printingIndex = i;
                        printFont = new Font("Century Gothic", 10);
                        PrintDocument pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                        pd.PrinterSettings.PrinterName = printPath;
                        // Print the document.
                        for (int j = 0; j < printCopy; j++)
                        {
                            pd.Print();
                        }

                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                catch (Exception)
                {

                }
            }
            return "true";
        }

        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           

            int total = 0;
            float cash = 0.00f;
            float change = 0.00f;

            //this prints the reciept

            Graphics graphic = e.Graphics;

            Font font = new Font("Courier New", 10); //must use a mono spaced font as the spaces need to line up

            float fontHeight = font.GetHeight();

            int startX = 20;
            int startY = 5;
            int offset = 20 * printFormat.lineBreakOnTop;

            if (printFormat.showCompanyName == "Y")
            {
                string companyName = printFormat.companyName;

                graphic.DrawString(companyName, new Font("Courier New", 11, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            }

            offset = offset + (int)fontHeight;//make the spacing consistent

            String kotType = "";

            if(isprint == "Y")
            {
                kotType = printFormat.normalPrint;
            }

            if (isreprint == "Y")
            {
                kotType = printFormat.rePrint;
            }
            if (iscancelled == "Y")
            {
                kotType = printFormat.cancelledPrint;
            }
            graphic.DrawString(kotType, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 90, startY+ offset );

            offset = offset + (int)fontHeight;
            graphic.DrawString("-----------------------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;

            string kotnoanddate = "KOT No:" + printerPrintData.kotNumber.ToString().PadRight(5) + " Date:" + DateTime.Now.ToString("dd/MM/yy");
            graphic.DrawString(kotnoanddate, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;

            string kottime = "KOT Time: " + DateTime.Now.ToString("h:mm:ss tt");
            graphic.DrawString(kottime, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;

            string wtrandtableid = "Wtr :" + printerPrintData.waiterId.ToString().PadRight(12) + " Tbl :" + tableName;
            graphic.DrawString(wtrandtableid, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;

            string pax = "PAX :" + printerPrintData.PAX;
            graphic.DrawString(pax, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight;
            graphic.DrawString("-----------------------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 10; //make the spacing consistent
            string top = "Item Name".PadRight(20) + " QTY";
            graphic.DrawString(top, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight; //make the spacing consistent
            graphic.DrawString("-----------------------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5; //make the spacing consistent

            int totalprice = 0;

            foreach (PrintData item in printObject[printObject.Keys.ElementAt(printingIndex)])
            {
                //create the string to print on the reciept
                string productDescription = item.ItemName.PadRight(20) + " " + (item.KOTQuantity);
                // string productTotal = item.Substring(item.Length - 6, 6);

                //MessageBox.Show(item.Substring(item.Length - 5, 5) + "PROD TOTAL: " + productTotal);

                totalprice += Convert.ToInt32(item.KOTQuantity);

                if (productDescription.Contains("  -"))
                {
                    string productLine = productDescription.Substring(0, 24);

                    graphic.DrawString(productLine, new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Red), startX, startY + offset);

                    offset = offset + (int)fontHeight + 5; //make the spacing consistent
                }
                else
                {
                    string productLine = productDescription;

                    graphic.DrawString(productLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

                    offset = offset + (int)fontHeight + 5; //make the spacing consistent

                    string additionalInst = "'" + item.AdditionalInstructions + "'";
                    if (additionalInst != "''")
                    {
                        graphic.DrawString(additionalInst, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

                        offset = offset + (int)fontHeight + 5;
                    }
                    
                }

            }

            offset = offset + (int)fontHeight;
            graphic.DrawString("-----------------------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);


            // change = (cash - totalprice);

            //when we have drawn all of the items add the total

            offset = offset + 20; //make some room so that the total stands out.

            graphic.DrawString("         Total Qty : ".PadRight(8) + totalprice, new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (20 * printFormat.lineBreakOnBottom);
            graphic.DrawString("         ... ", new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
        }
        
    }
}