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
    public class ShiftToNewTableController : ApiController
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;

        [System.Web.Http.HttpGet]
        public IHttpActionResult shitTable (String oldTableId, String newTableId, String shifType)
        {
            Boolean valid = false;
            var oldTableKOTs = new List<PrintEntity>();

            SqlConnection connection = new SqlConnection(connectionString);
            //get all active KOTIDs
            String query = "select  KOTNO from TrnHdrKOT where TblID = '" + oldTableId + "' and Billed ='N' order by KOTNO asc";
            try
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var kotData = new PrintEntity();
                    kotData.kotNumber = reader.GetInt32(0);
                    oldTableKOTs.Add(kotData);
                }
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

            // Update new table Id in TrnHdrKot table
            for(var i = 0; i < oldTableKOTs.ToArray().Length; i++)
            {
                String query2 = "update TrnHdrKOT set TblID = "+newTableId+" where KOTNO= '"+oldTableKOTs[i].kotNumber+"' ";
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

                var itemList = new List<ItemRate>();
                // get ItemId and PCATID for updating item rate in TrnDtlKOT
                String query3 = " select d.ItemID,d.KOTQty, i.PCatID from TrnDtlKOT as d, MstItemRate as i where d.KOTNO = '"+oldTableKOTs[i].kotNumber+"' and d.ItemID = i.ItemID";
                try
                {
                    SqlCommand cmd = new SqlCommand(query3, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var itemData = new ItemRate();
                        itemData.itemId = reader.GetInt32(0);
                        itemData.KOTQuantity = reader.GetDecimal(1);
                        itemData.PCATID = reader.GetInt32(2);
                        itemList.Add(itemData);
                    }
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

                for( var j = 0; j < itemList.Count; j++)
                {
                    // update item rates in TrnDltKOT table using KOTNO
                    String query4 = " update TrnDtlKOT set KOTRate = (select  tr.ItemRate from MstItemRate as tr  where tr.ItemID = '"+itemList[i].itemId+"' and  tr.PCatID ='"+itemList[i].PCATID+"'), KOTAmt = "+itemList[i].KOTQuantity+ " * (select  tr.ItemRate from MstItemRate as tr  where tr.ItemID = '" + itemList[i].itemId + "' and  tr.PCatID ='" + itemList[i].PCATID + "') where KOTNO = '" + oldTableKOTs[i].kotNumber + "' and ItemID = '" + itemList[i].itemId + "' ";
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query4, connection);
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
                }
            }

            // update table status after shift

            String query5 = "";

            if (shifType == "merge")
            {
                query5 = " update MstTBL set TblStatus='V' where TBLID= '" + oldTableId + "'; ";
            }
            if ( shifType == "shift")
            {
                query5 = " update MstTBL set TblStatus='V' where TBLID= '" + oldTableId + "'; update MstTBL set TblStatus= 'O' where TBLID='" + newTableId + "'; ";
            }
            
            try
            {
                SqlCommand cmd = new SqlCommand(query5, connection);
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
                
                if(shifType == "merge")
                {
                    return Content(HttpStatusCode.OK, "Table Merged Successfully");
                }
                if(shifType == "shift")
                {
                    return Content(HttpStatusCode.OK, "Table Shifted Successfully");
                }
            } else
            {
                if (shifType == "merge")
                {
                    return Content(HttpStatusCode.NotFound, "Error Merging Table");
                }
                if (shifType == "shift")
                {
                    return Content(HttpStatusCode.NotFound, "Error Shifting Table");
                }
            }

            return Content(HttpStatusCode.NotFound, "Error, Try again");


        }

    }
}