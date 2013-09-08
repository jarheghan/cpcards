using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CP_Cards.infasctructure
{
    public static class RamdomTransactionNo
    {
       private static Random random = new Random((int)DateTime.Now.Ticks);
       private static readonly Random random_int = new Random();
       private static readonly object syncLock = new object();
        public static string GenerateTransationNumber()
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            int size = 10;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static int GenerateTransationNumberInt()
        {
            lock (syncLock)
            { // synchronize
                int ss = random_int.Next(1000000, 99999999);
                return ss;
            }
        }
    }
}