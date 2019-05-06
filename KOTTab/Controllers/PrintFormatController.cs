using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using KOTTab.BusinessEntities;
using System.Data.SqlClient;

namespace KOTTab.Controllers
{
    public class PrintFormatController : ApiController
    {
        PrintFormat printFormat = new PrintFormat();
        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;
       
        [System.Web.Http.HttpGet]
        public PrintFormat getPrintFormat()
        {
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

            }
            catch (Exception)
            {
                
            }
            finally
            {
                connection.Close();
            }
            return printFormat;
        }

        [System.Web.Http.HttpPut]
        public IHttpActionResult updatePrintFormat([FromBody] PrintFormat printFormat)
        {
            Boolean valid = false;
            SqlConnection connection = new SqlConnection(connectionString);
            String query = "update MstKOTPrintFormat set CompanyName = '"+printFormat.companyName+ "', NormalPrint ='"+printFormat.normalPrint+ "', RePrint = '"+printFormat.rePrint+ "',CancelPrint = '"+printFormat.cancelledPrint+ "', IsCompanyNamePrintable = '"+printFormat.showCompanyName+ "', LineBreakOnTop= '"+printFormat.lineBreakOnTop+ "', LineBreakOnBottom = '"+printFormat.lineBreakOnBottom+"' where id = 1 ";
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                valid = true;
            }
            catch (Exception ex)
            {
                valid = false;
            }
            finally
            {
                connection.Close();
            }

            if (valid)
            {
                return Content(HttpStatusCode.OK, "Updated Print Format");
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "Error updating print format, try again");
            }
        }
    }
}