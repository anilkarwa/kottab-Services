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
    public class SaveOrderController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET","POST")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Post([FromBody]SaveOrder saveorder)
        {
            var saveOrderModal = new SaveOrderModal();
            var result = saveOrderModal.SaveKOTOrder(saveorder);
            if(result == "true")
            {
                return Ok(result);
            }
            else if(result == "false")
            {
                return Content(HttpStatusCode.NotFound, "Error saving order");
            }
            return Content(HttpStatusCode.NotFound, "Try again");
        }
    }
}