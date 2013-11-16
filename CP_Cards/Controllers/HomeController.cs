using CP_Cards.infasctructure;
using CP_Cards.Models;
using CP_Cards.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP_Cards.Controllers
{
  
    [Authorize]
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

        public ActionResult MainPage(string Territory,string Password, string SessionEnd)
        {
            IEnumerable<Retailers> ret =  ds.GetLoginInfo();
            ViewBag.Terr = Territory;
            if (SessionEnd == "True")
                OrderProcessEnd();
            return View(ret);
        }

        public void OrderProcessEnd()
        {
            if (SessionHandler.OrderID != String.Empty)
            {
                ds.UpdateExportFlag(SessionHandler.OrderID);
            }
            Session.Clear();
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
            TSView ret = new TSView();
            ret.AccountAll = ds.GetAllAccountInfo(Territory);
            ret.Account = ds.GetSingleAccountInfo("0000");
            ViewBag.Terr = Territory;
            return View(ret);
        }

        [HttpPost]
        public ActionResult TimeSheet(string tempAccount, string ss, string Territory, Accounts ts)
        {

            TSView ret = new TSView();
            //ret.Account = ds.GetSingleAccountInfo(tempAccount);
            if (tempAccount == "")
            {
                ret.Invoice = ds.GetInvoiceData(ts.StoreNumber);
                ret.Account = ds.GetSingleAccountInfo(ts.StoreNumber);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            else if (tempAccount != "" || ts.StoreNumber != "" )
            {
                ret.Invoice = ds.GetInvoiceData(tempAccount);
                ret.Account = ds.GetSingleAccountInfo(tempAccount);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            ret.TaskList = ds.GetTaskList();
            ViewBag.Terr = Territory;
            return View(ret);
        }
        public ActionResult TSMessage(string Territory)
        {
            ViewBag.Terr = Territory;
            return View();
        }
        
        public ActionResult TimeSheetAdd(TimeSheet timeSheet)
        {
           int cnt =  ds.AddTimeSheeInfo(timeSheet);
           if (cnt > 0)
               return Json(new { success = true });
            else
               return Json(new { success = false });
        }


        public ActionResult RecentOrders(string Territory, string Val)
        {

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

        public ActionResult UpdatePersonalInfo()
        {
            return View();
        }

       
    }
}
