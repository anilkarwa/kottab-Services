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
    public class PrinterSettingsController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;

        public List<Printers> get()
        {

            var printerList = new List<Printers>();

            SqlConnection connection = new SqlConnection(connectionString);

            String query = "select kp.*, k.KCATName, f.FoodAreaName from MstKCatPrnTablet as kp, MstKCAT as k , MstFoodArea as f where kp.KCATID = k.KCATID and kp.FoodAreaID = f.FoodAreaID";
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var printerData = new Printers();
                    printerData.KCatId = reader.GetInt32(0);
                    printerData.FoodAreaID = reader.GetInt32(1);
                    printerData.PrntPath = reader.GetString(2);
                    printerData.PrntCopy = reader.GetInt32(3);
                    printerData.Id = reader.GetInt32(4);
                    printerData.PrntPath2 = reader.GetString(5);
                    printerData.PrntCopy2 = reader.GetInt32(6);
                    printerData.kotTitle = reader.GetString(7);
                    printerData.kotTitle2 = reader.GetString(8);
                    printerData.KCatName = reader.GetString(9);
                    printerData.FoodAreaName = reader.GetString(10);
                    printerList.Add(printerData);
                  
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return printerList;
        }

        public IHttpActionResult post(int kcatid, int foodareaid, String printpath, int printcopy, String printpath2, int printcopy2, String kotTitle, String kotTitle2)
        {

            Boolean valid = false;
            string error = "";

            SqlConnection connection = new SqlConnection(connectionString);

            String query = "insert into MstKCatPrnTablet values("+kcatid+", "+foodareaid+", '"+printpath+"', "+printcopy+ ", '" + printpath2 + "', '" + printcopy2 + "', '" + kotTitle + "', '" + kotTitle2 + "') ";
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
                error = ex.ToString();
            }
            finally
            {
                connection.Close();
            }

            if (!valid)
            {
                return Content(HttpStatusCode.InternalServerError, "Error while saving data = " + error);
                    
            }
            else{
                return Content(HttpStatusCode.OK, "Record Inserted");
            }
        }

        public IHttpActionResult put(int id,int kcatid, int foodareaid, String printpath, int printcopy, String printpath2, int printcopy2, String kottitle, String kottitle2)
        {

            Boolean valid = false;
            string error = "";

            SqlConnection connection = new SqlConnection(connectionString);

            String query = "update MstKCatPrnTablet set KCATID =  " + kcatid + ", FoodAreaID= " + foodareaid + ",PrntPath1 = '" + printpath + "',PrntCopy = " + printcopy + ",PrntPath2 = '" + printpath2 + "',PrntCopy2 = " + printcopy2 + ",kotTitle = '" + kottitle + "',kotTitle2 = '" + kottitle2 + "' where Id = " + id+" ";
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
                error = ex.ToString();
            }
            finally
            {
                connection.Close();
            }

            if (!valid)
            {
                return Content(HttpStatusCode.InternalServerError, "Error while updating data = " + error);

            }
            else
            {
                return Content(HttpStatusCode.OK, "Record Updated");
            }
        }

        public IHttpActionResult delete(int id)
        {

            Boolean valid = false;
            string error = "";

            SqlConnection connection = new SqlConnection(connectionString);

            String query = "delete from MstKCatPrnTablet where Id=" + id + " ";
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
                error = ex.ToString();
            }
            finally
            {
                connection.Close();
            }

            if (!valid)
            {
                return Content(HttpStatusCode.InternalServerError, "Error while delete data = " + error);

            }
            else
            {
                return Content(HttpStatusCode.OK, "Record Deleted");
            }
        }
    }
}