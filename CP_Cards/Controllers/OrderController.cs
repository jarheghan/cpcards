using CP_Cards.infasctructure;
using CP_Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP_Cards.Controllers
{
    public class OrderController : Controller
    {
        TSView vTS = new TSView();
        //
        // GET: /Home/
        DataService ds = new DataService();

        public ActionResult OrderEntryStep1(string Territory, string Val)
        {
            TSView ret = new TSView();
            ret.AccountAll = ds.GetAllAccountInfo(Territory);
            ret.SingleAccount = ds.GetSingleAccountInfo1("0101");
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
            else if (tempAccount != "" && (ts.StoreNumber != "" || ts.StoreNumber == ""))
            {
                ret.Order = ds.GetInvoiceInfo(tempAccount);
                ret.SingleAccount = ds.GetSingleAccountInfo1(tempAccount);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            ViewBag.Terr = Territory;
            return View(ret);
        }

        public ActionResult OrderEntryStep2(string storenumber, string Display)
        {
            //storenumber = "0129";
            RackView RV = new RackView();
            RV.Cards = ds.GetRackByCardType(storenumber, Display);
            ViewBag.display = Display;
            RV.EDCards = ds.GetEveryDayCard("", "");
            RV.Accounts = ds.GetSingleAccountInfo(storenumber);
            return View(RV);
        }

        [HttpPost]
        public ActionResult OrderEntryStep2(string storenumber, string Display, string opp)
        {
            //storenumber = "0129";
            RackView RV = new RackView();
            RV.Cards = ds.GetRackByCardType(storenumber, Display);
            ViewBag.display = Display;
            RV.EDCards = ds.GetEveryDayCard("", "");
            RV.Accounts = ds.GetSingleAccountInfo(storenumber);
            return View(RV);
        }


        public ActionResult OrderEntryStep3(string Thelocation, string NextRack, string Display, string TheRack)
        {
            RackView RV = new RackView();
            RV.Racktemp = new Rack();
            //RV.Racktemp.TheLocation = Thelocation;
            RV.Racktemp.NextRack = NextRack;
            RV.Racktemp.Display = Display;
            //RV.Racktemp.TheRack = TheRack;
            return View(RV);
        }
        


    }
}
