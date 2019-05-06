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
    public class ActiveOrderDetailsController : ApiController
    {
       public SaveOrder Get(String tableid)
        {
            var activeOrderModal = new ActiveOrderModal();
            return activeOrderModal.getOrderDetails(tableid);
        }
    }
}