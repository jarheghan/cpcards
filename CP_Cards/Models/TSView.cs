using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class TSView
    {
        public IEnumerable<Accounts> Account { get; set; }
        public IEnumerable<Accounts> AccountAll { get; set; }
        public IEnumerable<Orders> Order { get; set; }
        public TimeSheet TimeSheets { get; set; }
        public ConstValues ConstVal { get; set; }
        public IEnumerable<Retailers> Retailers { get; set; }
        public string  StoreNumber { get; set; }
    }
}