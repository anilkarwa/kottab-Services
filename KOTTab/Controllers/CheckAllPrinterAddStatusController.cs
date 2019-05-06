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
    public class CheckAllPrinterAddStatusController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;

        [System.Web.Http.HttpGet]
        public String printerStatus()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            Boolean valid = false;
            String result = "";
            //String query = "select Count(*) as totalPrinterSet, (select Count(*) from MstKCAT where KCATID > 0) as totalKOT from MstKCatPrnTablet as p , MstKCAT as k where p.KCATID = k.KCATID";
            String query = "select k.KCATName from MstKCAT as k  where k.KCATID not in (select KCATID from MstKCatPrnTablet) and k.InActive = 'N' and k.KCATID > 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result += reader.GetString(0);
                    result += ", ";
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                if(result == "")
                {
                    result = "true";
                }
                connection.Close();
            }

            return result;

           
        }

    }
}