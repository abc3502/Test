using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace LinqToSqlTest
{
    class Program
    {
        public static  string constr = ConfigurationManager.ConnectionStrings["sqlserver"].ConnectionString;
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            testDataContext db = new testDataContext();
            Table<BCS_District> district = db.GetTable<BCS_District>();
            db.Log = Console.Out;
            //IQueryable<BCS_District> query = from a in district where a.DepartmentCode == "21" select a;
            //foreach (var item in query)
            //{
            //    Console.WriteLine(item.DistrictName, item.CreateDT);
            //}



            //// IQueryable<BCS_Charge> query = db.BCS_Charge.Take<BCS_Charge>(100000);
            ////查询方法
            //IQueryable<BCS_District> query1 = db.BCS_District.Where<BCS_District>(x=>x.Dis_ID>3);
            //查询语法
            //db.Log = Console.Out;
            IQueryable<BCS_District> query2 = from x in district where x.Dis_ID >= 3 select x;

            watch.Stop();
            Console.WriteLine(query2);
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Restart();
            string sql = @"SELECT TOP 100000 [Charge_ID] ,[InvoiceCode],[PriceMonth_ID],[BM_ID]
      ,[GetMoneyTime]
      ,[DepartmentCode]
      ,[CM_ID]
      ,[UsedWaterNumber]
      ,[GetMoney]
      ,[BankOfDeposit]
      ,[GetMoneyPersonId]
      ,[GetMoneyPersonName]
      ,[state]
      ,[PlanMoney]
      ,[LastBalance]
      ,[ThisBalance]
      ,[InvoiceOver]
      ,[BookOver]
      ,[OldInvoiceCode]
      ,[PosYn]
      ,[PosMoney]
      ,[CardType]
      ,[AdvanceYn]
      ,[MoneyType]
      ,[BalanceDt]
      ,[AdvanceReturn]
      ,[CreateDT]
      ,[ReviseDT]
      ,[Creator]
      ,[Operator]
      ,[Old定价]
      ,[Old年月]
      ,[Old客户]
      ,[Old水表]
      ,[Old支付方式]
      ,[OldUser]
      ,[otherOrderCode] FROM [FFSLJ].[dbo].[BCS_Charge]";
            ExcuteTable(sql,CommandType.Text);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadKey();
        }
        public static DataTable ExcuteTable(string sql,CommandType cmdType)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter=new SqlDataAdapter(sql,constr))
            {
                adapter.SelectCommand.CommandType = cmdType;
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
