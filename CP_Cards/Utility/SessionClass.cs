using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_Cards.Utility
{
    public static class SessionHandler
    {
        private static string _orderID = "OrderID";
        public static string OrderID
        {
            get
            {
                if (HttpContext.Current.Session[SessionHandler._orderID] == null)
                { return string.Empty; }
                else
                {
                    return HttpContext.Current.Session[SessionHandler._orderID].ToString();
                }
            }

            set {HttpContext.Current.Session[SessionHandler._orderID] = value;}

        }
    }
}