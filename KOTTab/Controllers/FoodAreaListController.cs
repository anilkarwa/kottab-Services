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
    public class FoodAreaListController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();


        [System.Web.Http.HttpGet]
        public List<FoodAreaList_Result> FoodAreaList()
        {
            List<FoodAreaList_Result> authStatus = new List<FoodAreaList_Result>();
            authStatus = result.FoodAreaList().ToList<FoodAreaList_Result>();

            return authStatus;
        }
    }
}