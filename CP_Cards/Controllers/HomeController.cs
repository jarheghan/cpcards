using CP_Cards.infasctructure;
using CP_Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP_Cards.Controllers
{
    public class HomeController : Controller
    {
       
        //
        // GET: /Home/
        DataService ds = new DataService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult MainPage(string Territory,string Password)
        {
            IEnumerable<Retailers> ret =  ds.GetLoginInfo();
            ViewBag.Terr = Territory;
            return View(ret);
        }

        public ActionResult Memo(string Territory)
        {
            IEnumerable<Memos> mem = ds.GetMemoInfo(Territory);
            return View(mem);
        }

        [HttpPost]
        public ActionResult Memo(string memoid, string id)
        {
            return View();
        }

        public ActionResult TimeSheet(string Territory)
        {
            //storenumber = "0131";
            TSView ret = new TSView();
            ret.Account = ds.GetAllAccountInfo(Territory);
            ViewBag.Terr = Territory;
            return View(ret);
        }

        [HttpPost]
        public ActionResult TimeSheet(string tempAccount, string ss, string Territory)
        {
            //storenumber = "0131";
            TSView ret = new TSView();
            ret.Account = ds.GetSingleAccountInfo(tempAccount);
            ret.Order = ds.GetInvoiceInfo(tempAccount);
            ViewBag.Terr = Territory;
            return View(ret);
        }

        
        public ActionResult TimeSheetAdd(TimeSheet timeSheet)
        {
            ds.AddTimeSheeInfo(timeSheet);
            return View();
        }


        public ActionResult RecentOrders(string Territory, string Val)
        {
            TSView ret = new TSView();
            ret.Account = ds.GetAllAccountInfo(Territory);
            ViewBag.Terr = Territory;
            if (Val == "All")
            {
                ret.Order = ds.GetAllInvoiceInfo(Territory);
            }
            ViewBag.val = Val;
            return View(ret);
          
        }

        [HttpPost]
        public ActionResult RecentOrders(string tempAccount, string ss, string Territory)
        {
            TSView ret = new TSView();
            ret.Account = ds.GetSingleAccountInfo(tempAccount);
            if (tempAccount == "")
            {
                ret.Order = ds.GetAllInvoiceInfo(Territory);
            }
            else
            {
                ret.Order = ds.GetInvoiceInfo(tempAccount);
            }
            ViewBag.Terr = Territory;
            return View(ret);

        }

        

    }
}
