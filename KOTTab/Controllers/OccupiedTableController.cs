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
    public class OccupiedTableController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();

       

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<OccupiedTableList_Result> getOccupiedTableList()
        {
            List<OccupiedTableList_Result> tableList = new List<OccupiedTableList_Result>();
            tableList = result.OccupiedTableList().ToList<OccupiedTableList_Result>();
            return tableList;
        }
    }
}