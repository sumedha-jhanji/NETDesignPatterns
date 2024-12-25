using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceCustomer
{
    // Design pattern :- Stratergy pattern helps to choose lgorithms dynamically
    public interface IValidation<AnyType>
    {
        void Validate(AnyType obj);
    }
}
