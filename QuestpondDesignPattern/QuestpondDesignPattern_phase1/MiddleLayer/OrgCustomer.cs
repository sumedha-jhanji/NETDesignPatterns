using InterfaceCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class OrgCustomer : BaseCustomer
    {
        public OrgCustomer(IValidation<ICustomer> obj, string customerType) :base(obj){
            CustomerType = customerType;
        }
    }
}
