﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class BaseCustomer
    {
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime? BillDate { get; set; }

        public virtual void Validate()
        {
            throw new Exception("Not implemented");
        }
    }
}
