using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class Cards
    {
        public string  Number { get; set; }
        public string  Rack { get; set; }
        public decimal Space { get; set; }
        public int Bin { get; set; }
        public string Display { get; set; }
        public decimal A_Retail { get; set; }
        public string A_Design { get; set; }
        public string M_Design { get; set; }
        public string Dis1 { get; set; }
        public decimal AccountID { get; set; }
    }
}