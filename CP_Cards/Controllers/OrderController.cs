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
    public class OrderController : Controller
    {
        TSView vTS = new TSView();
        //
        // GET: /Home/
        DataService ds = new DataService();
        public Decimal Price { get; set; }
        public string Cnt_Flag { get; set; }
        public ActionResult OrderEntryStep1(string Territory, string Val)
        {
            TSView ret = new TSView();
            ret.AccountAll = ds.GetAllAccountInfo(Territory);
            if (ret.AccountAll.Count() > 0)
            {
                Accounts ss = new Accounts();
                ss.StoreNumber = string.Empty;
                ss.Phone = string.Empty;
                ss.CustName = string.Empty;
                ret.SingleAccount = ss;
            }
            //ret.SingleAccount = ds.GetSingleAccountInfo1("00000");
            ViewBag.Terr = Territory;
            if (Val == "All")
            {
                ret.Order = ds.GetAllInvoiceInfo(Territory);
            }
            ViewBag.val = Val;
            return View(ret);
        }

        public ActionResult CreateTransactionOrder(string Territory, string val, string storenumber)
        {
            //create order transaction
            ordertransaction ordertrans = new ordertransaction();
            string trans_no = RamdomTransactionNo.GenerateTransationNumber();

            ordertrans.ort_store_no = storenumber;
            ordertrans.ort_transation_no = trans_no;
            ordertrans.ort_delete_flag = false;
            ds.InsertOrderTransactionInfo(ordertrans);

            return RedirectToAction("OrderEntryStep1", new { Territory = Territory, Val = val });
        }

        [HttpPost]
        public ActionResult OrderEntryStep1(string tempAccount, string ss, string Territory, Accounts ts,string StoreNumber)
        {
            TSView ret = new TSView();
            //ret.Account = ds.GetSingleAccountInfo(tempAccount);
            if (tempAccount == "" && ts.StoreNumber == "")
            {
                ret.Order = ds.GetAllInvoiceInfo(Territory);
                ret.Account = ds.GetSingleAccountInfo(ts.StoreNumber);
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            else if (tempAccount != "" || (ts.StoreNumber != "" || ts.StoreNumber == ""))
            {
                ret.Order = ds.GetInvoiceInfo(tempAccount);
                ret.SingleAccount = tempAccount != ""? ds.GetSingleAccountInfo1(tempAccount):ds.GetSingleAccountInfo1(ts.StoreNumber) ;
                ret.AccountAll = ds.GetAllAccountInfo(Territory);
            }
            ViewBag.Terr = Territory;
            return View(ret);
        }

        public ActionResult OrderEntryStep2(string storenumber, string Display, string TheRack, string Territory, string complete)
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
            ViewBag.Terr = Territory;

            ConstValues cv = new ConstValues();
            if (complete == null || complete == "")
            {
                cv.Completed = "0";
                RV.CosstValue = cv;
            }
            else
            {

                cv.Completed = complete;
                RV.CosstValue = cv;
            }
                return View(RV);
            
        }

        [HttpPost]
        public ActionResult OrderEntryStep2(string storenumber, string Display, string rack, Cards cards, string rackspace,string Territory, IEnumerable<Cards> EDCards)
        {
            //storenumber = "0129";
            RackView RV = new RackView();
            RV.Cards = ds.GetRackByCardType(storenumber, Display);
            ViewBag.display = cards.Display;
            ViewBag.Storenumber = storenumber;
            RV.EDCards = ds.GetEveryDayCard( cards.Rack,storenumber);
            RV.Accounts = ds.GetSingleAccountInfo(storenumber);
            ViewBag.rackid = rack;
            ViewBag.Terr = Territory;
            ViewBag.RackRow = ds.GetRackMaxRow(cards.Rack, storenumber);
            ConstValues cv = new ConstValues();
            cv.Completed = "0";
            RV.CosstValue = cv;
            return View(RV);
        }

        

        public ActionResult OrderEntry2Save(IEnumerable<string> rackspace, RackView SingleCard, string storenumber,
            string Display, string Territory, IEnumerable<string> retailPrice)
        {
            Price = 0;
            Cnt_Flag = "0";
            RackView RV = new RackView();
            RV.OrderDetail = new Order_Details();
            ///Create a an Order for every transaction of card type that is created.
            ///So create a method to for the created
            //int trans_no = ds.GetTransationNumber(storenumber);

            if (SessionHandler.OrderID == String.Empty)
            {
                int trans_noint = RamdomTransactionNo.GenerateTransationNumberInt();
                SessionHandler.OrderID = trans_noint.ToString();
            }
            
            
            ///Getting the Order information to create an order entry
            if (retailPrice != null)
            {
                foreach (string price in retailPrice)
                {
                    Price = Price + (price == "" ? 0 : Convert.ToDecimal(price));
                }

                int InvNumber = ds.GetTransationNumber(int.Parse(SessionHandler.OrderID));
                try
                {
                    if (InvNumber == 0)
                    {
                        Orders ord = new Orders();
                        Accounts acct = ds.GetSingleAccountInfo1(storenumber);
                        ord.AccountID = acct.AccountID;
                        ord.City = acct.City;
                        ord.CustName = acct.CustName;
                        ord.S_Date = DateTime.Now;
                        ord.State = acct.State;
                        ord.Territory = acct.Territory;
                        ord.Amount = Price;
                        ord.SeasonName = Display == "E" ? "Everyday Card" : "";
                        ord.Code = Display;
                        ord.InvNumber = int.Parse(SessionHandler.OrderID);
                        ord.StoreNumber = storenumber;
                        ord.OrderComplete = false;

                        ds.InsertOrder(ord);
                    }
                }
                catch { }



                try
                {
                    foreach (var space in rackspace)
                    {
                        if (space != "")
                        {
                            RV.OrderDetail.Rack_Space = Convert.ToInt16(space);
                            RV.OrderDetail.Rack_ID = SingleCard.SingleCards.Rack;
                            RV.OrderDetail.Store_No = storenumber;
                            RV.OrderDetail.Rack_Display = Display;
                            RV.OrderDetail.Ord_Ort_ID = int.Parse(SessionHandler.OrderID);
                            ds.InsertOrderDetailsInfo(RV.OrderDetail);

                        }
                    }
                    Cnt_Flag = "1";
                }

                catch { Cnt_Flag = "0"; }
            }
            else { Cnt_Flag = "2"; }
           
            ViewBag.Terr = Territory;

            return RedirectToAction("OrderEntryStep2", new { Display = Display, storenumber = storenumber, Territory = Territory, complete = Cnt_Flag });
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



        public ActionResult OrderEntryStep1new(string Territory)
        {
            ViewBag.Terr = Territory;
            return View();
        }

        public ActionResult OrderEntryStep2Advance(string storenumber, string Display, string TheRack, string Territory, string complete)
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
            ViewBag.Terr = Territory;

            ConstValues cv = new ConstValues();
            if (complete == null || complete == "")
            {
                cv.Completed = "0";
                RV.CosstValue = cv;
            }
            else
            {
                cv.Completed = complete;
                RV.CosstValue = cv;
            }
            return View(RV);
        }

        [HttpPost]
        public ActionResult OrderEntryStep2Advance(string storenumber, string Display, string rack, Cards cards, string rackspace, string Territory, IEnumerable<Cards> EDCards)
        {
            //storenumber = "0129";
            RackView RV = new RackView();
            RV.Cards = ds.GetRackByCardType(storenumber, Display);
            ViewBag.display = cards.Display;
            ViewBag.Storenumber = storenumber;
            RV.EDCards = ds.GetEveryDayCard(cards.Rack, storenumber);
            RV.Accounts = ds.GetSingleAccountInfo(storenumber);
            ViewBag.Terr = Territory;
            ConstValues cv = new ConstValues();
            cv.Completed = "0";
            RV.CosstValue = cv;
            return View(RV);
        }


        public ActionResult OrderEntry2AdvanceSave(IEnumerable<string> rackspace, RackView SingleCard, string storenumber,
            string Display, string Territory, IEnumerable<string> retailPrice)
        {
            Price = 0;
            Cnt_Flag = "0";
            RackView RV = new RackView();
            RV.OrderDetail = new Order_Details();
            ///Create a an Order for every transaction of card type that is created.
            ///So create a method to for the created
            //int trans_no = ds.GetTransationNumber(storenumber);
            int trans_noint = RamdomTransactionNo.GenerateTransationNumberInt();
            ///Getting the Order information to create an order entry
            ///
            foreach (string price in retailPrice)
            {
                Price = Price + (price == "" ? 0 : Convert.ToDecimal(price));
            }
            try
            {
                Orders ord = new Orders();
                Accounts acct = ds.GetSingleAccountInfo1(storenumber);
                ord.AccountID = acct.AccountID;
                ord.City = acct.City;
                ord.CustName = acct.CustName;
                ord.S_Date = DateTime.Now;
                ord.State = acct.State;
                ord.Territory = acct.Territory;
                ord.Amount = Price;
                ord.SeasonName = "Everyday Card";
                ord.Code = Display;
                ord.InvNumber = trans_noint;
                ord.StoreNumber = storenumber;

                ds.InsertOrder(ord);
            }
            catch { }

            int InvNumber = ds.GetTransationNumber(trans_noint);
            if (InvNumber == trans_noint)
            {
                try
                {
                    foreach (var space in rackspace)
                    {
                        if (space != "")
                        {
                            RV.OrderDetail.Rack_Space = Convert.ToInt16(space);
                            RV.OrderDetail.Rack_ID = SingleCard.SingleCards.Rack;
                            RV.OrderDetail.Store_No = storenumber;
                            RV.OrderDetail.Rack_Display = Display;
                            RV.OrderDetail.Ord_Ort_ID = trans_noint;
                            ds.InsertOrderDetailsInfo(RV.OrderDetail);

                        }
                    }
                    Cnt_Flag = "1";
                }

                catch { Cnt_Flag = "0"; }
            }
            ViewBag.Terr = Territory;

            return RedirectToAction("OrderEntryStep2", new { Display = Display, storenumber = storenumber, Territory = Territory, complete = Cnt_Flag });
        }

    }
}
