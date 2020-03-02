using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace LinqToDataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataSet ds = new DataSet();
                string connectString = "Data Source=.;Initial Catalog=FFSLJ;uid=sa;password=123456";
                SqlDataAdapter da = new SqlDataAdapter("select a.Cus_ID, a.CustomerCode,a.TrueName,a.DistirctID  " +
                    "from  BCS_Customer a where a.DistirctID=1 " +
                    "select b.Cus_ID, b.CM_ID, b.DetailedAddress,b.Dis_ID   from BCS_Customer a inner join BCS_CustomerMeter b on a.Cus_ID = b.Cus_ID where b.Dis_ID = 1" +
                    "select d.Dis_ID,d.DistrictName,d.MeterReadingRouteID  from BCS_Customer a inner join BCS_District d on a.DistirctID=d.Dis_ID where  d.Dis_ID = 1", connectString);
                //Add Table Mapping
                da.TableMappings.Add("Table", "Customer");
                da.TableMappings.Add("Table1", "CustomerMeter");
                da.TableMappings.Add("Table2", "District");
                //Fill DataSet
                da.Fill(ds);
                //Add data ralations
                DataTable customer = ds.Tables["Customer"];
                DataTable customerMeter = ds.Tables["CustomerMeter"];
                DataRelation cusMeter = new DataRelation("cusMeter", customer.Columns["Cus_ID"], customerMeter.Columns["Cus_ID"], true);
                ds.Relations.Add(cusMeter);

                DataTable customer2 = ds.Tables["Customer"];
                DataTable distinct = ds.Tables["District"];
            //    DataRelation cusDistinct = new DataRelation("cusDis", distinct.Columns["Dis_ID"], customer2.Columns["DistirctID"], true);
             //   ds.Relations.Add(cusDistinct);

                var query = from cus in customer.AsEnumerable()
                            where cus.Field<Int64>("Cus_ID") == 5829
                            select new {
                                Cus_ID = cus.Field<Int64>("Cus_ID"),
                                CustomerCode = cus.Field<string>("CustomerCode"),
                                TrueName=cus.Field<string>("TrueName")
                            };
                foreach (var item in query)
                {
                    Console.WriteLine("{0} {1} {2}",item.Cus_ID,item.CustomerCode,item.TrueName);
                }
                
            }
            catch (Exception ex)
            {

                //   Console.WriteLine("SQL exception occurred: " + ex.Message); 
                throw;
            }

      












        }
    }
}
