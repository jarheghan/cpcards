using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class SpecialItems
    {
        public string Year { get; set; }
        public string  Month { get; set; }
        public string Day { get; set; }
        public string TimeStartHour { get; set; }
        public string TimeStartMinute { get; set; }
        public string TimeStartAMorPM { get; set; }
        public string TimeEndHour { get; set; }
        public String TimeEndMinutes { get; set; }
        public string TimeEndAmorPM { get; set; }
    }
}