using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class RackView
    {
        public IEnumerable<Cards> Cards { get; set; }
        public Cards SingleCards { get; set; }
        public IEnumerable<Cards> EDCards { get; set; }
        public IEnumerable<Accounts> Accounts { get; set; }
        public string  rack { get; set; }
        public Rack Racktemp { get; set; }
        public Order_Details OrderDetail { get; set; }
        public ConstValues CosstValue { get; set; }
    }
}