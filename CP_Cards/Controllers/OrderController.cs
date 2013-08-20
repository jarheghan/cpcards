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

        public ActionResult OrderEntryStep2(string storenumber, string Display, string TheRack)
        {
            //storenumber = "0129";
            RackView RV = new RackView();
            if (TheRack == null)
            {
                RV.Cards = ds.GetRackByCardType(storenumber, Display);
            }
            else
            {
                RV.Cards = ds.GetRackByCardType(TheRack, Display);
            }
            ViewBag.display = Display;
            RV.EDCards = ds.GetEveryDayCard("", "");
            if (TheRack == null)
            {
                RV.Accounts = ds.GetSingleAccountInfo(storenumber);
            }
            else
            {
                RV.Accounts = ds.GetSingleAccountInfo(TheRack);
            }
  //          RV.Accounts = ds.GetSingleAccountInfo(storenumber);
            ViewBag.Storenumber = storenumber;
            return View(RV);
        }

        [HttpPost]
        public ActionResult OrderEntryStep2(string storenumber, string Display, string rack, Cards cards)
        {
            //storenumber = "0129";
            RackView RV = new RackView();
            RV.Cards = ds.GetRackByCardType(storenumber, Display);
            ViewBag.display = cards.Display;
            ViewBag.Storenumber = storenumber;
            RV.EDCards = ds.GetEveryDayCard( cards.Rack,storenumber);
            RV.Accounts = ds.GetSingleAccountInfo(storenumber);
            return View(RV);
        }


        public ActionResult OrderEntryStep3(string Thelocation, string NextRack, string Display, string TheRack,string storenumber)
        {
            RackView RV = new RackView();
            RV.Racktemp = new Rack();
            RV.Racktemp.TheLocation = "OrderEntryStep2";
            RV.Racktemp.NextRack = NextRack;
            RV.Racktemp.Display = Display;
            RV.Racktemp.TheRack = storenumber;
            return View(RV);
        }


        public ActionResult OrderSupplies(string Territory)
        {
            ViewBag.Terr = Territory;
            return View();
        }
        


    }
}
