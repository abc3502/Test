using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using QuartzJob.Util;

namespace QuartzJob.Job
{
    public class DataUploadJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return null;
          //  throw new NotImplementedException();
        }

        public void DataUpload() {
            string url = "http://10.10.207.34/ExtIntTest/MeterRecord.svc";
         ///   IDoubleService proxy = WcfInvokeFactory.CreateServiceByUrl<string>(url);
            int result = proxy.Add(1, 3);

        }







    }
}