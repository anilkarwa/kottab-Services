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
    public class LoginController : ApiController
    {
        KOTTabModalContainer result = new KOTTabModalContainer();

        [System.Web.Http.AcceptVerbs("GET","POST")]
        [System.Web.Http.HttpGet]
        public List<AuthenticateUser_Result3> AuthenicateUser(String Username, String Password)
        {
            List<AuthenticateUser_Result3> authStatus = new List<AuthenticateUser_Result3>();
            authStatus = result.AuthenticateUser(Username, Password).ToList<AuthenticateUser_Result3>();
 
            return authStatus;
        }
    }
}