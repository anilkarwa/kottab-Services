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
    public class SearchVacantTableController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<SearchVacantTables_Result> searchTable(String term)
        {
            
                List<SearchVacantTables_Result> tableList = new List<SearchVacantTables_Result>();
                tableList = result.SearchVacantTables(term).ToList<SearchVacantTables_Result>();
                return tableList;
        }
    }
}