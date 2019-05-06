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
    public class AddItemToActiveOrderController : ApiController
    {
       public IHttpActionResult Post(SaveOrder saveOrder)
        {
            var activeOrderModal = new ActiveOrderModal();
            var output = activeOrderModal.saveMoreItems(saveOrder);
            if( output)
            {
                return Ok("Saved Items");
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "Error Saveing Items");
            }
        }
    }
}