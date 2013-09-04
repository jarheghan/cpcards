using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CP_Cards.Models;
using CP_Cards.infasctructure;

namespace CP_Card.Test
{
    [TestFixture]
    public class TestClass
    {
        DataService ds = new DataService();
        [Test]
        public void check_for_value()
        {
            RackView RV = new RackView();
            RV.EDCards = ds.GetEveryDayCard("A", "0101");
            var seq = RV.EDCards.ToList<Cards>();
            var result = seq.Select((s, i) => new { Value = s, Index = i }).GroupBy(item => item.Index / 13, item => item.Value);
            var result1 = seq.GroupBy(x => x.Space/8);

            //IEnumerable<string > num = result.Select(s => s.Select(x => x.Space)).Cast<string>();

            //Console.WriteLine(num.GetType());
            //Assert.AreEqual(6, num);

            foreach (var ss in result1.Reverse())
            {
               // Assert.AreEqual("6", ss.Select(s => s.M_Design));
                Console.WriteLine(ss.Key.ToString());
                foreach(var tt  in ss.OrderByDescending(x=>x.Space))
                {
                Console.WriteLine(tt.Space);
                //.Where(s => s.Equals(2)));
                }
            }
        }

        [Test]
        public void  create_ramdom_number_using_string()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            int size = 10;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
               
            }
            Console.WriteLine(builder.ToString());
        }
    }
}
