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
    public class KOTCATListController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();

        [System.Web.Http.HttpGet]
        public List<KOTCATList_Result> Kotcatlist()
        {
            List<KOTCATList_Result> authStatus = new List<KOTCATList_Result>();
            authStatus = result.KOTCATList().ToList<KOTCATList_Result>();

            return authStatus;
        }
    }
}