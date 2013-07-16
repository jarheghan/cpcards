using CP_Cards.infasctructure;
using CP_Cards.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP_Cards.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        DataService ds = new DataService();

        public ActionResult Login()
        {
            TSView ret = new TSView();
             ret.Retailers =  ds.GetLoginInfo();
             return View(ret);
        }

        [HttpPost]
        public ActionResult Login(string Territory, string Password)
        {

            var model = new TSView
            {
                Retailers = ds.GetLoginInfo(),
            };
            ViewBag.Terr = Territory;
            foreach (var myret in model.Retailers)
            {
                if (myret.Territory == Territory && myret.Password.TrimEnd() == Password)
                {
                    return RedirectToAction("MainPage", "Home", new { Territory = Territory});
                }
            }
            return View(Territory);
        }

    }
}
