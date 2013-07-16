using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class Accounts
    {
        public string StoreNumber { get; set; }
        public string  Contact { get; set; }
        public string CustName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Territory { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public decimal AccountID { get; set; }
    }
}