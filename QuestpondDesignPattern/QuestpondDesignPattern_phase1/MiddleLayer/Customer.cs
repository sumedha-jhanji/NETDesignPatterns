using InterfaceCustomer;

namespace MiddleLayer
{
    //obsolete
    public class Customer : BaseCustomer
    {
        public Customer()
        {
            CustomerType = "Customer";
        }
        public Customer(IValidation<ICustomer> obj): base(obj) { CustomerType = "Customer"; }
    }
}
