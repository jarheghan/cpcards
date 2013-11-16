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
    [Authorize]
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
            ViewBag.Terr = Territory;
            ViewBag.PasswordError = true;
             if (ModelState.IsValid)
             {
                 if (ret.IsValid(Territory, Password))
                 {
                     string[] qsInfo = {Territory,ReturnUrl};
                     FormsAuthentication.SetAuthCookie(Territory, ret.RememberMe);
                     return RedirectToLocal(qsInfo);
                 }
                 else
                 {
                     ret1.Retailers = ds.GetLoginInfo();
                     return View(ret1);
                     
                 }
                
             }
            
             return View();
        }
       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        private ActionResult RedirectToLocal(string[] qsInfo)
        {
            var territory = qsInfo[0];
            var returnUrl = qsInfo[1];
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("MainPage", "Home", new { Territory = territory });
            }
        }
    }

}
