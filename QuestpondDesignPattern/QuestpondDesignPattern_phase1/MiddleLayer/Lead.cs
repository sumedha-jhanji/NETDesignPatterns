using InterfaceCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class Lead : BaseCustomer
    {
        public Lead(IValidation<ICustomer> obj) : base(obj) { }
    }
}
