using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class ordertransaction
    {
        public string ort_transation_no { get; set; }
        public string ort_store_no { get; set; }
        public bool ort_delete_flag { get; set; }
        public DateTime ort_add_date { get; set; }
        public string ort_add_user { get; set; }
    }
}