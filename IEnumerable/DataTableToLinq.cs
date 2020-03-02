using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;

namespace IEnumerable
{
  public   class DataTableToLinq
    {
        
        public DataTable GetDataTable() {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID",typeof(System.Int32));
            dt.Columns.Add("Name",typeof(System.String));
            int[] id = { 1,2,3,4,5,6,7,8};
            string[] name = { "aaa","bbb","ccc","dddd","eee","fff","ggg","hhh"};
            for (int i = 0; i < 8; i++)
            {
                dt.Rows.Add(new object[] { id[i], name[i] });
            }
            return dt;

        }
        public string  DataTalbeToLinq() {
            DataTable dt = GetDataTable();
            
            var result = dt.AsEnumerable().OrderByDescending(x=>x.Field<int>("ID")).FirstOrDefault(x=>x.Field<int>("ID")==2);
            string json = JsonConvert.SerializeObject(result);
            return json;
        }
       
    }
}
