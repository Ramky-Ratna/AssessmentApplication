using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class CustomerType
    {
        public int CustomerTypeId { get; set; }
        public string CustomerTypeValue { get; set; }
        public bool? IsActive { get; set; }
    }
}
