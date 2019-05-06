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
using System.Configuration;
using System.Data.SqlClient;

namespace KOTTab.Controllers
{
    public class SearchPaymentTableController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<SearchPaymentables_Result> searchTable(String term)
        {

            List<SearchPaymentables_Result> tableList = new List<SearchPaymentables_Result>();
            tableList = result.SearchPaymentables(term).ToList<SearchPaymentables_Result>();

            return tableList;

        }
    }
}