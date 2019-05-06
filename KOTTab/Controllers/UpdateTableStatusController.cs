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
    public class UpdateTableStatusController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Get(int tableId, String tableStatus)
        {
            result.UpdateTableStatus(tableId, tableStatus);
            return Content(HttpStatusCode.OK, "Update Table Status");
        }

    }
}