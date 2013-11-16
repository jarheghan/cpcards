using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class Order_Details
    {
        public int  Rack_Space { get; set; }
        public int Ord_Ort_ID { get; set; }
        public string Rack_ID { get; set; }
        public string Rack_Display { get; set; }
        public string Store_No { get; set; }
        public DateTime Add_Date { get; set; }
        public DateTime Change_Date { get; set; }
        public string Add_User { get; set; }
        public string Change_User { get; set; }
        public bool Delete_Flag { get; set; }
        public bool Export_Flag { get; set; }
    }
}