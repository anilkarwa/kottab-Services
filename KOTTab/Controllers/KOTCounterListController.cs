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

namespace KOTTab.Controllers
{
    public class KOTCounterListController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<KOTCounterList_Result> Get()
        {
            List<KOTCounterList_Result> counterList = new List<KOTCounterList_Result>();
            return result.KOTCounterList().ToList<KOTCounterList_Result>();
        }
    }
}