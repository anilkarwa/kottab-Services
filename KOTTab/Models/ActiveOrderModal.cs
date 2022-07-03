using KOTTab.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KOTTab.Models
{
    public class ActiveOrderModal
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;

        public SaveOrder getOrderDetails(String tableid)
        {
            var orderData = new SaveOrder();
            
            var orderList = new List<SaveOrder>();
            SqlConnection connection = new SqlConnection(connectionString);

            String query = "select * from TrnHdrKOT where TblID='"+tableid+"' and Billed='N' order by KOTNO asc";
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    var orderDataTemp = new SaveOrder();
                    orderData.KOTNO = reader.GetInt32(0);
                    orderDataTemp.KOTNO = reader.GetInt32(0);
                    orderData.KOTDate = reader.GetDateTime(1).ToString();
                    orderData.TableId = reader.GetInt32(3);
                    orderData.WaiterId = reader.GetInt32(4);
                    orderData.PAX = reader.GetInt32(10);
                    orderList.Add(orderDataTemp);
                }
                
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            
            var itemList = new List<KOTOrderItems>();
            for (int i = 0; i < orderList.Count; i++)
            {
                String query2 = "select d.*, i.itemName,i.KCATID, (d.KOTQty - d.QtyCancelled) as KOTActiveQty from TrnDtlKOT as d, MstItem as i where KOTNO='" + orderList[i].KOTNO + "' and d.ItemID= i.ItemID  order by  d.SlNO";
                SqlCommand cmd2 = new SqlCommand(query2, connection);
                try
                {

                    connection.Open();
                    SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetDecimal(12) > 0)
                        {
                            var itemData = new KOTOrderItems();
                            itemData.KOTNO = reader.GetInt32(1);
                            itemData.SlNo = reader.GetInt32(2);
                            itemData.ItemId = reader.GetInt32(3);
                            itemData.AdditionalInstructions = reader.GetString(4);
                            itemData.KOTQuantity = (int)reader.GetDecimal(5);
                            itemData.KOTRate = (int)reader.GetDecimal(6);
                            itemData.KOTAmount = (int)reader.GetDecimal(7);
                            itemData.ItemName = reader.GetString(10);
                            itemData.KCATID = reader.GetInt32(11);
                            itemList.Add(itemData);
                        }

                    }
                    orderData.ItemsList = itemList.ToArray();
                }
                catch (Exception)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

            return orderData;
        }

        public Boolean saveMoreItems(SaveOrder orderData)
        {
            Boolean valid = false;
            SqlConnection connection = new SqlConnection(connectionString);

            for (int i = 0; i < orderData.ItemsList.Length; i++)
            {
                String query2 = "insert into TrnDtlKOT values('" + orderData.KOTNO + "','" + orderData.ItemsList[i].SlNo + "','" + orderData.ItemsList[i].ItemId + "','" + orderData.ItemsList[i].AdditionalInstructions + "','" + orderData.ItemsList[i].KOTQuantity + "','" + orderData.ItemsList[i].KOTRate + "','" + orderData.ItemsList[i].KOTAmount + "',0.00,0.00)";
                SqlCommand cmd2 = new SqlCommand(query2, connection);

                try
                {
                    connection.Open();
                    cmd2.ExecuteNonQuery();
                    valid = true;
                }
                catch (Exception)
                {
                    valid = false;
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