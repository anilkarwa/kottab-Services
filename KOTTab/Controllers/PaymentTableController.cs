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
    public class PaymentTableController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();

        

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<PaymentTableList_Result> getPaymentTableList()
        {
            List<PaymentTableList_Result> tableList = new List<PaymentTableList_Result>();
            tableList = result.PaymentTableList().ToList<PaymentTableList_Result>();
            return tableList;
        }
    }
}