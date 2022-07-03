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
    public class CancelItemQuantityController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;

        [System.Web.Http.HttpGet]
        public IHttpActionResult cancelItemQuantity(int KOTNO, int itemId, int quantityNo)
        {
            Boolean valid = false;

            SqlConnection connection = new SqlConnection(connectionString);

            String query2 = "update TrnDtlKOT set KOTAmt = (KOTQty - " + quantityNo + ") * KOTRate , QtyCancelled = QtyCancelled + '" + quantityNo + "' where KOTNO = '" + KOTNO + "' and  ItemID ='" + itemId + "' ";
            try
            {
                SqlCommand cmd = new SqlCommand(query2, connection);
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
                return Content(HttpStatusCode.OK, "Item Quantity Cancelled");
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "Error Cancelling Item Quantity ");
            }
        }
    }
}