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
    public class WaiterListController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<WaiterList_Result> getWaiterList()
        {
            List<WaiterList_Result> waiterList = new List<WaiterList_Result>();
            waiterList = result.WaiterList().ToList<WaiterList_Result>();
            return waiterList;

        }
    }
}