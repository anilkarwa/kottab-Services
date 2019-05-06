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
    public class VacantTableController : ApiController
    {

        KOTTabModalContainer result = new KOTTabModalContainer();

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<VacantTables_Result> getVacantTableList()
        {
            List<VacantTables_Result> tableList = new List<VacantTables_Result>();
            tableList = result.VacantTables().ToList<VacantTables_Result>();

            return tableList;
        }

      
        
    }
}