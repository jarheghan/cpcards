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

        public string Login(string Territory, string Password)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                string territory = sqlConnection.Query<string>("select Territory from retailers where Territory = @Territory and Password = @Password",
                                                            new {Territory = Territory, Password = Password })
                                                            .FirstOrDefault();
                sqlConnection.Close();
                return territory;
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

        public Accounts GetSingleAccountInfo1(string storenumber)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                Accounts retalierInfo
                    = sqlConnection.Query<Accounts>("select * from Accounts where storenumber = @storenumber", new { storenumber = storenumber }).FirstOrDefault();
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
                    , new { racks = racks , cardtype = cardType});
                sqlConnection.Close();
                return cardInfo;

            }
        }


        public int GetRackMaxRow(string racks, string storenumber)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
               int cardInfo
                    = sqlConnection.Query<int>("select MAX(row) from cards where Rack = @racks and Number = @storenumber"
                    , new { racks = racks, storenumber = storenumber }).First();
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
                    = sqlConnection.Query<Cards>("select * from cards where Rack =  @racks  and Number = @storenumber order by row desc,space"
                    , new { racks = racks, storenumber = storenumber });
                sqlConnection.Close();
                return cardInfo;

            }
        }

        public void InsertOrderDetailsInfo(Order_Details orderDetails)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                int cnt = sqlConnection.Execute("insert into Order_Details(ord_order_id,ord_rack_space,ord_rack_id,ord_rack_display,ord_store_no" +
                                             ",ord_delete_flag,ord_add_user,ord_add_date,ord_export_flag)" +
                                                   "values(@Ord_Ort_ID,@Rack_Space,@Rack_ID,@Rack_Display,@Store_No,@Delete_Flag,@Add_User,@Add_Date,@Export_Flag)"
                                                   , new Order_Details
                                                   {
                                                       Ord_Ort_ID = orderDetails.Ord_Ort_ID,
                                                       Rack_Space = orderDetails.Rack_Space,
                                                       Rack_ID = orderDetails.Rack_ID,
                                                       Rack_Display = orderDetails.Rack_Display,
                                                       Store_No = orderDetails.Store_No,
                                                       Delete_Flag = orderDetails.Delete_Flag,
                                                       Add_User = Environment.UserName,
                                                       Add_Date = DateTime.Now,
                                                       Export_Flag = true
                                                   });
                sqlConnection.Close();

            }
        }

        public void UpdateExportFlag(string order_id)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                int complete = sqlConnection.Execute("update order_details set ord_export_flag = 0 where ord_order_id = @order_id", new {order_id = order_id });
                sqlConnection.Close();
            }

        }

        public void InsertOrder(Orders orders)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                int cnt = sqlConnection.Execute("insert into Orders(S_Date,StoreNumber,InvNumber,SeasonName,Code,Amount,Territory,City,State,CustName,Order_Complete)" +
                                                   "values(@S_Date,@StoreNumber,@InvNumber,@SeasonName,@Code,@Amount,@Territory,@City,@State,@CustName,@OrderComplete)"
                                                   , new Orders
                                                   {
                                                       S_Date = orders.S_Date,
                                                       StoreNumber = orders.StoreNumber,
                                                       InvNumber = orders.InvNumber,
                                                       SeasonName = orders.SeasonName,
                                                       Code = orders.Code,
                                                       Amount = orders.Amount,
                                                       Territory = orders.Territory,
                                                       City = orders.City,
                                                       State = orders.State,
                                                       AccountID = orders.AccountID,
                                                       CustName = orders.CustName,
                                                       OrderComplete = orders.OrderComplete
                                                   });
                sqlConnection.Close();

            }
        }


        public void InsertOrderTransactionInfo(ordertransaction ordertrans)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                int cnt = sqlConnection.Execute("insert into Order_Transations(ort_transation_no,ort_store_no,ort_delete_flag,ort_add_date)" +
                                                   "values(@ort_transation_no,@ort_store_no,@ort_delete_flag,@ort_add_date)"
                                                   , new ordertransaction
                                                   {
                                                       ort_transation_no = ordertrans.ort_transation_no,
                                                       ort_store_no = ordertrans.ort_store_no,
                                                       ort_delete_flag = ordertrans.ort_delete_flag,
                                                       ort_add_date = DateTime.Now
                                                   });
                sqlConnection.Close();

            }
        }

        public int GetTransationNumber(int InvNumber)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                int st_number = sqlConnection.Query<int>("select count(InvNumber) from orders where InvNumber = @InvNumber"
                                                  , new { InvNumber = InvNumber }).FirstOrDefault();
                sqlConnection.Close();
                return st_number;
            }
        }

        public int GetTransationNumber2(string storenumber, string adddate, bool ordercomplete)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                int st_number = sqlConnection.Query<int>("select InvNumber from orders where StoreNumber = @StoreNumber" +
                                                    " and S_Date = @add_date and Order_Complete = @ordercomplete"
                                                  , new { StoreNumber = storenumber, add_date = adddate,
                                                            ordercomplete = ordercomplete}).FirstOrDefault();
                sqlConnection.Close();
                return st_number;
            }
        }


        public IEnumerable<Invoice> GetInvoiceData(string storenumber)
        {
            IEnumerable<Invoice> invoice;
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                invoice = sqlConnection.Query<Invoice>("select * from invoice where inv_store_number = @storenumber order by inv_date ASC"
                                                  , new
                                                  {
                                                      storenumber = storenumber,
                                                  });
                sqlConnection.Close();
                return invoice;
            }
        }


        public IEnumerable<TaskList> GetTaskList()
        {
            IEnumerable<TaskList> tasklist;
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                tasklist = sqlConnection.Query<TaskList>("select tal_id, tal_name from tasklist where tal_delete_flag = 0");
                                                  
                sqlConnection.Close();
                return tasklist;
            }
        }
        
        


        //select tal_id, tal_name from tasklist where tal_delete_flag = 0





        public int AddTimeSheeInfo(TimeSheet timesheet)
        {
            using (var sqlConnection = new SqlConnection(Constant.connectionString))
            {
                sqlConnection.Open();
                try
                {
                    int cnt = sqlConnection.Execute("insert into TimeSheet(StoreNumber,TSDate,Cartons,InvoiceNo,ServiceProvided,Notes,TrackingNumber,InvoiceDate,Add_User,Add_Date,Export_flag)" +
                                                       "values(@StoreNumber,@TSDate,@Cartons,@InvoiceNo,@ServiceProvided,@Notes,@TrackingNumber,@InvoiceDate,@Add_User,@Add_Date,@ExportFlag)"
                                                       , new TimeSheet
                                                       {
                                                           StoreNumber = timesheet.StoreNumber,
                                                           TSDate = timesheet.TSDate,
                                                           Cartons = timesheet.Cartons,
                                                           InvoiceNo = timesheet.InvoiceNo,
                                                           TrackingNumber = timesheet.TrackingNumber,
                                                           ServiceProvided = timesheet.ServiceProvided,
                                                           InvoiceDate = timesheet.InvoiceDate,
                                                           Notes = timesheet.Notes,
                                                           Add_User = Environment.UserName,
                                                           Add_Date = DateTime.Now,
                                                           ExportFlag = false
                                                       });
                    sqlConnection.Close();
                    return cnt;
                }
                catch (Exception ex)
                { return 0; }
               

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