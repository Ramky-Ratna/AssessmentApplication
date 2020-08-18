using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public interface IDataRepository
    {
        public void LogFile(string sExceptionName, string sEventName, string sControlName, string sFormName);
        public void AddCustomer(List<Customers> customer);
        public List<Customers> GetAllCustomers();

    }
}
