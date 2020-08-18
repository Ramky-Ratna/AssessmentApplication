using DataService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataService
{
    public class DataRepository : IDataRepository
    {
        public void LogFile(string sExceptionName, string sEventName, string sControlName, string msg)
        {
            StreamWriter log;
            if (!File.Exists("logfile.txt"))
            {
                log = new StreamWriter("logfile.txt");
            }
            else
            {
                log = File.AppendText("logfile.txt");
            }
            // Write to the file:
            log.WriteLine("Data Time:" + DateTime.Now);
            log.WriteLine("Exception Name:" + sExceptionName);
            log.WriteLine("Event Name:" + sEventName);
            log.WriteLine("Class Name:" + sControlName);
            log.WriteLine("Message:" + msg);
            // Close the stream:
            log.Close();
        }
    
        public void AddCustomer(List<Customers> customers)
        {
            try
            {
                using (var context = new AssessmentDBContext())
                {
                    foreach (Customers customer in customers)
                    {
                        context.Customers.Add(customer);
                        context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                LogFile(ex.GetType().ToString(),"AddCustomer","DataRepository.cs",ex.InnerException.Message.ToString());
                Console.WriteLine(ex.InnerException.Message);
            }
            
        }

        public List<Customers> GetAllCustomers()
        {
            try
            {
                using (var context = new AssessmentDBContext())
                {
                    var customers = context.Customers.ToList();
                    return customers;
                }
            }
            catch (Exception ex)
            {               
                LogFile(ex.GetType().ToString(), "GetAllCustomers", "DataRepository.cs", ex.InnerException.Message.ToString());
                return null;
            }
        }
    }
}
