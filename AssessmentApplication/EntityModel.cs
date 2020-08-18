using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentApplication
{
    public class EntityModel
    {
        public int CustomerId { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public double TotalSalesAmount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
