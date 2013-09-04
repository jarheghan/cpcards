using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class TimeSheet
    {
        public string TSDate { get; set; }
        public string Miles { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string InvWorkOn { get; set; }
        public string ServiceProvided { get; set; }
        public string Notes { get; set; }
        public string StoreNumber { get; set; }
    }
}