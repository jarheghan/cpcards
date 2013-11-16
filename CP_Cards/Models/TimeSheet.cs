using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class TimeSheet
    {
        public string TSDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Cartons { get; set; }
        public string TrackingNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string ServiceProvided { get; set; }
        public string Notes { get; set; }
        public string StoreNumber { get; set; }
        public String Add_User { get; set; }
        public DateTime Add_Date { get; set; }
        public bool ExportFlag { get; set; }
    }
}