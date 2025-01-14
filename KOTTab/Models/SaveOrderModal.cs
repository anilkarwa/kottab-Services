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

            String query = "insert into TrnHdrKOT (kotNo,KotDate, Cancelled,TblID,WtrID,AddedBy," +
                "TimeOfKot,AddedOn,Billed,NoOfPax) values(" +
                "(select isnull(max(kotno),0)+1 from trnhdrkot h where h.kotdate = '" + saveorder.KOTDate + "')," +
                "'" + saveorder.KOTDate + "'," +
                "'N'," +
                "'" + saveorder.TableId + "'," +
                "'" + saveorder.WaiterId + "'," +
                "'" + saveorder.AddedBy + "'," +
                "'" + saveorder.TimeOfKOT + "'," +
                "'" + saveorder.AddedDateTime + "'," +
                "'N'," +
                "'" + saveorder.PAX + "')";

           
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                cmd.ExecuteScalar();
                valid = "true";
            }
            catch (Exception ex)
            {
                valid = ex.ToString() + query;
            }
            finally
            {
                connection.Close();
            }


            SqlCommand cmd3 = new SqlCommand("Select Max(KOTNO) From TrnHdrKOT", connection);
            try
            {
                connection.Open();
                insertId = Convert.ToInt32(cmd3.ExecuteScalar());
                valid = "true";
            }
            catch (Exception ex)
            {
                valid = ex.ToString() + "max no";
            }
            finally
            {
                connection.Close();
            }


            for (int i = 0; i < saveorder.ItemsList.Length; i++)
            {
                String query2 = "insert into TrnDtlKOT(KotID, slno, " +
                    "Itemid,itemextrainfo,Remarks, " +
                    "kotqty, kotrate,kotamt) values('"
                    + insertId + "','" + saveorder.ItemsList[i].SlNo +
                    "','" + saveorder.ItemsList[i].ItemId + "','" + saveorder.ItemsList[i].AdditionalInstructions + 
                    "','','" + saveorder.ItemsList[i].KOTQuantity + "','" + saveorder.ItemsList[i].KOTRate + "','" +
                    saveorder.ItemsList[i].KOTAmount + "')";


                SqlCommand cmd2 = new SqlCommand(query2, connection);
                try
                {
                    connection.Open();
                    cmd2.ExecuteNonQuery();
                    valid = "true";
                }
                catch (Exception ex)
                {
                    valid = ex.ToString() + query2;
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