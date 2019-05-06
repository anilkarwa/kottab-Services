using KOTTab.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KOTTab.Models
{
    public class SaveOrderModal
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;

        public String SaveKOTOrder(SaveOrder saveorder)
        {
            int insertId = 0;
            String valid = "false";
            SqlConnection connection = new SqlConnection(connectionString);

            //var smalldate = saveorder.KOTDate.ToString("yyyy-MM-dd hh:mm:ss");

            String query = "insert into TrnHdrKOT values('" + saveorder.KOTDate + "','N','" + saveorder.TableId + "','" + saveorder.WaiterId + "','" + saveorder.AddedBy + "','" + saveorder.TimeOfKOT + "','" + saveorder.AddedDateTime + "','N','-','" + saveorder.PAX + "') SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                insertId = (int)(decimal)cmd.ExecuteScalar();
                valid = "true";
            }
            catch (Exception ex)
            {
                valid = "false";
            }
            finally
            {
                connection.Close();
            }


            for (int i = 0; i < saveorder.ItemsList.Length; i++)
            {
                String query2 = "insert into TrnDtlKOT values('" + insertId + "','" + saveorder.ItemsList[i].SlNo + "','" + saveorder.ItemsList[i].ItemId + "','" + saveorder.ItemsList[i].AdditionalInstructions + "','" + saveorder.ItemsList[i].KOTQuantity + "','" + saveorder.ItemsList[i].KOTRate + "','" + saveorder.ItemsList[i].KOTAmount + "',0.00,0.00)";
                SqlCommand cmd2 = new SqlCommand(query2, connection);

                try
                {
                    connection.Open();
                    cmd2.ExecuteNonQuery();
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

            }
            return valid;
        }
    }
}