using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;
using System.Collections;
using CP_Cards.Models;
using System.Data;

namespace CP_Cards.infasctructure
{
    public class DataService
    {
        public IEnumerable<Retailers> GetLoginInfo()
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Retailers> retalierInfo = sqlConnection.Query<Retailers>("select * from retailers");
                sqlConnection.Close();
                return retalierInfo;
            }
        }


        public IEnumerable<Memos> GetMemoInfo(string Territory)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Memos> retalierInfo 
                    = sqlConnection.Query<Memos>("select * from memo where Territory = @Territory", new {Territory =  Territory});
                sqlConnection.Close();
                return retalierInfo;
               
            }
        }

        public IEnumerable<Accounts> GetSingleAccountInfo(string storenumber)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Accounts> retalierInfo 
                    = sqlConnection.Query<Accounts>("select * from Accounts where storenumber = @storenumber", new {storenumber = storenumber});
                sqlConnection.Close();
                return retalierInfo;

            }
        }


        public IEnumerable<Accounts> GetAllAccountInfo(string Territory)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Accounts> retalierInfo
                    = sqlConnection.Query<Accounts>("select *, storenumber + '-' + Custname + '-' + City as ConInfo from Accounts where Territory = @Territory", new { Territory = Territory });
                sqlConnection.Close();
                return retalierInfo;

            }
        }

        public IEnumerable<Orders> GetInvoiceInfo(string storenumber)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Orders> retalierInfo
                    = sqlConnection.Query<Orders>("select * from Orders where storenumber = @storenumber", new { storenumber = storenumber });
                sqlConnection.Close();
                return retalierInfo;

            }
        }


        public IEnumerable<Orders> GetAllInvoiceInfo(string Territory)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Orders> retalierInfo
                    = sqlConnection.Query<Orders>("select * from Orders where territory LIKE '%' + @Territory + '%'", new { Territory = Territory });
                sqlConnection.Close();
                return retalierInfo;

            }
        }


        public IEnumerable<Cards> GetRackByCardType(string racks, string cardType)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Cards> cardInfo
                    = sqlConnection.Query<Cards>("select Distinct Rack from cards where Number = @racks  and Display LIKE '%' + @cardtype + '%'"
                    , new { racks = "0101" , cardtype = "E"});
                sqlConnection.Close();
                return cardInfo;

            }
        }

        public IEnumerable<Cards> GetEveryDayCard(string racks, string storenumber)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Cards> cardInfo
                    = sqlConnection.Query<Cards>("select * from cards where Rack = 'Binary' + @racks  and Number = @storenumber"
                    , new { racks = racks, storenumber = storenumber });
                sqlConnection.Close();
                return cardInfo;

            }
        }








        public void AddTimeSheeInfo(TimeSheet timesheet)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                int cnt = sqlConnection.Execute("insert into TimeSheet(StoreNumber,TSDate,StartTime,EndTime,InvoiceNo,ServiceProvided,Notes)" +
                                                   "values(@StoreNumber,@TSDate,@StartTime,@EndTime,@InvoiceNo,@ServiceProvided,@Notes)"
                                                   , new TimeSheet
                                                   {
                                                       StoreNumber = timesheet.StoreNumber,
                                                       TSDate = timesheet.TSDate,
                                                       StartTime = timesheet.StartTime,
                                                       FinishTime = timesheet.FinishTime,
                                                       InvWorkOn = timesheet.InvWorkOn,
                                                       ServiceProvided = timesheet.ServiceProvided,
                                                       Miles = timesheet.Miles,
                                                       Notes = timesheet.Notes
                                                   });
                sqlConnection.Close();

            }

        }
    }






    class Constant
    {

        public static readonly string connectionString = "Data Source=localhost;Initial Catalog=cardsplus;Integrated Security=True";

        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

    }


}