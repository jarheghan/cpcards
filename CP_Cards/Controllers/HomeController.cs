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
       TSView vTS = new TSView();
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
            //TSView ret = new TSView();
            //ret.Account = ds.GetAllAccountInfo(Territory);
            //ViewBag.Terr = Territory;
            //return View(ret);


            TSView ret = new TSView();
            ret.AccountAll = ds.GetAllAccountInfo(Territory);
            ret.Account = ds.GetSingleAccountInfo("0000");
            ViewBag.Terr = Territory;
            return View(ret);
        }

        [HttpPost]
        public ActionResult TimeSheet(string tempAccount, string ss, string Territory, Accounts ts)
        {
            //storenumber = "0131";
            //TSView ret = new TSView();
            //ret.Account = ds.GetSingleAccountInfo(tempAccount);
            //ret.Order = ds.GetInvoiceInfo(tempAccount);
            //ViewBag.Terr = Territory;
            //return View(ret);


            TSView ret = new TSView();
            //ret.Account = ds.GetSingleAccountInfo(tempAccount);
            if (tempAccount == "")
            {
                ret.Order = ds.GetInvoiceInfo(ts.StoreNumber);
                ret.Account = ds.GetSingleAccountInfo(ts.StoreNumber);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            else if (tempAccount != "" && (ts.StoreNumber != "" || ts.StoreNumber == ""))
            {
                ret.Order = ds.GetInvoiceInfo(tempAccount);
                ret.Account = ds.GetSingleAccountInfo(tempAccount);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
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
            //TSView ret = new TSView();
            //ret.Account = ds.GetAllAccountInfo(Territory);
            //ViewBag.Terr = Territory;
            //if (Val == "All")
            //{
            //    ret.Order = ds.GetAllInvoiceInfo(Territory);
            //}
            //ViewBag.val = Val;
            //return View(ret);

            TSView ret = new TSView();
            ret.AccountAll = ds.GetAllAccountInfo(Territory);
            ret.Account = ds.GetSingleAccountInfo("0000");
            if (Val == "All")
            {
                ret.Order = ds.GetAllInvoiceInfo(Territory);
            }
            ViewBag.Terr = Territory;
            ViewBag.val = Val;
            return View(ret);
          
        }

        [HttpPost]
        public ActionResult RecentOrders(string tempAccount, string ss, string Territory, Accounts ts)
        {
            //TSView ret = new TSView();
            //ret.Account = ds.GetSingleAccountInfo(tempAccount);
            //if (tempAccount == "")
            //{
            //    ret.Order = ds.GetAllInvoiceInfo(Territory);
            //}
            //else
            //{
            //    ret.Order = ds.GetInvoiceInfo(tempAccount);
            //}
            //ViewBag.Terr = Territory;
            //return View(ret);



            TSView ret = new TSView();
            //ret.Account = ds.GetSingleAccountInfo(tempAccount);
            if (tempAccount == "")
            {
                ret.Order = ds.GetInvoiceInfo(ts.StoreNumber);
                ret.Account = ds.GetSingleAccountInfo(ts.StoreNumber);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            else if (tempAccount != "" && (ts.StoreNumber != "" || ts.StoreNumber == ""))
            {
                ret.Order = ds.GetInvoiceInfo(tempAccount);
                ret.Account = ds.GetSingleAccountInfo(tempAccount);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            ViewBag.Terr = Territory;
            return View(ret);

        }

        public ActionResult OrderEntryStep1(string Territory, string Val)
        {
            TSView ret = new TSView();
            ret.AccountAll = ds.GetAllAccountInfo(Territory);
            ret.Account = ds.GetSingleAccountInfo("0000");
            ViewBag.Terr = Territory;
            if (Val == "All")
            {
                ret.Order = ds.GetAllInvoiceInfo(Territory);
            }
            ViewBag.val = Val;
            return View(ret);
        }

        [HttpPost]
        public ActionResult OrderEntryStep1(string tempAccount, string ss, string Territory, Accounts ts)
        {
            TSView ret = new TSView();
            //ret.Account = ds.GetSingleAccountInfo(tempAccount);
            if (tempAccount == "")
            {
                ret.Order = ds.GetAllInvoiceInfo(Territory);
                ret.Account = ds.GetSingleAccountInfo(ts.StoreNumber);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            else if(tempAccount != "" && (ts.StoreNumber != "" || ts.StoreNumber == ""))
            {
                ret.Order = ds.GetInvoiceInfo(tempAccount);
                ret.Account = ds.GetSingleAccountInfo(tempAccount);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            ViewBag.Terr = Territory;
            return View(ret);
        }

        public ActionResult OrderEntryStep2(string storenumber)
        {
            storenumber = "0129";
            RackView RV = new RackView();
            RV.Cards = ds.GetRackByCardType("", "");
            RV.EDCards = ds.GetEveryDayCard("", "");
            RV.Accounts = ds.GetSingleAccountInfo(storenumber);
            return View(RV);
        }
        

    }
}
