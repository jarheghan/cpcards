using CP_Cards.infasctructure;
using CP_Cards.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CP_Cards.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        DataService ds = new DataService();

        [AllowAnonymous]
        public ActionResult Login()
        {
            TSView ret = new TSView();
             ret.Retailers =  ds.GetLoginInfo();
             return View(ret);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string Territory, string Password, string ReturnUrl)
        {
            Retailers ret = new Retailers();
            TSView ret1 = new TSView();
            ret.RememberMe = true;
            //var model = new TSView
            //{
            //    Retailers = ds.GetLoginInfo(),
            //};
           
            //foreach (var myret in model.Retailers)
            //{
            //    if (myret.Territory == Territory && myret.Password.TrimEnd() == Password)
            //    {
            //        return RedirectToAction("MainPage", "Home", new { Territory = Territory});
            //    }
            //}
             ViewBag.Terr = Territory;
             if (ModelState.IsValid)
             {
                 if (ret.IsValid(Territory, Password))
                 {
                     FormsAuthentication.SetAuthCookie(Territory, ret.RememberMe);
                     return RedirectToLocal(ReturnUrl);
                 }
             }
             else
             {
                 ret1.Retailers = ds.GetLoginInfo();
                 return View(ret1);
             }
            
        }
       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("MainPage", "Home", new { Territory = "A0" });
            }
        }
    }

}
