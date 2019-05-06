using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using KOTTab.BusinessEntities;
using System.Data.SqlClient;
namespace KOTTab.Controllers
{
    public class ItemMenuGroupListController : ApiController
    {

        KOTTabModalContainer result = new KOTTabModalContainer();

        [System.Web.Http.HttpGet]
        public List<ItemMenuGroups_Result> getItemMenuGroupList()
        {
            List<ItemMenuGroups_Result> groupList = new List<ItemMenuGroups_Result>();
            groupList = result.ItemMenuGroups().ToList<ItemMenuGroups_Result>();
            return groupList;
        }
    }
}