using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class Orders
    {
        public DateTime S_Date { get; set; }
        public string StoreNumber { get; set; }
        public int InvNumber { get; set; }
        public string SeasonName { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public string Territory { get; set; }
        public string CustName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal AccountID { get; set; }
        public bool OrderComplete { get; set; }
    }
}