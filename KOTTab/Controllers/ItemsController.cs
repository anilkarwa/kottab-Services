using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KOTTab.Controllers
{
    public class ItemsController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<GetAllItems1_Result> getAllItems(String tableId)
        {
            List<GetAllItems1_Result> itemList = new List<GetAllItems1_Result>();
            itemList = result.GetAllItems1(tableId).ToList<GetAllItems1_Result>();
            return itemList;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public List<SearchItem_Result1> SearchItem(String term, String tableId, int itemGroupId)
        {
            List<SearchItem_Result1> itemList = new List<SearchItem_Result1>();
            itemList = result.SearchItem(term,tableId,itemGroupId).ToList<SearchItem_Result1>();
            return itemList;
        }
        

    }
}