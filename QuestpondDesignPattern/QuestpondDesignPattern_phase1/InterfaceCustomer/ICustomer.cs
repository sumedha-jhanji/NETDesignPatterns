﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceCustomer
{
    public interface ICustomer
    {
        string CustomerName { get; set; }
        string CustomerAddress { get; set; }
        string PhoneNumber { get; set; }
        decimal BillAmount { get; set; }
        DateTime BillDate { get; set; }

        void Validate();

    }
}
