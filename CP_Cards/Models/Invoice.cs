using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class Invoice
    {
        public string inv_number { get; set; }
        public string inv_store_number { get; set; }
        public bool inv_export_flag { get; set; }
        public string inv_ship_track { get; set; }
        public DateTime  inv_date { get; set; }
        public int inv_cartons { get; set; }
        public string inv_type_code { get; set; }
        public string inv_add_user { get; set; }
        public DateTime inv_add_date { get; set; }
        public string inv_change_user { get; set; }
        public DateTime inv_change_date { get; set; }
        public string inv_season { get; set; }
        public int inv_time_wk { get; set; }
        public int inv_time_wk2 { get; set; }
    }
}