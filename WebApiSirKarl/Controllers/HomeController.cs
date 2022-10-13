using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Helpers;
using DirecLayer.Helpers;
using WebApiSirKarl.Models;

namespace WebApiSirKarl.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //bool res = PostingHelpers.LoginAction();
            //if (res)
            //{
            //    //Main Command that executes
            //    var result = PostingHelpers.SBOResponse("Get", $@"Items" , "" , "DocEntry");
            //}

            return View();
        }
    }
}
