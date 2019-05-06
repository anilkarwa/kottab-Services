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

namespace KOTTab.Controllers
{
    public class TableCountController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;

        [System.Web.Http.HttpGet]
        public TableCountEntity getTableCount()
        {
            var tableData = new TableCountEntity();
            
            SqlConnection connection = new SqlConnection(connectionString);

            String query = "select Count(*) from MstTBL as v where TblStatus = 'V';";
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tableData.vacantTable = reader.GetInt32(0);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            String query2 = "select Count(*) from MstTBL as v where TblStatus = 'O';";
            SqlCommand cmd2 = new SqlCommand(query2, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    tableData.occupiedTable = reader.GetInt32(0);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            String query3 = "select Count(*) from MstTBL as v where TblStatus = 'P';";
            SqlCommand cmd3 = new SqlCommand(query3, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd3.ExecuteReader();
                while (reader.Read())
                {
                    tableData.paymentTable = reader.GetInt32(0);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return tableData;
        }
        
    }
}