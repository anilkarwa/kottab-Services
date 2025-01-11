using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KOTTab.BusinessEntities;
using KOTTab.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Drawing;

namespace KOTTab.Controllers
{
    public class CancellSingleItemPrintController : ApiController
    {
        String valid = "false";
        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;
        String printItemName, printTableName, printKOTNO;
        int printCancelledQty = 0, printWtrId = 0, printPAX = 0, printKCATID;
        private Font printFont;
        public String printPath = "";
        public int printCopy = 0, printingIndex = -1;
        PrintFormat printFormat = new PrintFormat();

        [System.Web.Http.HttpGet]
        public String printCancelledSingleItem(int userId, int KCATID,String KOTNO, String itemName, int cancelledQty, String tableName, int wtrId, int PAX)
        {
            printUser = userId;
            printItemName = itemName; printTableName = tableName;printKOTNO = KOTNO;
            printKCATID = KCATID; printCancelledQty = cancelledQty; printWtrId = wtrId; printPAX = PAX;

            SqlConnection connection = new SqlConnection(connectionString);
            

            String query = "select * from MstKOTPrintFormat";
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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

                valid = "true";

            }
            catch (Exception)
            {
                valid = "false";
            }
            finally
            {
                connection.Close();
            }


            // get printer settings and print
            try
            {


                String query2 = "select PrntPath1,PrntCopy from MstKCatPrnTablet where FoodAreaID = (select FoodAreaID from mstuser where USERID = " + printUser + ") AND KCATID = " + printKCATID + "";
                SqlCommand cmd2 = new SqlCommand(query2, connection);
                connection.Open();
                SqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    printPath = reader.GetString(0);
                    printCopy = reader.GetInt32(1);
                }
                try
                {
                    printFont = new Font("Century Gothic", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    pd.PrinterSettings.PrinterName = printPath;
                    // Print the document.
                    
                        pd.Print();
                    valid = "true";
                    
                }
                finally
                {
                    connection.Close();
                }


            }
            catch (Exception)
            {
                valid = "false";
            }

            return valid;
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

                graphic.DrawString(companyName, new Font("Courier New", 11, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY  + offset);
            }

            offset = offset + (int)fontHeight;//make the spacing consistent

            string kotType = printFormat.cancelledPrint;

            graphic.DrawString(kotType, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 90, startY + offset);

            offset = offset + (int)fontHeight;
            graphic.DrawString("-----------------------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;

            string kotnoanddate = "KOT No:" + printKOTNO.PadRight(5) + " Date:" + DateTime.Now.ToString("dd/MM/yy");
            graphic.DrawString(kotnoanddate, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;

            string kottime = "KOT Time: " + DateTime.Now.ToString("h:mm:ss tt");
            graphic.DrawString(kottime, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;

            string wtrandtableid = "Wtr :" + printWtrId.ToString().PadRight(12) + " Tbl :" + printTableName;
            graphic.DrawString(wtrandtableid, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;

            string pax = "PAX :" + printPAX;
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

            
                //create the string to print on the reciept
                string productDescription = printItemName.PadRight(20) + " " + printCancelledQty;
                // string productTotal = item.Substring(item.Length - 6, 6);

                //MessageBox.Show(item.Substring(item.Length - 5, 5) + "PROD TOTAL: " + productTotal);

                totalprice += printCancelledQty;

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

                    string additionalInst = "''";
                    if (additionalInst != "''")
                    {
                        graphic.DrawString(additionalInst, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

                        offset = offset + (int)fontHeight + 5;
                    }

                }

            offset = offset + (int)fontHeight; //make the spacing consistent
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