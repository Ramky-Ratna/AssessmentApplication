using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Customers
    {
        public int CustomerId { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public decimal? TotalSalesAmount { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
